# Read data and redefine factors
data <- readDataFrame(globalSettings, endpoints)
head(data, n=20)

# read contrast file
contrast <- read.csv(globalSettings$contrastFile)

# loop over endpoints
for (endpoint in endpoints) {
  print(endpoint$name)
}
q()

# loop over different endpoints
nEndpoint <- length(elemEndpoint)
dataNames <- names(data)
for (iEndpoint in endpoint) {
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
