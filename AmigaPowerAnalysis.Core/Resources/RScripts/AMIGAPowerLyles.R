# 1. Return signed non-centrality parameters for equivalence test. 
#    This is required by bivariate non-central t distribution.
# 2. Fitting the NB distribution with fixed theta gives wrong standard errors.
#    Standard errors need to be divided by the squared root of the dispersion parameter

######################################################################################################################
createSyntheticDataOld <- function(simulatedData, settings, multiplyWeight) {
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
    simulate <- ropoisson(nRandomDraws, simulatedData[["Effect"]][i], settings$dispersion, settings$PowerLawPower, settings$Distribution, settings$ExcessZeroesPercentage)
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
createSyntheticData <- function(simulatedData, settings, multiplyWeight) {
  require(reshape)  # for untable()
  nrows <- nrow(simulatedData)

  # Obtain the unique values of the Means for which to simulate
  uniqueMu <- unique(simulatedData[["Effect"]])
  nUniqueMu = length(uniqueMu)
  
  # The number of random draws depends on the size of the dataset (=nUnique)
  if (settings$MeasurementType == "Count") {
    maxNdraws <- settings$NumberOfSimulationsLylesMethod
    maxTotalDraws <- 4000000
    minNdraws <- 4000
  } else if (settings$MeasurementType == "Nonnegative") {
    maxNdraws <- ceiling(settings$NumberOfSimulationsLylesMethod/10)
    maxTotalDraws <- 1000000
    minNdraws <- 1000
  } else {
    stop(paste("MeasurementType", settings$MeasurementType, "in function createSyntheticData() is not implemented.", call. = FALSE))
  }
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
    if (settings$MeasurementType == "Count") {
      simulate <- ropoisson(nRandomDraws, uniqueMu[i], settings$dispersion, settings$PowerLawPower, settings$Distribution, settings$ExcessZeroesPercentage)
      table <- as.data.frame(table(simulate))
      nsamplerow[i] <- nrow(table)
      ResponseTmp <- c(ResponseTmp, as.numeric(levels(table[["simulate"]])))
      WeightTmp   <- c(WeightTmp, table[["Freq"]]/nRandomDraws)
    } else if (settings$MeasurementType == "Nonnegative") {
      response <- rnonnegative(nRandomDraws, uniqueMu[i], settings$CVComparator, settings$Distribution)
      nsamplerow[i] <- nRandomDraws
      ResponseTmp <- c(ResponseTmp, response)
      WeightTmp   <- c(WeightTmp, 1/nRandomDraws + 0*response)
    }
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
  if (settings$MeasurementType == "Count") {
    row.names(syntheticData) <- paste0(rep(row.names(simulatedData), nsamplerow), "-", Response)
  }
  #print(syntheticData)
  return(syntheticData)
}

######################################################################################################################
normalLyles <- function(data, settings, modelSettings, nreps, effect, multiplyWeight, doDiff, doEqui) {

  # Define non-centrality parameters
  ncDiff    = NaN
  ncEqui = list(Low=NaN, Upp=NaN)

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
  estiEffect <- as.numeric(glmH1$coef[2])
  seEffect <- sqrt(vcov(glmH1)[2,2]) / sigma1

  # Results based on Wald tests; this is equiovalent to LR
  if (doDiff) {
    ncDiff <- estiEffect/seEffect
  }
  if ((doEqui) && (FALSE)) {
    ncEqui <- waldEquiNoncentral(estiEffect, seEffect, settings$TestType, settings$TransformLocLower, settings$TransformLocUpper)
  }

  # Scale non-centrality parameters
  scale = nreps/estDispersion/multiplyWeight/modelSettings$blocks
  ncDiff = ncDiff * sqrt(scale)
  ncEqui$Low = ncEqui$Low * sqrt(scale)
  ncEqui$Upp = ncEqui$Upp * sqrt(scale)
  power = calculatePowerFromNc(nreps, effect, df, ncDiff, ncEqui, settings, doDiff, doEqui)
  return(power)

  # LR: fit alternative models; not this section is not executed; 
  # It can be used to show that 
  # Difference test
  if (doDiff) {
    glmH0 <- lm(modelSettings$formulaH0, data=data, weight=Weight)
    ncDiff <- glmH0$df.residual*summary(glmH0)$sigma^2 - glmH1$df.residual*summary(glmH1)$sigma^2
    ncDiff[ncDiff<0] = 0
    ncDiff = sign(estiEffect)*sqrt(ncDiff)
  }
  if ((doEqui) && (FALSE)) {
    if ((settings$TestType == "twosided") || (settings$TestType == "left")) {
      # Lower equivalence limit 
      glmH0low <- lm(modelSettings$formulaH0_low, data=data, weight=Weight)
      ncEqui$Low <- glmH0low$df.residual*summary(glmH0low)$sigma^2 - glmH1$df.residual*summary(glmH1)$sigma^2
      ncEqui$Low[ncEqui$Low<0] = 0
      ncEqui$Low <- sqrt(ncEqui$Low)
    } else {
      ncEqui$Low  <- NA
    }
    if ((settings$TestType == "twosided") || (settings$TestType == "right")) {
      # Upper equivalence limit 
      glmH0upp <- lm(modelSettings$formulaH0_upp, data=data, weight=Weight)
      ncEqui$Upp <- glmH0upp$df.residual*summary(glmH0upp)$sigma^2 - glmH1$df.residual*summary(glmH1)$sigma^2
      ncEqui$Upp[ncEqui$Upp<0] = 0
      ncEqui$Upp <- sqrt(ncEqui$Upp)
    } else {
      ncEqui$Upp  <- NA
    }
  }
}

######################################################################################################################
logNormalLyles <- function(data, settings, modelSettings, nreps, effect, multiplyWeight) {

  # Transform data (this is done locally)
  data[["Response"]] = log(data[["Response"]]+1)
  if (!is.na(settings$LocLower)) settings$TransformLocLower <- log(settings$LocLower + 1)
  if (!is.na(settings$LocUpper)) settings$TransformLocUpper <- log(settings$LocUpper + 1)
  data[["LowOffset"]][ data[["LowOffset"]]==settings$TransformLocLower ] = settings$TransformLocLower
  data[["UppOffset"]][ data[["UppOffset"]]==settings$TransformLocUpper ] = settings$TransformLocUpper
  return(normalLyles(data, settings, modelSettings, nreps, effect, multiplyWeight, 
      settings$DoLNdiff, settings$DoLNequi))
}

######################################################################################################################
squareRootLyles <- function(data, settings, modelSettings, nreps, effect, multiplyWeight) {

  # Transform data (this is done locally)
  data[["Response"]] = sqrt(data[["Response"]])
  if (!is.na(settings$LocLower)) settings$TransformLocLower <- sqrt(settings$LocLower)
  if (!is.na(settings$LocUpper)) settings$TransformLocUpper <- sqrt(settings$LocUpper)
  data[["LowOffset"]][ data[["LowOffset"]]==settings$TransformLocLower ] = settings$TransformLocLower
  data[["UppOffset"]][ data[["UppOffset"]]==settings$TransformLocUpper ] = settings$TransformLocUpper
  return(normalLyles(data, settings, modelSettings, nreps, effect, multiplyWeight, 
      settings$DoSQdiff, settings$DoSQequi))
}

######################################################################################################################
overdispersedPoissonLyles <- function(data, settings, modelSettings, nreps, effect, multiplyWeight) {

  # Define non-centrality parameters
  ncDiff    = NaN
  ncEqui = list(Low=NaN, Upp=NaN)

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

  estiEffect <- as.numeric(glmH1$coef[2])
  seEffect <- sqrt(vcov(glmH1)[2,2]) 
  if (settings$UseWaldTest) {  
    # Results based on Wald tests
    if (settings$DoOPdiff) {
      ncDiff = estiEffect/seEffect
    }
    if (settings$DoOPequi) {
      ncEqui <- waldEquiNoncentral(estiEffect, seEffect, settings$TestType, settings$TransformLocLower, settings$TransformLocUpper)
    }
    
    if (FALSE) {
      cat(paste0("estiEffect: ", estiEffect, "\n"))
      cat(paste0("seEffect: ", seEffect, "\n"))
      cat(paste0("ncDiff: ", ncDiff, "\n"))
      cat(paste0("ncEqui$Low: ", ncEqui$Low, "\n"))
      cat(paste0("ncEqui$Upp: ", ncEqui$Upp, "\n"))
    }

  } else {
    # LR: fit alternative models
    data[["Lp"]] <- glmH1$linear.predictor
    if (settings$DoOPdiff) {
      # Difference test
      glmH0 <- glm(modelSettings$formulaH0, family=family, data=data, weight=Weight, etastart=Lp)
      ncDiff <- deviance(glmH0) - deviance(glmH1)
      ncDiff[ncDiff<0] = 0
      ncDiff = sign(estiEffect)*sqrt(ncDiff)
    }
    if (settings$DoOPequi) {
      if ((settings$TestType == "twosided") || (settings$TestType == "left")) {
        # Lower equivalence limit 
        glmH0low <- glm(modelSettings$formulaH0_low, family=family, data=data, weight=Weight, etastart=Lp)
        ncEqui$Low <- deviance(glmH0low) - deviance(glmH1)
        ncEqui$Low[ncEqui$Low<0] = 0
        ncEqui$Low <- sqrt(ncEqui$Low)
      } else {
        ncEqui$Low = NA
      }
      if ((settings$TestType == "twosided") || (settings$TestType == "right")) {
        # Upper equivalence limit 
        glmH0upp <- glm(modelSettings$formulaH0_upp, family=family, data=data, weight=Weight, etastart=Lp)
        ncEqui$Upp <- deviance(glmH0upp) - deviance(glmH1)
        ncEqui$Upp[ncEqui$Upp<0] = 0
        ncEqui$Upp <- sqrt(ncEqui$Upp)
      } else {
        ncEqui$Upp = NA
      }
    }
  }

  # Scale non-centrality parameters
  scale = nreps/estDispersion/multiplyWeight/modelSettings$blocks
  ncDiff = ncDiff * sqrt(scale)
  ncEqui$Low = ncEqui$Low * sqrt(scale)
  ncEqui$Upp = ncEqui$Upp * sqrt(scale)
  power = calculatePowerFromNc(nreps, effect, df, ncDiff, ncEqui, settings, settings$DoOPdiff, settings$DoOPequi)
  return(power)
}

######################################################################################################################
negativeBinomialLyles <- function(data, settings, modelSettings, nreps, effect, multiplyWeight) {

  # Define non-centrality parameters
  ncDiff    = NaN
  ncEqui = list(Low=NaN, Upp=NaN)

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
  # Note that the standard error must be corrected to fix the value of the dispersion 
  correction <- summary(glmH1)$dispersion
  estiEffect <- as.numeric(glmH1$coef[2])
  seEffect <- sqrt(vcov(glmH1)[2,2]/correction) 

  if (settings$UseWaldTest) {  
    # Results based on Wald tests
    if (settings$DoNBdiff) {
      ncDiff <- estiEffect/seEffect
    }
    if (settings$DoNBequi) {
      ncEqui <- waldEquiNoncentral(estiEffect, seEffect, settings$TestType, settings$TransformLocLower, settings$TransformLocUpper)
    }
  } else {
    # LR: fit alternative models
    data[["Lp"]] <- glmH1$linear.predictor
    if (settings$DoNBdiff) {
      # Difference test
      glmH0 <- glm(modelSettings$formulaH0, family=family, data=data, weight=Weight, etastart=Lp)
      ncDiff <- deviance(glmH0) - deviance(glmH1)
      ncDiff[ncDiff<0] = 0
      ncDiff <- sign(estiEffect)*sqrt(ncDiff)
    }
    if (settings$DoNBequi) {
      if ((settings$TestType == "twosided") || (settings$TestType == "left")) {
        # Lower equivalence limit 
        glmH0low <- glm(modelSettings$formulaH0_low, family=family, data=data, weight=Weight, etastart=Lp)
        ncEqui$Low <- deviance(glmH0low) - deviance(glmH1)
        ncEqui$Low[ncEqui$Low<0] = 0
        ncEqui$Low <- sqrt(ncEqui$Low)
      } else {
        ncEqui$Low = NA
      }
      if ((settings$TestType == "twosided") || (settings$TestType == "right")) {
        # Upper equivalence limit 
        glmH0upp <- glm(modelSettings$formulaH0_upp, family=family, data=data, weight=Weight, etastart=Lp)
        ncEqui$Upp <- deviance(glmH0upp) - deviance(glmH1)
        ncEqui$Upp[ncEqui$Upp<0] = 0
        ncEqui$Upp <- sqrt(ncEqui$Upp)
      } else {
        ncEqui$Upp = NA
      }
    }
  }

  # Scale non-centrality parameters
  scale = nreps/multiplyWeight/modelSettings$blocks
  ncDiff = ncDiff * sqrt(scale)
  ncEqui$Low = ncEqui$Low * sqrt(scale)
  ncEqui$Upp = ncEqui$Upp * sqrt(scale)
  power = calculatePowerFromNc(nreps, effect, df, ncDiff, ncEqui, settings, settings$DoNBdiff, settings$DoNBequi)
  return(power)
}

######################################################################################################################
gammaLyles <- function(data, settings, modelSettings, nreps, effect, multiplyWeight) {

  # Define non-centrality parameters
  ncDiff = NaN
  ncEqui = list(Low=NaN, Upp=NaN)

  # Estimate dispersion parameter (which is the constant Coefficient of Variation) by means of sqrt(Variance)/Mean
  Means <- tapply(data[["Weight"]] * data[["Response"]], data[["Row"]], sum)/multiplyWeight
  expanded2 <- (data[["Response"]] - Means[data[["Row"]]])^2
  Variances <- tapply(data[["Weight"]] * expanded2, data[["Row"]], sum)/multiplyWeight
  estDispersion = mean(sqrt(Variances)/Means)

  # Fit full model and determine degrees of freedom
  glmH1 <- glm(as.formula(modelSettings$formulaH1), family=Gamma(link=log), data=data, weight=Weight, etastart=Lp)
  modelDf = nrow(data) - glmH1$df.residual
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    df = nreps*modelSettings$ndata - modelDf
  } else {
    df = nreps*(modelSettings$ndata-1) - modelDf + modelSettings$blocks 
  }

  # Note that the standard error must be corrected to fix the value of the dispersion 
  correction <- summary(glmH1)$dispersion
  estiEffect <- as.numeric(glmH1$coef[2])
  seEffect <- sqrt(vcov(glmH1)[2,2]/correction) 
  if (settings$UseWaldTest) {  
    # Results based on Wald tests
    if (settings$DoGMdiff) {
      ncDiff = estiEffect/seEffect
    }
    if (settings$DoGMequi) {
      ncEqui <- waldEquiNoncentral(estiEffect, seEffect, settings$TestType, settings$TransformLocLower, settings$TransformLocUpper)
    }
    if (FALSE) {
      cat(paste0("estiEffect: ", estiEffect, "\n"))
      cat(paste0("seEffect: ", seEffect, "\n"))
      cat(paste0("ncDiff: ", ncDiff, "\n"))
      cat(paste0("ncEqui$Low: ", ncEqui$Low, "\n"))
      cat(paste0("ncEqui$Upp: ", ncEqui$Upp, "\n"))
    }
    
  } else {
    # LR: fit alternative models
    data[["Lp"]] <- glmH1$linear.predictor
    if (settings$DoGMdiff) {
      # Difference test
      glmH0 <- glm(modelSettings$formulaH0, family=Gamma(link=log), data=data, weight=Weight, etastart=Lp)
      ncDiff <- deviance(glmH0) - deviance(glmH1)
      ncDiff[ncDiff<0] = 0
      ncDiff = sign(estiEffect)*sqrt(ncDiff)
    }
    if (settings$DoGMequi) {
      if ((settings$TestType == "twosided") || (settings$TestType == "left")) {
        # Lower equivalence limit 
        glmH0low <- glm(modelSettings$formulaH0_low, family=Gamma(link=log), data=data, weight=Weight, etastart=Lp)
        ncEqui$Low <- deviance(glmH0low) - deviance(glmH1)
        ncEqui$Low[ncEqui$Low<0] = 0
        ncEqui$Low <- sqrt(ncEqui$Low)
      } else {
        ncEqui$Low = NA
      }
      if ((settings$TestType == "twosided") || (settings$TestType == "right")) {
        # Upper equivalence limit 
        glmH0upp <- glm(modelSettings$formulaH0_upp, family=Gamma(link=log), data=data, weight=Weight, etastart=Lp)
        ncEqui$Upp <- deviance(glmH0upp) - deviance(glmH1)
        ncEqui$Upp[ncEqui$Upp<0] = 0
        ncEqui$Upp <- sqrt(ncEqui$Upp)
      } else {
        ncEqui$Upp = NA
      }
    }
  }

  # Scale non-centrality parameters
  if (FALSE) { # scale with estimated CV
    scale = nreps/multiplyWeight/modelSettings$blocks/(estDispersion^2)
  } else {    # scale with set CV
    scale = nreps/multiplyWeight/modelSettings$blocks/((settings$CVComparator/100)^2)
  }
  ncDiff = ncDiff * sqrt(scale)
  ncEqui$Low = ncEqui$Low * sqrt(scale)
  ncEqui$Upp = ncEqui$Upp * sqrt(scale)
  power = calculatePowerFromNc(nreps, effect, df, ncDiff, ncEqui, settings, settings$DoGMdiff, settings$DoGMequi)
  return(power)
}

######################################################################################################################
# Returns the (signed) non-centrality parameter pvalue for the Wald Equivalence test 
waldEquiNoncentral <- function(estiEffect, seEffect, testType, locLower, locUpper) {
  if (testType == "twosided") {
    ncLow <-  (estiEffect-locLower)/seEffect
    ncUpp <- -(estiEffect-locUpper)/seEffect
  } else if (testType == "left") {
    ncLow <-  (estiEffect-locLower)/seEffect
    ncUpp <- NA
  } else { # right
    ncLow <- NA
    ncUpp <- -(estiEffect-locUpper)/seEffect
  }
  return(list(Low=ncLow, Upp=ncUpp))
}

######################################################################################################################
calculatePowerFromNc <- function(nreps, effect, df, ncDiff, ncEqui, settings, doDiff, doEqui) {
  # Twosided difference test
  if (doDiff) {
    if (settings$TestType == "twosided") {
      critFvalue = qf(1.0 - settings$SignificanceLevel, 1, df)
      powerDiff = pf(critFvalue, 1, df, ncDiff^2, lower.tail=FALSE)
      # Equivalent calculation is
      # critTvalue = qt(1.0 - settings$SignificanceLevel/2, df)
      # powerDiff = pt(critTvalue, df, ncDiff, lower.tail=FALSE) + pt(-critTvalue, df, ncDiff, lower.tail=TRUE)
    } else {
      critTvalue = qt(1.0 - settings$SignificanceLevel, df)
      if (settings$TestType == "left") {
        powerDiff = pt(critTvalue, df, -ncDiff, lower.tail=FALSE)
      } else {
        powerDiff = pt(critTvalue, df, ncDiff, lower.tail=FALSE)
      }
    }
  } else {
    powerDiff = NaN * nreps
  }
  # Twosided equivalence test
  if (doEqui) {
    critTvalue = qt(1.0 - settings$SignificanceLevel, df)
    if (settings$TestType == "twosided") {
      # Bivariate non-central Student distribution
      corr   = matrix(c(1,1,1,1), ncol=2)
      ntimes = length(nreps)
      powerEqui = rep(NA, ntimes)
      for (i in 1:ntimes) {
        if ((is.na(ncEqui$Low[i])) | (is.na(ncEqui$Upp[i]))) {
          powerEqui[i] = NaN
        } else {
          tval = critTvalue[i]
          low = c(tval, -Inf)
          upp = c(Inf, -tval)
          delta = c(ncEqui$Low[i], -ncEqui$Upp[i])
          pp = pmvt(low=low, upp=upp, delta=delta, df=df[i], corr=corr)
          powerEqui[i] = pp
        }
      }
    } else if (settings$TestType == "left") {
      powerEqui = pt(critTvalue, df, ncEqui$Low, lower.tail=FALSE)
    } else { # right
      powerEqui = pt(critTvalue, df, ncEqui$Upp, lower.tail=FALSE)
    }
  } else {
    powerEqui = NaN * nreps
  }
  power = as.data.frame(cbind(effect, nreps, df, ncDiff, ncEquiLow=ncEqui$Low, ncEquiUpp=ncEqui$Upp, powerDiff, powerEqui))
  return(power)

  # Compare with simple product
  ncEqui$Low = ncEqui$Low^2
  ncEqui$Upp = ncEqui$Upp^2
  critFvalue = qf(1.0-2*settings$SignificanceLevel, 1, df)
  pEquiLow = pf(critFvalue, 1, df, ncEqui$Low, lower.tail=FALSE)
  pEquiUpp = pf(critFvalue, 1, df, ncEqui$Upp, lower.tail=FALSE)
  pEquiPrd = pEquiLow*pEquiUpp
  compare = as.data.frame(cbind(df, pEquiLow, pEquiUpp, pEquiPrd, powerEqui))
  #print(compare, digits=3)
  return(power)
}

######################################################################################################################
lylesPowerAnalysis <- function(data, settings, modelSettings, blocks, effect, debugSettings) {

  DEBUG <- FALSE

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
    localBlocks <- 1
  } else if (settings$CVBlocks == 0.0) {
    nreps <- rep(2:blocks)
    localBlocks <- 2
  } else {
    nreps <- blocks
    localBlocks <- blocks
  }
  modelSettings$blocks <- localBlocks

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
  #simulationSettings <- createSimulationSettings(settings)
  simulatedData <- createSimulatedData(data, settings, localBlocks, effect)
  if (FALSE) {
    pos = match(c("Test", "Mean", "Block", "Lp", "Effect"), names(simulatedData))
    print(simulatedData[,pos])
  }
  synData <- createSyntheticData(simulatedData, settings, multiplyWeight) 
  if (settings$IsOutputSimulatedData) {
    csvFile = paste0(localDir, "simulatedData-", localBlocks, ".csv")
    write.csv(simulatedData, csvFile, row.names=FALSE)
    csvFile = paste0(localDir, "synData-", localBlocks, ".csv")
    write.csv(synData, csvFile, row.names=FALSE)
  }

  #synDataOld <- createSyntheticDataNew(simulatedData, settings, multiplyWeight) 
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
  if ((settings$DoLOdiff) || (settings$DoLOequi)) {
    # powerLO is a matrix rather than a dataframe
    if (DEBUG) cat(paste("\nLyles LO exact calculation", "---------------------------------------------------------\n"))
    powerLO <- exactPowerAnalysisHelper(data, settings, modelSettings, nreps, effect)
    if (DEBUG) print(powerLO, digits=3)
    if (settings$DoLOdiff) pValues$Diff[,"LogPlusM"] <- powerLO[,"powerDiff"]
    if (settings$DoLOequi) pValues$Equi[,"LogPlusM"] <- powerLO[,"powerEqui"]
    pValues$Extra[,"Df"] <- powerLO[,"Df"]
  }
  if ((settings$DoGMdiff) || (settings$DoGMequi)) {
    if (DEBUG) cat(paste("\nLyles GM", "---------------------------------------------------------\n"))
    powerGM <- gammaLyles(synData, settings, modelSettings, nreps, effect, multiplyWeight)
    if (DEBUG) print(powerGM, digits=3)
    if (settings$DoGMdiff) pValues$Diff[,"Gamma"] <- powerGM[["powerDiff"]]
    if (settings$DoGMequi) pValues$Equi[,"Gamma"] <- powerGM[["powerEqui"]]
    pValues$Extra[,"Df"] <- powerGM[["df"]]
  }

  if ((FALSE) || (DEBUG)) {
    # Print raw pValues
    printPvaluesApproximate(pValues, settings, blocks, effect)
  }
  return(pValues)
}

######################################################################################################################
# Does exact power analysis for the normal and lognormal distribution
# Note that effect is always multiplicative on the log-scale
exactPowerAnalysis <- function(data, settings, modelSettings, blocks, effect, debugSettings) {
  if (settings$MeasurementType == "Continuous") {
    # Normal: calculate effect on the normal additive scale; Transformed LOC are already on that scale
    doDiff <- settings$DoNOdiff 
    doEqui <- settings$DoNOequi
    effect <- settings$OverallMean * exp(effect) - settings$OverallMean
    distribution = "Normal" 
  } else if (settings$MeasurementType == "Nonnegative") {
    # LogNormal: use mean on transformed scale; not that settings$dispersion is already on transformed scale
    doDiff = settings$DoLOdiff 
    doEqui = settings$DoLOequi
    settings$OverallMean <- log(settings$OverallMean) - settings$dispersion/2
    distribution = "LogPlusM" 
  }

  # Define number of reps and calculate the power
  nreps <- rep(2:blocks)
  power <- exactPowerAnalysisHelper(data, settings, modelSettings, nreps, effect) 

  # Define and fill output structure
  nrow <- length(nreps)
  pValues <- list(Diff=NULL, Equi=NULL, 
      Extra=matrix(nrow=nrow, ncol=3, dimnames=list(nreps, c("Reps", "Effect", "Df"))))
  if (doDiff)  {
    pValues$Diff <- matrix(power[,"powerDiff"], nrow=nrow, ncol=1, dimnames=list(nreps, distribution))
  } 
  if (doEqui) {
    pValues$Equi <- matrix(power[,"powerEqui"], nrow=nrow, ncol=1, dimnames=list(nreps, distribution))
  }
  pValues$Extra[,"Reps"] <- nreps
  pValues$Extra[,"Effect"] <- effect
  pValues$Extra[,"Df"] <- power[,"Df"]
  return(pValues)
}

######################################################################################################################
# Helper for exact power analysis; always returns power for Difference AND equivalence test
exactPowerAnalysisHelper <- function(data, settings, modelSettings, nreps, effect) {
  # Define output structure
  results <- matrix(nrow=length(nreps), ncol=6, dimnames=list(nreps, c("Df", "powerDiff", "powerEqui", "ncDiff", "ncEquiLow", "ncEquiUpp")))

  # Fit model to obtain degrees of freedom by using a dummy dataset
  tmpBlocks <- 2
  simulatedData <- createSimulatedData(data, settings, tmpBlocks, 0)
  simulatedData[["Response"]] =  simulatedData[["Mean"]]
  lmH1 <- lm(as.formula(modelSettings$formulaH1), data=simulatedData)
  regDF <- nrow(simulatedData) - lmH1$df.residual
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    df <- nreps*nrow(data) - regDF
  } else {
    df <- nreps*(nrow(data)-1) - (regDF - tmpBlocks)
  }
  results[,"Df"] <- df

  # Difference test
  varEffect <- 2*settings$dispersion/nreps
  sdEffect <- sqrt(varEffect)
  ncDiff <- effect/sdEffect
  ncEqui <- waldEquiNoncentral(effect, sdEffect, settings$TestType, settings$TransformLocLower, settings$TransformLocUpper)
  power <- calculatePowerFromNc(nreps, effect, df, ncDiff, ncEqui, settings, TRUE, TRUE)
  results[,"powerDiff"] <- power[["powerDiff"]]
  results[,"powerEqui"] <- power[["powerEqui"]]
  results[,"ncDiff"]    <- ncDiff
  results[,"ncEquiLow"] <- ncEqui$Low
  results[,"ncEquiUpp"] <- ncEqui$Upp
  return(results)
}

######################################################################################################################
# Function to display the raw pvalues for each dataset in a concise format
printPvaluesApproximate <- function(pValues, settings, blocks, effect) {
  pVal = cbind(pValues$Diff, pValues$Equi)
  rownames(pVal) <- paste0(" ", pValues$Extra[,1])
  colnames <- colnames(pVal)
  if (settings$DoLNdiff) colnames <- replaceFirst(colnames, "LogNormal",            "  diffLN")
  if (settings$DoLNequi) colnames <- replaceFirst(colnames, "LogNormal",            "  equiLN")
  if (settings$DoSQdiff) colnames <- replaceFirst(colnames, "SquareRoot",           "  diffSQ")
  if (settings$DoSQequi) colnames <- replaceFirst(colnames, "SquareRoot",           "  equiSQ")
  if (settings$DoOPdiff) colnames <- replaceFirst(colnames, "OverdispersedPoisson", "  diffOP")
  if (settings$DoOPequi) colnames <- replaceFirst(colnames, "OverdispersedPoisson", "  equiOP")
  if (settings$DoNBdiff) colnames <- replaceFirst(colnames, "NegativeBinomial",     "  diffNB")
  if (settings$DoNBequi) colnames <- replaceFirst(colnames, "NegativeBinomial",     "  equiNB")
  colnames(pVal) <- colnames
  effectOriginalScale = round(10000*exp(effect))/10000
  cat(paste0("\n", settings$ProjectName,";  Nreps: ", blocks, ";  Effect: ", effectOriginalScale, "\n"))
  print(round(100000*pVal)/100000, na.print="-")
}
replaceFirst <- function(colnames, pattern, replacement) {
  match = match(pattern, colnames)
  colnames[match[1]] <- replacement
  return(colnames)
}


