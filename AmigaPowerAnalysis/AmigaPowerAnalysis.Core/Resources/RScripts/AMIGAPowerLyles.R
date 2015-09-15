# 1. Return signed non-centrality parameters for equivalence test. 
#    This is required by bivariate non-central t distribution.
# 2. Fitting the NB distribution with fixed theta gives wrong standard errors.
#    Standard errors need to be divided by the squared root of the dispersion parameter

######################################################################################################################
createSyntheticData <- function(simulatedData, settings, simulationSettings, multiplyWeight) {
  require(reshape)  # for untable()
  nrows <- nrow(simulatedData)
  nsamplerow <- rep(NaN,nrows)
  Response <- c()
  Weight   <- c()
  for (i in 1:nrows) {
    # Simulate possibble outcomes and their probabilities
    simulate <- ropoisson(settings$NumberOfSimulationsLylesMethod, simulatedData[["Effect"]][i], simulationSettings$dispersion, settings$PowerLawPower, settings$Distribution)
    table <- as.data.frame(table(simulate))
    nsamplerow[i] <- nrow(table)
    Response <- c(Response, as.numeric(levels(table[["simulate"]])))
    Weight   <- c(Weight, table[["Freq"]]/settings$NumberOfSimulationsLylesMethod)
  }
  # Stack Design matrix and append Response and Weight
  syntheticData <- untable(simulatedData, num=nsamplerow)  
  syntheticData[["Response"]] <- Response
  syntheticData[["Weight"]]   <- multiplyWeight*Weight
  return(syntheticData)
}

######################################################################################################################
logNormalLyles <- function(data, settings, modelSettings, nreps, multiplyWeight) {

  # Transform data (this is done locally)
  data[["Response"]] = log(data[["Response"]]+1)
  locLower <- log(settings$LocLower + 1)
  locUpper <- log(settings$LocUpper + 1)
  data[["LowOffset"]][ data[["LowOffset"]]==settings$TransformLocLower ] = locLower
  data[["UppOffset"]][ data[["UppOffset"]]==settings$TransformLocUpper ] = locUpper
  # Estimate dispersion parameter by mean of Variance
  Means <- tapply(data[["Weight"]] * data[["Response"]], data[["Row"]], sum)/multiplyWeight
  expanded2 <- (data[["Response"]] - Means[data[["Row"]]])^2
  Variances <- tapply(data[["Weight"]] * expanded2, data[["Row"]], sum)/multiplyWeight
  estDispersion <- mean(Variances)

  # Fit full model and determine degrees of freedom
  glmH1 <- lm(as.formula(modelSettings$formulaH1), data=data, weight=Weight)
  sigma1 <- summary(glmH1)$sigma
  dfModel = nrow(data) - glmH1$df.residual
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    df = nreps*modelSettings$ndata - dfModel
  } else {
    df = nreps*(modelSettings$ndata-1) - dfModel + 1
  }

  # Return signed non-centrality parameters for equivalence test;  
  if (settings$UseWaldTest) {  
    # Wald
    estiEffect <- as.numeric(glmH1$coef[2])
    seEffect <- sqrt(vcov(glmH1)[2,2]) / sigma1
    ncDiff <- (abs(estiEffect)/seEffect)^2
    ncEquiLow <- (estiEffect-locLower)/seEffect
    ncEquiUpp <- (estiEffect-locUpper)/seEffect
  } else {
    # LR: fit alternative models
    # Difference test
    glmH0 <- lm(modelSettings$formulaH0, data=data, weight=Weight)
    ncDiff <- glmH0$df.residual*summary(glmH0)$sigma^2 - glmH1$df.residual*summary(glmH1)$sigma^2
    ncDiff[ncDiff<0] = 0
    # Lower equivalence limit 
    glmH0low <- lm(modelSettings$formulaH0_low, data=data, weight=Weight)
    ncEquiLow <- glmH0low$df.residual*summary(glmH0low)$sigma^2 - glmH1$df.residual*summary(glmH1)$sigma^2
    ncEquiLow[ncEquiLow<0] = 0
    ncEquiLow <- sqrt(ncEquiLow)
    # Upper equivalence limit 
    glmH0upp <- lm(modelSettings$formulaH0_upp, data=data, weight=Weight)
    ncEquiUpp <- glmH0upp$df.residual*summary(glmH0upp)$sigma^2 - glmH1$df.residual*summary(glmH1)$sigma^2
    ncEquiUpp[ncEquiUpp<0] = 0
    ncEquiUpp <- -1*sqrt(ncEquiUpp)
  }

  # Scale non-centrality parameters
  scale = nreps/estDispersion/multiplyWeight
  ncDiff = ncDiff * scale
  ncEquiLow = ncEquiLow * sqrt(scale)
  ncEquiUpp = ncEquiUpp * sqrt(scale)
  power = calculatePowerFromNc(ncDiff, ncEquiLow, ncEquiUpp, df, settings$SignificanceLevel)
  return(power)
}

######################################################################################################################
squareRootLyles <- function(data, settings, modelSettings, nreps, multiplyWeight) {

  # Transform data (this is done locally)
  data[["Response"]] = sqrt(data[["Response"]])
  locLower <- sqrt(settings$LocLower)
  locUpper <- sqrt(settings$LocUpper)
  # Estimate dispersion parameter by mean of Variance
  Means <- tapply(data[["Weight"]] * data[["Response"]], data[["Row"]], sum)/multiplyWeight
  expanded2 <- (data[["Response"]] - Means[data[["Row"]]])^2
  Variances <- tapply(data[["Weight"]] * expanded2, data[["Row"]], sum)/multiplyWeight
  estDispersion <- mean(Variances)

  # Fit full model and determine degrees of freedom
  glmH1 <- lm(as.formula(modelSettings$formulaH1), data=data, weight=Weight)
  sigma1 <- summary(glmH1)$sigma
  dfModel = nrow(data) - glmH1$df.residual
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    df = nreps*modelSettings$ndata - dfModel
  } else {
    df = nreps*(modelSettings$ndata-1) - dfModel + 1
  }

  # Return signed non-centrality parameters for equivalence test;  
  if (settings$UseWaldTest) {  
    # Wald
    estiEffect <- as.numeric(glmH1$coef[2])
    seEffect <- sqrt(vcov(glmH1)[2,2]) / sigma1
    ncDiff <- (abs(estiEffect)/seEffect)^2
    ncEquiLow <- (estiEffect-locLower)/seEffect
    ncEquiUpp <- (estiEffect-locUpper)/seEffect
  } else {
    # LR: fit alternative models
    # Difference test
    glmH0 <- lm(modelSettings$formulaH0, data=data, weight=Weight)
    ncDiff <- glmH0$df.residual*summary(glmH0)$sigma^2 - glmH1$df.residual*summary(glmH1)$sigma^2
    ncDiff[ncDiff<0] = 0
    # Lower equivalence limit 
    glmH0low <- lm(modelSettings$formulaH0_low, data=data, weight=Weight)
    ncEquiLow <- glmH0low$df.residual*summary(glmH0low)$sigma^2 - glmH1$df.residual*summary(glmH1)$sigma^2
    ncEquiLow[ncEquiLow<0] = 0
    ncEquiLow <- sqrt(ncEquiLow)
    # Upper equivalence limit 
    glmH0upp <- lm(modelSettings$formulaH0_upp, data=data, weight=Weight)
    ncEquiUpp <- glmH0upp$df.residual*summary(glmH0upp)$sigma^2 - glmH1$df.residual*summary(glmH1)$sigma^2
    ncEquiUpp[ncEquiUpp<0] = 0
    ncEquiUpp <- -1*sqrt(ncEquiUpp)
  }

  # Scale non-centrality parameters
  scale = nreps/estDispersion/multiplyWeight
  ncDiff = ncDiff * scale
  ncEquiLow = ncEquiLow * sqrt(scale)
  ncEquiUpp = ncEquiUpp * sqrt(scale)
  power = calculatePowerFromNc(ncDiff, ncEquiLow, ncEquiUpp, df, settings$SignificanceLevel)
  return(power)
}

######################################################################################################################
overdispersedPoissonLyles <- function(data, settings, modelSettings, nreps, multiplyWeight) {

  # Estimate dispersion parameter by mean of Variance/Mean
  Means <- tapply(data[["Weight"]] * data[["Response"]], data[["Row"]], sum)/multiplyWeight
  expanded2 <- (data[["Response"]] - Means[data[["Row"]]])^2
  Variances <- tapply(data[["Weight"]] * expanded2, data[["Row"]], sum)/multiplyWeight
  estDispersion = mean(Variances/Means)

  # Fit full model and determine degrees of freedom
  glmH1 <- glm(as.formula(modelSettings$formulaH1), family="poisson", data=data, weight=Weight, etastart=TransformedEffect)
  dfModel = nrow(data) - glmH1$df.residual
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    df = nreps*modelSettings$ndata - dfModel
  } else {
    df = nreps*(modelSettings$ndata-1) - dfModel + 1
  }

  # Return signed non-centrality parameters for equivalence test;  
  if (settings$UseWaldTest) {  
    # Wald
    estiEffect <- as.numeric(glmH1$coef[2])
    seEffect <- sqrt(vcov(glmH1)[2,2]) 
    ncDiff <- (abs(estiEffect)/seEffect)^2
    ncEquiLow <- (estiEffect-settings$TransformLocLower)/seEffect
    ncEquiUpp <- (estiEffect-settings$TransformLocUpper)/seEffect
  } else {
    # LR: fit alternative models
    data[["Lp"]] <- glmH1$linear.predictor
    # Difference test
    glmH0 <- glm(modelSettings$formulaH0, family="poisson", data=data, weight=Weight, etastart=Lp)
    ncDiff <- deviance(glmH0) - deviance(glmH1)
    ncDiff[ncDiff<0] = 0
    # Lower equivalence limit 
    glmH0low <- glm(modelSettings$formulaH0_low, family="poisson", data=data, weight=Weight, etastart=Lp)
    ncEquiLow <- deviance(glmH0low) - deviance(glmH1)
    ncEquiLow[ncEquiLow<0] = 0
    ncEquiLow <- sqrt(ncEquiLow)
    # Upper equivalence limit 
    glmH0upp <- glm(modelSettings$formulaH0_upp, family="poisson", data=data, weight=Weight, etastart=Lp)
    ncEquiUpp <- deviance(glmH0upp) - deviance(glmH1)
    ncEquiUpp[ncEquiUpp<0] = 0
    ncEquiUpp <- -1*sqrt(ncEquiUpp)
  }

  # Scale non-centrality parameters
  scale = nreps/estDispersion/multiplyWeight
  ncDiff = ncDiff * scale
  ncEquiLow = ncEquiLow * sqrt(scale)
  ncEquiUpp = ncEquiUpp * sqrt(scale)
  power = calculatePowerFromNc(ncDiff, ncEquiLow, ncEquiUpp, df, settings$SignificanceLevel)
  return(power)
}

######################################################################################################################
negativeBinomialLyles <- function(data, settings, modelSettings, nreps, multiplyWeight) {

  # Estimate dispersion parameter by mean of Variance/Mean
  Means <- tapply(data[["Weight"]] * data[["Response"]], data[["Row"]], sum)/multiplyWeight
  expanded2 <- (data[["Response"]] - Means[data[["Row"]]])^2
  Variances <- tapply(data[["Weight"]] * expanded2, data[["Row"]], sum)/multiplyWeight
  estDispersion = mean(Means*Means/(Variances-Means))

  # Fit full model and determine degrees of freedom
  family <- negative.binomial(theta = estDispersion, link="log")
  glmH1 <- glm(as.formula(modelSettings$formulaH1), family=family, data=data, weight=Weight, etastart=TransformedEffect)
  dfModel = nrow(data) - glmH1$df.residual
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    df = nreps*modelSettings$ndata - dfModel
  } else {
    df = nreps*(modelSettings$ndata-1) - dfModel + 1
  }

  # Return signed non-centrality parameters for equivalence test;
  # Note that the standard error must be corrected               
  if (settings$UseWaldTest) {  
    # Wald
    estDispersion <- summary(glmH1)$dispersion
    estiEffect <- as.numeric(glmH1$coef[2])
    seEffect <- sqrt(vcov(glmH1)[2,2]/estDispersion) 
    ncDiff <- (abs(estiEffect)/seEffect)^2
    ncEquiLow <- (estiEffect-settings$TransformLocLower)/seEffect
    ncEquiUpp <- (estiEffect-settings$TransformLocUpper)/seEffect
  } else {
    # LR: fit alternative models
    data[["Lp"]] <- glmH1$linear.predictor
    # Difference test
    glmH0 <- glm(modelSettings$formulaH0, family=family, data=data, weight=Weight, etastart=Lp)
    ncDiff <- deviance(glmH0) - deviance(glmH1)
    ncDiff[ncDiff<0] = 0
    # Lower equivalence limit 
    glmH0low <- glm(modelSettings$formulaH0_low, family=family, data=data, weight=Weight, etastart=Lp)
    ncEquiLow = deviance(glmH0low) - deviance(glmH1)
    ncEquiLow[ncEquiLow<0] = 0
    ncEquiLow <- sqrt(ncEquiLow)
    # Upper equivalence limit 
    glmH0upp <- glm(modelSettings$formulaH0_upp, family=family, data=data, weight=Weight, etastart=Lp)
    ncEquiUpp = deviance(glmH0upp) - deviance(glmH1)
    ncEquiUpp[ncEquiUpp<0] = 0
    ncEquiUpp <- -1*sqrt(ncEquiUpp)
  }

  # Scale non-centrality parameters
  scale = nreps/multiplyWeight
  ncDiff = ncDiff * scale
  ncEquiLow = ncEquiLow * sqrt(scale)
  ncEquiUpp = ncEquiUpp * sqrt(scale)
  power = calculatePowerFromNc(ncDiff, ncEquiLow, ncEquiUpp, df, settings$SignificanceLevel)
  return(power)
}

######################################################################################################################
calculatePowerFromNc <- function(ncDiff, ncEquiLow, ncEquiUpp, df, alfa, towsided=TRUE) {
  # Twosided difference test
  critFvalue = qf(1.0-alfa, 1, df)
  powerDiff = pf(critFvalue, 1, df, ncDiff, lower.tail=FALSE)
  # Twosided equivalence test
  critTvalue = qt(1.0-alfa, df)
  corr   = matrix(c(1,1,1,1), ncol=2)
  ntimes = length(critFvalue)
  powerEqui = rep(NA, ntimes)
  for (i in 1:ntimes) {
    tval = critTvalue[i]
    low = c(tval, -Inf)
    upp = c(Inf, -tval)
    delta = c(ncEquiLow[i], ncEquiUpp[i])
    pp = pmvt(low=low, upp=upp, delta=delta, df=df[i], corr=corr)
    powerEqui[i] = pp
  }
  power = as.data.frame(cbind(ncDiff, ncEquiLow, ncEquiUpp, df, powerDiff, powerEqui))
  return(power)

  # Compare with simple product
  ncEquiLow = ncEquiLow^2
  ncEquiUpp = ncEquiUpp^2
  critFvalue = qf(1.0-2*settings$SignificanceLevel, 1, df)
  pEquiLow = pf(critFvalue, 1, df, ncEquiLow, lower.tail=FALSE)
  pEquiUpp = pf(critFvalue, 1, df, ncEquiUpp, lower.tail=FALSE)
  pEquiPrd = pEquiLow*pEquiUpp
  compare = as.data.frame(cbind(df, pEquiLow, pEquiUpp, pEquiPrd, powerEqui))
  print(compare, digits=3)
  return(power)
}

######################################################################################################################
lylesPowerAnalysis <- function(data, settings, modelSettings, blocks, effect, debugSettings) {

  # Define number of reps
  nreps  <- c(6,10,12,14,16,18,20,25)
  nreps  <- rep(2:blocks)

  # Define output structure
  nrow <- length(nreps)
  nanalysis <- length(settings$AnalysisMethods)
  pValues <- list(
    Diff  = matrix(nrow=nrow, ncol=nanalysis, dimnames=list(NULL, settings$AnalysisMethods)),
    Equi  = matrix(nrow=nrow, ncol=nanalysis, dimnames=list(NULL, settings$AnalysisMethods)),
    Extra = matrix(nrow=nrow, ncol=2, dimnames=list(NULL, c("Reps", "Effect")))
  )
  pValues$Extra[,"Reps"] <- nreps
  pValues$Extra[,"Effect"] <- effect

  DEBUG  <- TRUE
  blocks <- 1
  multiplyWeight <- 10

  # Setup simulation settings
  simulationSettings <- createSimulationSettings(settings)

  simulatedData <- createSimulatedData(data, settings, simulationSettings, blocks, effect)
  synData <- createSyntheticData(simulatedData, settings, simulationSettings, multiplyWeight) 
  #print(cbind(synData[["Response"]],synData[["Weight"]]))
  # Fit models
  if (!is.na(match("LogNormal", settings$AnalysisMethods))) {
    if (DEBUG) print(paste("Lyles ", "LN"))
    powerLN <- logNormalLyles(synData, settings, modelSettings, nreps, multiplyWeight)
    print(powerLN, digits=3)
    pValues$Diff[,"LogNormal"] <- powerLN[["powerDiff"]]
    pValues$Equi[,"LogNormal"] <- powerLN[["powerEqui"]]
  }
  if (!is.na(match("SquareRoot", settings$AnalysisMethods))) {
    if (DEBUG) print(paste("Lyles ", "SQ"))
    powerSQ <- squareRootLyles(synData, settings, modelSettings, nreps, multiplyWeight)
    print(powerSQ, digits=3)
    pValues$Diff[,"SquareRoot"] <- powerSQ[["powerDiff"]]
    pValues$Equi[,"SquareRoot"] <- powerSQ[["powerEqui"]]
  }
  if (!is.na(match("OverdispersedPoisson", settings$AnalysisMethods))) {
    if (DEBUG) print(paste("Lyles ", "OP"))
    powerOP <- overdispersedPoissonLyles(synData, settings, modelSettings, nreps, multiplyWeight)
    print(powerOP, digits=3)
    pValues$Diff[,"OverdispersedPoisson"] <- powerOP[["powerDiff"]]
    pValues$Equi[,"OverdispersedPoisson"] <- powerOP[["powerEqui"]]
  }
  if (!is.na(match("NegativeBinomial", settings$AnalysisMethods))) {
    if (DEBUG) print(paste("Lyles ", "NB"))
    powerNB <- negativeBinomialLyles(synData, settings, modelSettings, nreps, multiplyWeight)
    print(powerNB, digits=3)
    pValues$Diff[,"NegativeBinomial"] <- powerNB[["powerDiff"]]
    pValues$Equi[,"NegativeBinomial"] <- powerNB[["powerEqui"]]
  }
  return(pValues)
}

