library(ggplot2)
library(xlsx)
library(plyr)
library(data.table)
library(gridExtra)
library(grid)
library(lme4)

stdErr <- function(x) {sd(x)/ sqrt(length(x))}

AMIGA_extract_endpoints_data_models <- function (endpoints, anticipated_number_of_periods) {
  le <- length(endpoints)
  endpoints_amiga <- data.frame(EndpointID= character(le),stringsAsFactors=FALSE)
  endpoints_amiga$Group <- "Herbivores"
  endpoints_amiga$MeasurementType <- "COUNT"
  endpoints_amiga$LocLower <- 0.5
  endpoints_amiga$LocUpper <- 2.0
  endpoints_amiga$DistributionType <- "OverdispersedPoisson"
  endpoints_amiga$Mean <- NaN
  endpoints_amiga$CV <- NaN
  endpoints_amiga$CV_within_blocks <- NaN
  endpoints_amiga$CV_between_blocks <- NaN
  endpoints_amiga$CV_naive <- NaN
  
  # Initialize data frame for plot totals
  data_plot_totals <- data.table(data)
  
  # Filter records with DAP < 0
  data_plot_totals <- data_plot_totals[data_plot_totals$DAP > 0,]
  
  # Multiply by number of traps
  data_plot_totals[, (endpoints) := lapply(.SD, function(x) x * data_plot_totals[['Traps']]), .SDcols = endpoints]
  
  for (i in 1:length(endpoints)) {
    
    # Combine counts over all counting days
    endpoint_plot_totals <- ddply(
      data_plot_totals,
      c("Site","Year","Replicate"),
      .fun = function(x, col) {
        c(
          plot_total = sum(x[[col]]),
          plot_periods = length(x[[col]]),        
          period_average = sum(x[[col]]) / length(x[[col]]),
          correction_anticipated_number_of_periods = anticipated_number_of_periods * sum(x[[col]]) / length(x[[col]]),
          rounded_correction_anticipated_number_of_periods = round(anticipated_number_of_periods * sum(x[[col]]) / length(x[[col]]))
        )
      },
      endpoints[i])
    
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
    
    measurements <- na.omit(endpoint_plot_totals$correction_anticipated_number_of_periods)
    endpoint_mean <- mean(measurements)
    endpoint_cv <- sd(measurements) / endpoint_mean * 100
    
    fit <- tryCatch(glmer(rounded_correction_anticipated_number_of_periods ~ 1 + (1|Site:Year), data=endpoint_plot_totals, family="poisson")), error=function(e) NULL)
    sink(file.path("Outputs", paste(endpoints[i], "_glm_within_location_year_variation.txt", sep=""), fsep = "\\"))
    if (is.null(fit)) {
      print("GLM fit for extracting the within location/year variation failed")
      cv_within_blocks <- NA
    } else {
      fit_summary <- summary(fit)
      dispersion <- fit_summary$dispersion
      cv_within_blocks <- 100 * sqrt(endpoint_mean * dispersion) / endpoint_mean
      print(summary(fit))
      print(cv_within_blocks)
    }
    sink()
    
    fit <- tryCatch(glmm(rounded_correction_anticipated_number_of_periods ~ 1 + (1|Site.Year), data=endpoint_plot_totals, family=quasipoisson()), error=function(e) NULL)
    sink(file.path("Outputs", paste(endpoints[i], "_glm_between_location_year_variation.txt", sep=""), fsep = "\\"))
    if (is.null(fit)) {
      print("GLMM fit for extracting the between location/year variation failed")
      cv_between_blocks <- NA
    } else {
      fit_summary <- summary(fit)
      dispersion <- fit_summary$dispersion
      cv_between_blocks <- 100 * sqrt(endpoint_mean * dispersion) / endpoint_mean
      print(summary(fit))
      print(cv_between_blocks)
    }
    sink()
    
    endpoints_amiga$EndpointID[i] <- gsub("[.]","",endpoints[i])
    endpoints_amiga$Mean[i] <-  signif(endpoint_mean, digits = 4)
    endpoints_amiga$CV_naive[i] <- signif(endpoint_cv, digits = 4)
    endpoints_amiga$CV_within_blocks[i] <- signif(cv_within_blocks, digits = 4)
    endpoints_amiga$cv_between_blocks[i] <- signif(cv_between_blocks, digits = 4)
    endpoints_amiga$CV[i] <- endpoints_amiga$CV_within_blocks[i]
    
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

endpoints_amiga <- AMIGA_extract_endpoints_data_models(endpoints, 10)
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

endpoints_amiga <- AMIGA_extract_endpoints_data_models(selected_endpoints, 10)
write.csv(na.omit(endpoints_amiga), file = file.path("Selection_AMIGA_endpoints.csv", fsep = "\\"), row.names=FALSE)
write.csv(subset(endpoints_amiga, is.na(endpoints_amiga$CV)), file = file.path("Selection_AMIGA_endpoints_failed.csv", fsep = "\\"), row.names=FALSE)
