# Read data; redefine factors and define endpoints 
data <- read.csv(file.path(dir,dataFile))
data <- data[, !(names(data) %in% "FrequencyReplicate")]
nFactors <- length(factors)+2
elemFactors <- seq(1, nFactors)
elemEndpoint <- seq(nFactors+1, ncol(data))
for (i in elemFactors) {
  data[,i] <- as.factor(data[,i])
}

# read Contrast file; create extra columns in dataFrame; create modelTerms
contrast <- read.csv(file.path(dir,templateContrastFile))
contrastPlot <- as.factor(seq(1:length(contrast[[1]])))

# loop over different endpoints
nEndpoint <- length(elemEndpoint)
dataNames <- names(data)
for (i in 1:nEndpoint) {
  # Define current settings
  settings <- list(
    Design=design, 
    Factors=factors, 
    EndPoint=dataNames[elemEndpoint[i]],
    TestType=testType[i], 
    TransformLocLower=log(locLower[i]),
    TransformLocUpper=log(locUpper[i]), 
    ModifierModel=modifierModel[i]
  )
  # Add extra columns to dataframe and create models to fit
  newData <- createDataFrame(data, contrast[[nFactors-2+i]], settings)
  models <- createModels(newData, settings)

  # Check that the alternative model H1 is correct
  if (TRUE) {
    meanData <- read.csv(file.path(dir,"0-Input.csv"))
    checkFitModel(newData, settings, models, meanData) 
  }

  newData[[settings$EndPoint]] <- rpois(length(newData[["Plot"]]), 10)
  pvalues <- OPmodel(newData, settings, models)
  print(pvalues)

}
