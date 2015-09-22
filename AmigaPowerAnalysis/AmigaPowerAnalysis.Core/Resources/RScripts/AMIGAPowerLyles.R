# 1. Return signed non-centrality parameters for equivalence test. 
#    This is required by bivariate non-central t distribution.
# 2. Fitting the NB distribution with fixed theta gives wrong standard errors.
#    Standard errors need to be divided by the squared root of the dispersion parameter

######################################################################################################################
createSyntheticDataOld <- function(simulatedData, settings, simulationSettings, multiplyWeight) {
  require(reshape)  # for untable()
  nrows <- nrow(simulatedData)

  # The number of random draws depends on the size of the dataset (=nrows)
  maxTotalDraws <- 4000000
  maxNdraws <- settings$NumberOfSimulationsLylesMethod
  minNdraws <- 10000
  nRandomDraws = ceiling(maxTotalDraws/nrows)
  if (nRandomDraws > maxNdraws) {
    nRandomDraws = maxNdraws
  } else if (nRandomDraws < minNdraws) {
    nRandomDraws = minNdraws
  }

  # Sample for all rows
  nsamplerow <- rep(NaN,nrows)
  Response <- c()
  Weight   <- c()
  for (i in 1:nrows) {
    # Simulate possibble outcomes and their probabilities
    simulate <- ropoisson(nRandomDraws, simulatedData[["Effect"]][i], simulationSettings$dispersion, settings$PowerLawPower, settings$Distribution, settings$ExcessZeroesPercentage)
    table <- as.data.frame(table(simulate))
    nsamplerow[i] <- nrow(table)
    Response <- c(Response, as.numeric(levels(table[["simulate"]])))
    Weight   <- c(Weight, table[["Freq"]]/nRandomDraws)
  }
  # Stack Design matrix and append Response and Weight
  syntheticData <- untable(simulatedData, num=nsamplerow)  
  syntheticData[["Response"]] <- Response
  syntheticData[["Weight"]]   <- multiplyWeight*Weight
  return(syntheticData)
}

######################################################################################################################
createSyntheticData <- function(simulatedData, settings, simulationSettings, multiplyWeight) {
  require(reshape)  # for untable()
  nrows <- nrow(simulatedData)

  # Obtain the unique values of the Means for which to simulate
  uniqueMu <- unique(simulatedData[["Effect"]])
  nUniqueMu = length(uniqueMu)
  
  # The number of random draws depends on the size of the dataset (=nUnique)
  maxTotalDraws <- 4000000
  maxNdraws <- settings$NumberOfSimulationsLylesMethod
  minNdraws <- 10000
  nRandomDraws = ceiling(maxTotalDraws/nUniqueMu)
  if (nRandomDraws > maxNdraws) {
    nRandomDraws = maxNdraws
  } else if (nRandomDraws < minNdraws) {
    nRandomDraws = minNdraws
  }

  # Sample for these unique values
  nsamplerow <- rep(NaN,nUniqueMu)
  ResponseTmp <- c()
  WeightTmp   <- c()
  for (i in 1:nUniqueMu) {
    simulate <- ropoisson(nRandomDraws, uniqueMu[i], simulationSettings$dispersion, settings$PowerLawPower, settings$Distribution, settings$ExcessZeroesPercentage)
    table <- as.data.frame(table(simulate))
    nsamplerow[i] <- nrow(table)
    ResponseTmp <- c(ResponseTmp, as.numeric(levels(table[["simulate"]])))
    WeightTmp   <- c(WeightTmp, table[["Freq"]]/nRandomDraws)
  }
  UnitTmp <- rep(c(1:nUniqueMu), nsamplerow)

  # Combine using the correct position
  matchMu <- match(simulatedData[["Effect"]], uniqueMu)
  Response <- c()
  Weight   <- c()
  for (mm in matchMu) {
    Response <- c(Response, ResponseTmp[UnitTmp==mm])
    Weight   <- c(Weight,   WeightTmp[UnitTmp==mm])
  }
  nsamplerow <- nsamplerow[matchMu]
     
  # Stack Design matrix and append Response and Weight
  syntheticData <- untable(simulatedData, num=nsamplerow)
  syntheticData[["Response"]] <- Response
  syntheticData[["Weight"]]   <- multiplyWeight*Weight
  row.names(syntheticData) <- paste0(rep(row.names(simulatedData), nsamplerow), "-", Response)
  return(syntheticData)
}

######################################################################################################################
normalLyles <- function(data, settings, modelSettings, nreps, effect, multiplyWeight, doDiff, doEqui) {

  # Define non-centrality parameters
  ncDiff    = NaN
  ncEquiLow = NaN
  ncEquiUpp = NaN

  # Estimate dispersion parameter by mean of Variance
  Means <- tapply(data[["Weight"]] * data[["Response"]], data[["Row"]], sum)/multiplyWeight
  expanded2 <- (data[["Response"]] - Means[data[["Row"]]])^2
  Variances <- tapply(data[["Weight"]] * expanded2, data[["Row"]], sum)/multiplyWeight
  estDispersion <- mean(Variances)

  # Fit full model and determine degrees of freedom
  glmH1 <- lm(as.formula(modelSettings$formulaH1), data=data, weight=Weight)
  sigma1 <- summary(glmH1)$sigma
  modelDf = nrow(data) - glmH1$df.residual
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    df = nreps*modelSettings$ndata - modelDf
  } else {
    df = nreps*(modelSettings$ndata-1) - modelDf + modelSettings$blocks 
  }

  # Return signed non-centrality parameters for equivalence test;  
  if (settings$UseWaldTest) {  
    # Wald
    estiEffect <- as.numeric(glmH1$coef[2])
    seEffect <- sqrt(vcov(glmH1)[2,2]) / sigma1
    if (doDiff) {
      ncDiff <- (abs(estiEffect)/seEffect)^2
    }
    if ((doEqui) && (FALSE)) {
      ncEquiLow <- (estiEffect-settings$LocLower)/seEffect
      ncEquiUpp <- (estiEffect-settings$LocUpper)/seEffect
    }
  } else {
    # LR: fit alternative models
    # Difference test
    if (doDiff) {
      glmH0 <- lm(modelSettings$formulaH0, data=data, weight=Weight)
      ncDiff <- glmH0$df.residual*summary(glmH0)$sigma^2 - glmH1$df.residual*summary(glmH1)$sigma^2
      ncDiff[ncDiff<0] = 0
    }
    if ((doEqui) && (FALSE)) {
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
  }

  # Scale non-centrality parameters
  scale = nreps/estDispersion/multiplyWeight/modelSettings$blocks
  ncDiff = ncDiff * scale
  ncEquiLow = ncEquiLow * sqrt(scale)
  ncEquiUpp = ncEquiUpp * sqrt(scale)
  power = calculatePowerFromNc(nreps, effect, df, ncDiff, ncEquiLow, ncEquiUpp, settings$SignificanceLevel, 
      doDiff, doEqui)
  return(power)
}

######################################################################################################################
logNormalLyles <- function(data, settings, modelSettings, nreps, effect, multiplyWeight) {

  # Transform data (this is done locally)
  data[["Response"]] = log(data[["Response"]]+1)
  settings$TransformLocLower <- log(settings$LocLower + 1)
  settings$TransformLocUpper <- log(settings$LocUpper + 1)
  data[["LowOffset"]][ data[["LowOffset"]]==settings$TransformLocLower ] = settings$TransformLocLower
  data[["UppOffset"]][ data[["UppOffset"]]==settings$TransformLocUpper ] = settings$TransformLocUpper
  return(normalLyles(data, settings, modelSettings, nreps, effect, multiplyWeight, 
      settings$DoLNdiff, settings$DoLNequi))
}

######################################################################################################################
squareRootLyles <- function(data, settings, modelSettings, nreps, effect, multiplyWeight) {

  # Transform data (this is done locally)
  data[["Response"]] = sqrt(data[["Response"]])
  settings$TransformLocLower <- sqrt(settings$LocLower)
  settings$TransformLocUpper <- sqrt(settings$LocUpper)
  data[["LowOffset"]][ data[["LowOffset"]]==settings$TransformLocLower ] = settings$TransformLocLower
  data[["UppOffset"]][ data[["UppOffset"]]==settings$TransformLocUpper ] = settings$TransformLocUpper
  return(normalLyles(data, settings, modelSettings, nreps, effect, multiplyWeight, 
      settings$DoSQdiff, settings$DoSQequi))
}

######################################################################################################################
overdispersedPoissonLyles <- function(data, settings, modelSettings, nreps, effect, multiplyWeight) {

  # Define non-centrality parameters
  ncDiff    = NaN
  ncEquiLow = NaN
  ncEquiUpp = NaN

  # Estimate dispersion parameter by mean of Variance/Mean
  Means <- tapply(data[["Weight"]] * data[["Response"]], data[["Row"]], sum)/multiplyWeight
  expanded2 <- (data[["Response"]] - Means[data[["Row"]]])^2
  Variances <- tapply(data[["Weight"]] * expanded2, data[["Row"]], sum)/multiplyWeight
  estDispersion = mean(Variances/Means)

  # Fit full model and determine degrees of freedom
  family <- "poisson"
  glmH1 <- glm(as.formula(modelSettings$formulaH1), family=family, data=data, weight=Weight, etastart=Lp)
  modelDf = nrow(data) - glmH1$df.residual
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    df = nreps*modelSettings$ndata - modelDf
  } else {
    df = nreps*(modelSettings$ndata-1) - modelDf + modelSettings$blocks 
  }

  # Return signed non-centrality parameters for equivalence test;  
  if (settings$UseWaldTest) {  
    # Wald
    estiEffect <- as.numeric(glmH1$coef[2])
    seEffect <- sqrt(vcov(glmH1)[2,2]) 
    if (settings$DoOPdiff) {
      ncDiff <- (abs(estiEffect)/seEffect)^2
    }
    if (settings$DoOPequi) {
      ncEquiLow <- (estiEffect-settings$TransformLocLower)/seEffect
      ncEquiUpp <- (estiEffect-settings$TransformLocUpper)/seEffect
    }
    if (FALSE) {
      cat(paste0("estiEffect: ", estiEffect, "\n"))
      cat(paste0("estiEffect: ", estiEffect, "\n"))
      cat(paste0("seEffect: ", seEffect, "\n"))
      cat(paste0("ncDiff: ", ncDiff, "\n"))
      cat(paste0("ncEquiLow: ", ncEquiLow, "\n"))
      cat(paste0("ncEquiUpp: ", ncEquiUpp, "\n"))
    }
  } else {
    # LR: fit alternative models
    data[["Lp"]] <- glmH1$linear.predictor
    if (settings$DoOPdiff) {
      # Difference test
      glmH0 <- glm(modelSettings$formulaH0, family=family, data=data, weight=Weight, etastart=Lp)
      ncDiff <- deviance(glmH0) - deviance(glmH1)
      ncDiff[ncDiff<0] = 0
    }
    if (settings$DoOPequi) {
      # Lower equivalence limit 
      glmH0low <- glm(modelSettings$formulaH0_low, family=family, data=data, weight=Weight, etastart=Lp)
      ncEquiLow <- deviance(glmH0low) - deviance(glmH1)
      ncEquiLow[ncEquiLow<0] = 0
      ncEquiLow <- sqrt(ncEquiLow)
      # Upper equivalence limit 
      glmH0upp <- glm(modelSettings$formulaH0_upp, family=family, data=data, weight=Weight, etastart=Lp)
      ncEquiUpp <- deviance(glmH0upp) - deviance(glmH1)
      ncEquiUpp[ncEquiUpp<0] = 0
      ncEquiUpp <- -1*sqrt(ncEquiUpp)
    }
  }

  # Scale non-centrality parameters
  scale = nreps/estDispersion/multiplyWeight/modelSettings$blocks
  ncDiff = ncDiff * scale
  ncEquiLow = ncEquiLow * sqrt(scale)
  ncEquiUpp = ncEquiUpp * sqrt(scale)
  power = calculatePowerFromNc(nreps, effect, df, ncDiff, ncEquiLow, ncEquiUpp, settings$SignificanceLevel, 
        settings$DoOPdiff, settings$DoOPequi)
  return(power)
}

######################################################################################################################
negativeBinomialLyles <- function(data, settings, modelSettings, nreps, effect, multiplyWeight) {

  # Define non-centrality parameters
  ncDiff    = NaN
  ncEquiLow = NaN
  ncEquiUpp = NaN

  # Estimate dispersion parameter by mean of Variance/Mean
  Means <- tapply(data[["Weight"]] * data[["Response"]], data[["Row"]], sum)/multiplyWeight
  expanded2 <- (data[["Response"]] - Means[data[["Row"]]])^2
  Variances <- tapply(data[["Weight"]] * expanded2, data[["Row"]], sum)/multiplyWeight
  estDispersion = mean(Means*Means/(Variances-Means))

  # Fit full model and determine degrees of freedom
  family <- negative.binomial(theta = estDispersion, link="log")
  glmH1 <- glm(as.formula(modelSettings$formulaH1), family=family, data=data, weight=Weight, etastart=Lp)
  modelDf = nrow(data) - glmH1$df.residual
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    df = nreps*modelSettings$ndata - modelDf
  } else {
    df = nreps*(modelSettings$ndata-1) - modelDf + modelSettings$blocks 
  }

  # Return signed non-centrality parameters for equivalence test;
  # Note that the standard error must be corrected               
  if (settings$UseWaldTest) {  
    # Wald
    correction <- summary(glmH1)$dispersion
    estiEffect <- as.numeric(glmH1$coef[2])
    seEffect <- sqrt(vcov(glmH1)[2,2]/correction) 
    if (settings$DoNBdiff) {
      ncDiff <- (abs(estiEffect)/seEffect)^2
    }
    if (settings$DoNBequi) {
      ncEquiLow <- (estiEffect-settings$TransformLocLower)/seEffect
      ncEquiUpp <- (estiEffect-settings$TransformLocUpper)/seEffect
    }
  } else {
    # LR: fit alternative models
    data[["Lp"]] <- glmH1$linear.predictor
    if (settings$DoNBdiff) {
      # Difference test
      glmH0 <- glm(modelSettings$formulaH0, family=family, data=data, weight=Weight, etastart=Lp)
      ncDiff <- deviance(glmH0) - deviance(glmH1)
      ncDiff[ncDiff<0] = 0
    }
    if (settings$DoNBequi) {
      # Lower equivalence limit 
      glmH0low <- glm(modelSettings$formulaH0_low, family=family, data=data, weight=Weight, etastart=Lp)
      ncEquiLow <- deviance(glmH0low) - deviance(glmH1)
      ncEquiLow[ncEquiLow<0] = 0
      ncEquiLow <- sqrt(ncEquiLow)
      # Upper equivalence limit 
      glmH0upp <- glm(modelSettings$formulaH0_upp, family=family, data=data, weight=Weight, etastart=Lp)
      ncEquiUpp <- deviance(glmH0upp) - deviance(glmH1)
      ncEquiUpp[ncEquiUpp<0] = 0
      ncEquiUpp <- -1*sqrt(ncEquiUpp)
    }
  }

  # Scale non-centrality parameters
  scale = nreps/multiplyWeight/modelSettings$blocks
  ncDiff = ncDiff * scale
  ncEquiLow = ncEquiLow * sqrt(scale)
  ncEquiUpp = ncEquiUpp * sqrt(scale)
  power = calculatePowerFromNc(nreps, effect, df, ncDiff, ncEquiLow, ncEquiUpp, settings$SignificanceLevel, 
        settings$DoNBdiff, settings$DoNBequi)
  return(power)
}

######################################################################################################################
calculatePowerFromNc <- function(nreps, effect, df, ncDiff, ncEquiLow, ncEquiUpp, alfa, doDiff, doEqui, twosided=TRUE) {
  # Twosided difference test
  if (doDiff) {
    critFvalue = qf(1.0-alfa, 1, df)
    powerDiff = pf(critFvalue, 1, df, ncDiff, lower.tail=FALSE)
  } else {
    powerDiff = NaN * nreps
  }
  # Twosided equivalence test
  if (doEqui) {
    critTvalue = qt(1.0-alfa, df)
    corr   = matrix(c(1,1,1,1), ncol=2)
    ntimes = length(nreps)
    powerEqui = rep(NA, ntimes)
    for (i in 1:ntimes) {
      if ((is.na(ncEquiLow[i])) | (is.na(ncEquiUpp[i]))) {
        powerEqui[i] = NaN
      } else {
        tval = critTvalue[i]
        low = c(tval, -Inf)
        upp = c(Inf, -tval)
        delta = c(ncEquiLow[i], ncEquiUpp[i])
        pp = pmvt(low=low, upp=upp, delta=delta, df=df[i], corr=corr)
        powerEqui[i] = pp
      }
    }
  } else {
    powerEqui = NaN * nreps
  }
  power = as.data.frame(cbind(effect, nreps, df, ncDiff, ncEquiLow, ncEquiUpp, powerDiff, powerEqui))
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

  DEBUG  <- TRUE

  # Prepare for debugging, i.e. create directory to write files to
  if (settings$IsOutputSimulatedData) {
    localDir = paste0(settings$directory)
    localDir = paste0(localDir, settings$ComparisonId, "-Endpoint", "/")
    dir.create(localDir, showWarnings=FALSE)
    #localDir = paste0(localDir, "Rep", str_pad(debugSettings$iRep, 2, side="left", "0"), "/")
    #dir.create(localDir, showWarnings=FALSE)
    localDir = paste0(localDir, "Effect", str_pad(debugSettings$iEffect, 2, side="left", "0"), "/")
    dir.create(localDir, showWarnings=FALSE)
    #unlink(paste0(localDir, "*.csv"))
    debugSettings$displayFile = file(paste0(localDir, "00-DisplayFit.txt"), open="wt")
  } 

  # Define number of reps depending on the design
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    nreps <- rep(2:blocks)
    blocks <- 1
  } else if (settings$CVBlocks == 0.0) {
    nreps <- rep(2:blocks)
    blocks <- 2
  } else {
    nreps <- blocks
  }
  modelSettings$blocks <- blocks

  # Define output structure
  nrow <- length(nreps)
  nDiffTests <- length(settings$AnalysisMethodsDifferenceTests)
  nEquiTests <- length(settings$AnalysisMethodsEquivalenceTests)
  pValues <- list(
    Diff  = matrix(nrow=nrow, ncol=nDiffTests, dimnames=list(NULL, settings$AnalysisMethodsDifferenceTests)),
    Equi  = matrix(nrow=nrow, ncol=nEquiTests, dimnames=list(NULL, settings$AnalysisMethodsEquivalenceTests)),
    Extra = matrix(nrow=nrow, ncol=3, dimnames=list(NULL, c("Reps", "Effect", "Df")))
  )
  pValues$Extra[,"Reps"] <- nreps
  pValues$Extra[,"Effect"] <- effect

  # Setup simulation settings
  multiplyWeight <- 10
  simulationSettings <- createSimulationSettings(settings)
  simulatedData <- createSimulatedData(data, settings, simulationSettings, blocks, effect)
  if (FALSE) {
    pos = match(c("Test", "Mean", "Block", "Lp", "Effect"), names(simulatedData))
    print(simulatedData[,pos])
  }
  synData <- createSyntheticData(simulatedData, settings, simulationSettings, multiplyWeight) 
  if (settings$IsOutputSimulatedData) {
    csvFile = paste0(localDir, "simulatedData-", blocks, ".csv")
    write.csv(simulatedData, csvFile, row.names=FALSE)
    csvFile = paste0(localDir, "synData-", blocks, ".csv")
    write.csv(synData, csvFile, row.names=FALSE)
  }

  #synDataOld <- createSyntheticDataNew(simulatedData, settings, simulationSettings, multiplyWeight) 
  # Fit models
  if ((settings$DoLNdiff) || (settings$DoLNequi)) {
    if (DEBUG) cat(paste("\nLyles LN", "---------------------------------------------------------\n"))
    powerLN <- logNormalLyles(synData, settings, modelSettings, nreps, effect, multiplyWeight)
    if (DEBUG) print(powerLN, digits=3)
    if (settings$DoLNdiff) pValues$Diff[,"LogNormal"] <- powerLN[["powerDiff"]]
    if (settings$DoLNequi) pValues$Equi[,"LogNormal"] <- powerLN[["powerEqui"]]
    pValues$Extra[,"Df"] <- powerLN[["df"]]
  }
  if ((settings$DoSQdiff) || (settings$DoSQequi)) {
    if (DEBUG) cat(paste("\nLyles SQ", "---------------------------------------------------------\n"))
    powerSQ <- squareRootLyles(synData, settings, modelSettings, nreps, effect, multiplyWeight)
    if (DEBUG) print(powerSQ, digits=3)
    if (settings$DoSQdiff) pValues$Diff[,"SquareRoot"] <- powerSQ[["powerDiff"]]
    if (settings$DoSQequi) pValues$Equi[,"SquareRoot"] <- powerSQ[["powerEqui"]]
    pValues$Extra[,"Df"] <- powerSQ[["df"]]
  }
  if ((settings$DoOPdiff) || (settings$DoOPequi)) {
    if (DEBUG) cat(paste("\nLyles OP", "---------------------------------------------------------\n"))
    powerOP <- overdispersedPoissonLyles(synData, settings, modelSettings, nreps, effect, multiplyWeight)
    if (DEBUG) print(powerOP, digits=3)
    if (settings$DoOPdiff) pValues$Diff[,"OverdispersedPoisson"] <- powerOP[["powerDiff"]]
    if (settings$DoOPequi) pValues$Equi[,"OverdispersedPoisson"] <- powerOP[["powerEqui"]]
    pValues$Extra[,"Df"] <- powerOP[["df"]]
  }
  if ((settings$DoNBdiff) || (settings$DoNBequi)) {
    if (DEBUG) cat(paste("\nLyles NB", "---------------------------------------------------------\n"))
    powerNB <- negativeBinomialLyles(synData, settings, modelSettings, nreps, effect, multiplyWeight)
    if (DEBUG) print(powerNB, digits=3)
    if (settings$DoNBdiff) pValues$Diff[,"NegativeBinomial"] <- powerNB[["powerDiff"]]
    if (settings$DoNBequi) pValues$Equi[,"NegativeBinomial"] <- powerNB[["powerEqui"]]
    pValues$Extra[,"Df"] <- powerNB[["df"]]
  }
  return(pValues)
}

