# Read data and contrast file 
data <- readDataFile(settings, endpoints)
contrasts <- readContrastFile(settings, endpoints)

# loop over endpoints
prModel = TRUE
diffAnalysis <- list()
equiAnalysis <- list()

for (endpoint in endpoints) {
  if (is.null(endpoint)) next
  name <- endpoint$name
  printCaption(paste0("Analysis of endpoint ", name), "=", 0)
  newData <- createDataFrame(data, contrasts, settings, endpoint)
  models <- createModels(newData, settings, endpoint)

  # Fit model for Difference analysis
  if (endpoint$diffAnalysis == "LogNormal") {
    diffAnalysis[[name]] <- logNormalAnalysis(newData, settings, endpoint, models, prModel)
  } else if (endpoint$diffAnalysis == "SquareRoot") {
    diffAnalysis[[name]] <- squareRootAnalysis(newData, settings, endpoint, models, prModel)
  } else if (endpoint$diffAnalysis == "OverdispersedPoisson") {
    diffAnalysis[[name]] <- overdispersedPoissonAnalysis(newData, settings, endpoint, models, prModel)
  } else if (endpoint$diffAnalysis == "NegativeBinomial") {
    diffAnalysis[[name]] <- negativeBinomialAnalysis(newData, settings, endpoint, models, prModel)
  } else if (endpoint$diffAnalysis == "LogPlusM") {
    diffAnalysis[[name]] <- logPlusMAnalysis(newData, settings, endpoint, models, prModel)
  } else if (endpoint$diffAnalysis == "Gamma") {
    diffAnalysis[[name]] <- gammaAnalysis(newData, settings, endpoint, models, prModel)
  } else if (endpoint$diffAnalysis == "Normal") {
    diffAnalysis[[name]] <- normalAnalysis(newData, settings, endpoint, models, prModel)
  }

  # Fit model for Equivalence analysis when analysis method is different
  if (endpoint$diffAnalysis == endpoint$equiAnalysis) {
    equiAnalysis[[name]] <- diffAnalysis[[name]] 
  } else if (endpoint$equiAnalysis == "LogNormal") {
    equiAnalysis[[name]] <- logNormalAnalysis(newData, settings, endpoint, models, prModel)
  } else if (endpoint$equiAnalysis == "SquareRoot") {
    equiAnalysis[[name]] <- squareRootAnalysis(newData, settings, endpoint, models, prModel)
  } else if (endpoint$equiAnalysis == "OverdispersedPoisson") {
    equiAnalysis[[name]] <- overdispersedPoissonAnalysis(newData, settings, endpoint, models, prModel)
  } else if (endpoint$equiAnalysis == "NegativeBinomial") {
    equiAnalysis[[name]] <- negativeBinomialAnalysis(newData, settings, endpoint, models, prModel)
  } else if (endpoint$equiAnalysis == "LogPlusM") {
    equiAnalysis[[name]] <- logPlusMAnalysis(newData, settings, endpoint, models, prModel)
  } else if (endpoint$equiAnalysis == "Gamma") {
    equiAnalysis[[name]] <- gammaAnalysis(newData, settings, endpoint, models, prModel)
  } else if (endpoint$equiAnalysis == "Normal") {
    equiAnalysis[[name]] <- normalAnalysis(newData, settings, endpoint, models, prModel)
  }
  printResults(endpoint, diffAnalysis[[name]], equiAnalysis[[name]])
}

printSummary(endpoints, settings, diffAnalysis, equiAnalysis)
plotSummary(endpoints, settings, diffAnalysis, equiAnalysis, "Plot")
