library(ggplot2)
library(xlsx)
library(plyr)
library(data.table)
library(gridExtra)
library(grid)
library(lme4)
library(MASS)

stdErr <- function(x) {sd(x)/ sqrt(length(x))}

AMIGA_extract_endpoints_data_models <- function (endpoints, anticipated_number_of_periods, anticipated_number_of_samples_per_plot) {
  endpoints_amiga <- data.frame(EndpointID= character(length(endpoints)),stringsAsFactors=FALSE)
  endpoints_amiga$Group <- "Herbivores"
  endpoints_amiga$MeasurementType <- "COUNT"
  endpoints_amiga$LocLower <- 0.5
  endpoints_amiga$LocUpper <- 2.0
  endpoints_amiga$DistributionType <- "OverdispersedPoisson"
  endpoints_amiga$Mean <- NA
  endpoints_amiga$Mean_naive <- NA
  endpoints_amiga$CV <- NA
  endpoints_amiga$CvBlocks <- NA
  endpoints_amiga$CV_naive <- NA
  endpoints_amiga$CV_within_blocks_old <- NA
  endpoints_amiga$site_year_combinations_with_data <- NA
  endpoints_amiga$sites_with_data <- NA 
  endpoints_amiga$years_with_data <- NA
  
  anticipated_effort_per_block <- anticipated_number_of_periods * anticipated_number_of_samples_per_plot

  # Initialize data frame for plot totals
  data_plot_totals <- data.table(data)
  
  # Filter records with DAP < 0
  data_plot_totals <- data_plot_totals[data_plot_totals$DAP > 0,]
  
  # Multiply by number of traps: records represent the mean counts per trap
  data_plot_totals[, (endpoints) := lapply(.SD, function(x) x * data_plot_totals[['Traps']]), .SDcols = endpoints]
  
  for (i in 1:length(endpoints)) {

    # Combine counts over all counting days
    endpoint_plot_totals <- ddply(
      data_plot_totals,
      c("Site","Year","Replicate","Traps"),
      .fun = function(x, col) {
        c(
          plot_total = sum(x[[col]]),
          plot_periods = length(x[[col]])
        )
      },
      endpoints[i])
    endpoint_plot_totals$SiteYear <- paste(endpoint_plot_totals$Site,endpoint_plot_totals$Year)
    endpoint_plot_totals$effort <- endpoint_plot_totals$Traps * endpoint_plot_totals$plot_periods
    endpoint_plot_totals$anticipated_effort <- anticipated_effort_per_block
    endpoint_plot_totals$anticipated_effort_correction_factor <- endpoint_plot_totals$effort / anticipated_effort_per_block
    endpoint_plot_totals$corrected_plot_totals <- endpoint_plot_totals$plot_total / endpoint_plot_totals$anticipated_effort_correction_factor
    endpoint_plot_totals$rounded_corrected_plot_totals = round(endpoint_plot_totals$corrected_plot_totals)
    
    endpoint_summary_per_site_location <- ddply(
      endpoint_plot_totals,
      c("Site","Year"),
      summarize,
      N = length(plot_total),
      mean = mean(plot_total),
      sd = sd(plot_total),
      se = stdErr(plot_total),
      cv = sd(plot_total) / mean(plot_total)
    )
    
    measurements <- na.omit(endpoint_plot_totals$corrected_plot_totals)
    endpoint_mean_naive <- mean(measurements)
    endpoint_cv <- sd(measurements) / endpoint_mean_naive * 100
    
    fit <- tryCatch(glm(plot_total ~ 1 + Site*Year, offset=log(anticipated_effort_correction_factor), data=endpoint_plot_totals, family=quasipoisson()), error=function(e) NULL)
    sink(file.path("Outputs", paste(endpoints[i], "_glm_within_location_year_variation.txt", sep=""), fsep = "\\"))
    if (is.null(fit)) {
      print("GLM fit for extracting the within location/year variation failed")
      CV_within_blocks_old <- NA
    } else {
      fit_summary <- summary(fit)
      dispersion <- fit_summary$dispersion
      CV_within_blocks_old <- 100 * sqrt(endpoint_mean_naive * dispersion) / endpoint_mean_naive
      print(summary(fit))
      print(dispersion)
      print(CV_within_blocks_old)
    }
    sink()
    
    options(warn=1)
    fit <- tryCatch(glmmPQL(plot_total ~ 1 + offset(log(anticipated_effort_correction_factor)), random = ~1|SiteYear, data=endpoint_plot_totals, family=quasipoisson()), error=function(e) NULL)
    sink(file.path("Outputs", paste(endpoints[i], "_glmm_between_location_year_variation.txt", sep=""), fsep = "\\"))
    if (is.null(fit)) {
      print("GLMM fit for extracting the between location/year variation failed")
      endpoint_mean <- NA
      CV_within_blocks <- NA
      CV_between_blocks <- NA
    } else {
      fit_summary <- summary(fit)
      dispersion <- as.numeric(VarCorr(fit)[2,1])
      sigma <- as.numeric(VarCorr(fit)[1,2])
      endpoint_mean <- exp(as.numeric(fit$coefficients$fixed) + (sigma * sigma) / 2)
      CV_within_blocks <- 100 * sqrt(endpoint_mean * dispersion) / endpoint_mean
      CV_between_blocks <- 100 * sqrt(exp(sigma * sigma) - 1)
      print(summary(fit))
      print(dispersion)
      print(CV_within_blocks)
      print(CV_between_blocks)
    }
    sink()
    options(warn=0)
	
    endpoints_amiga$EndpointID[i] <- gsub("[.]"," ",endpoints[i])
    endpoints_amiga$Mean[i] <-  signif(endpoint_mean, digits = 4)
    endpoints_amiga$Mean_naive[i] <-  signif(endpoint_mean_naive, digits = 4)
    endpoints_amiga$CV[i] <- signif(CV_within_blocks, digits = 4)
    endpoints_amiga$CvBlocks[i] <- signif(CV_between_blocks, digits = 4)
    endpoints_amiga$CV_naive[i] <- signif(endpoint_cv, digits = 4)
    endpoints_amiga$CV_within_blocks_old[i] <- signif(CV_within_blocks_old, digits = 4)
    
    endpoint_summary_per_site_location_with_data <- subset(endpoint_summary_per_site_location, !is.na(endpoint_summary_per_site_location$mean))
    endpoints_amiga$site_year_combinations_with_data[i] <- nrow(endpoint_summary_per_site_location_with_data)
    endpoints_amiga$years_with_data[i] <- length(unique(endpoint_summary_per_site_location_with_data$Year))
    endpoints_amiga$sites_with_data[i] <- length(unique(endpoint_summary_per_site_location_with_data$Site))
    
    write.csv(endpoint_plot_totals, file = file.path("Outputs", paste(endpoints[i], "_plot_totals.csv", sep=""), fsep = "\\")) 
    write.csv(endpoint_summary_per_site_location, file = file.path("Outputs", paste(endpoints[i], "_summary.csv", sep=""), fsep = "\\")) 
  }
  return(endpoints_amiga);
}

# Read data file
data <- read.xlsx("ExtractAMIGA.xlsx", sheetName="Pitfall traps", startRow=2)
data[, 'Year'] <- as.factor(data[, 'Year'])

# Select columns
endpoints <- colnames(data)[-(1:10)]
endpoints <- endpoints[substring(endpoints, 1, 2) != "NA"]

endpoints_amiga <- AMIGA_extract_endpoints_data_models(endpoints, 6, 4)
write.csv(na.omit(endpoints_amiga), file = file.path("AMIGA_endpoints.csv", fsep = "\\"), row.names=FALSE)
write.csv(subset(endpoints_amiga, is.na(endpoints_amiga$CV)), file = file.path("AMIGA_endpoints_failed.csv", fsep = "\\"), row.names=FALSE)

selected_endpoints <- c(
  "General.collembola.all.families",
  "Springtails.entomobryids",
  "Sap.beetles",
  "Crickets",
  "Ants",
  "Spiders",
  "Wolf.spiders",
  "Centipedes",
  "Ground.beetles",
  "Rove.beetles"
)

endpoints_amiga <- AMIGA_extract_endpoints_data_models(selected_endpoints, 6, 4)
write.csv(na.omit(endpoints_amiga), file = file.path("AMIGA_endpoints_selection.csv", fsep = "\\"), row.names=FALSE)
write.csv(subset(endpoints_amiga, is.na(endpoints_amiga$CV)), file = file.path("AMIGA_endpoints_selection_failed.csv", fsep = "\\"), row.names=FALSE)

