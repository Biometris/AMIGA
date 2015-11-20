library(ggplot2)
library(xlsx)
library(plyr)
library(data.table)
library(gridExtra)
library(grid)

stdErr <- function(x) {sd(x)/ sqrt(length(x))}

# Settings
anticipated_number_of_periods <- 10

# Read data file
data <- read.xlsx("ExtractAMIGA.xlsx", sheetName="Pitfall traps", startRow=2)
data[, 'Year'] <- as.factor(data[, 'Year'])

# Select columns
endpoints <- c(
    "Sap.beetles",
    "Ants",
    "General.collembola.all.families",
    "Springtails.entomobryids",
    "Crickets",
    "Spiders",
    "Centipedes",
    "Wolf.spiders",
    "Ground.beetles"
)
endpoints <- colnames(data)[-(1:10)]

le <- length(endpoints)
endpoints_amiga <- data.frame(EndpointID= character(le),stringsAsFactors=FALSE)
endpoints_amiga$MeasurementType <- "COUNT"
endpoints_amiga$LocLower <- 0.5
endpoints_amiga$LocUpper <- 2.0
endpoints_amiga$DistributionType <- "OverdispersedPoisson"
endpoints_amiga$Mean <- NaN
endpoints_amiga$CV <- NaN

# Initialize data frame for plot totals
data_plot_totals <- data.table(data)

# Filter records with DAP < 0
data_plot_totals <- data_plot_totals[data_plot_totals$DAP > 0,]

# Multiply by number of traps
data_plot_totals[, (endpoints) := lapply(.SD, function(x) x * data_plot_totals[['Traps']]), .SDcols = endpoints]

for (i in 1:length(endpoints)) {
  print(endpoints[i])
  
  # Combine counts over all counting days
  endpoint_plot_totals <- ddply(
    data_plot_totals,
    c("Site","Year","Replicate"),
    .fun = function(x, col) {
      c(
        plot_total = sum(x[[col]]),
        plot_periods = length(x[[col]]),        
        period_average = sum(x[[col]]) / length(x[[col]]),
        correction_anticipated_number_of_periods = anticipated_number_of_periods * sum(x[[col]]) / length(x[[col]])
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
  endpoints_amiga$EndpointID[i] <- gsub("[.]","",endpoints[i])
  endpoints_amiga$Mean[i] <- endpoint_mean
  endpoints_amiga$CV[i] <- endpoint_cv
  
  write.csv(endpoint_plot_totals, file = file.path("Outputs", paste(endpoints[i], "_plot_totals.csv", sep=""), fsep = "\\")) 
  write.csv(endpoint_summary_per_site_location, file = file.path("Outputs", paste(endpoints[i], "_summary.csv", sep=""), fsep = "\\")) 
}
write.csv(endpoints_amiga, file = file.path("Outputs", "AMIGA_endpoints.csv", fsep = "\\"))

View(endpoints_amiga)
View(data_plot_totals)
View(data)
