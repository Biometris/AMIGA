# Remarks
# 1. Due to an apparent bug in the LSMEANS package formulaH1 is saved as a string and fitted by 
#    using as.formula()
#
# 2. OP and NB fails with mustart=data[["Response"]] when this contains zeros; 
#    etastart is used instead
#
# 3. OP and NB equivalent tests return pval=2 in case the estimate is outside the LOC interval. 
#    The pvalue is not calculate in that case to speed up the computation. 
#    In effect the pvalue is then in the interval [0.5,1].
#
# 4. OP and NB fall back to the Poisson in case the OP overdispersion parameter is smaller than 1
#
# 5. LN-GCI produces a NaN in case any of the ratios equals NaN
#

######################################################################################################################
linkFunction <- function(data, measurementType = c("Count", "Fraction", "Nonnegative", "Continuous")) {
  chkArg = match.arg(measurementType)
  if (measurementType == "Count") {
    return(log(data))
  } else if (measurementType == "Fraction") {
    return(log(data/(1-data)))
  } else if (measurementType == "Nonnegative") {
    return(log(data))
  } else if (measurementType == "Continuous") {
    return(data)
  }
}

######################################################################################################################
inverseLinkFunction <- function(data, measurementType = c("Count", "Fraction", "Nonnegative", "Continuous")) {
  chkArg = match.arg(measurementType)
  if (measurementType == "Count") {
    return(exp(data))
  } else if (measurementType == "Fraction") {
    return(1/(1+exp(-data)))
  } else if (measurementType == "Nonnegative") {
    return(exp(data))
  } else if (measurementType == "Continuous") {
    return(data)
  }
}

######################################################################################################################
ropoisson <- function(n, mean, dispersion=NaN, power=NaN, distribution=c("Poisson", "OverdispersedPoisson", "NegativeBinomial", "PoissonLogNormal", "PowerLaw")) {
  stopifnot(n == as.integer(n), n >= 1, mean >= 0)
  if ((length(mean) != 1) && (length(mean) != n)) {
    stop("The length of mean must equal 1 or the value of n.", call. = FALSE)
  }
  if ((length(dispersion) != 1) && (length(dispersion) != n)) {
    stop("The length of dispersion must equal 1 or the value of n.", call. = FALSE)
  }
  mean[mean==0] <- 1.0e-200
  if (min(mean) < 0) {
    stop("The mean must be positive.", call. = FALSE)
  }
  type = match.arg(distribution)
  if (type == "Poisson") {
    sample <- rpois(n, mean)
  } else if (type == "OverdispersedPoisson") {
    if (min(dispersion) <= 1) {
      stop("The dispersion parameter must be larger than 1 for the OverdispersedPoisson distribution.", call. = FALSE)
    }
    s <- dispersion - 1
    a <- mean/s
    sample <- rgamma(n, shape=a, scale=s)
    sample <- rpois(n, sample)
  } else if (type == "NegativeBinomial") {
    if (min(dispersion) <= 0) {
      stop("The dispersion parameter must be larger than 0 for the NegativeBinomial distribution.", call. = FALSE)
    }
    s <- dispersion*mean
    a <- mean/s
    sample <- rgamma(n, shape=a, scale=s)
    sample <- rpois(n, sample)
  } else if (type == "PoissonLogNormal") {
    if (min(dispersion) <= 0) {
      stop("The dispersion parameter must be larger than 0 for the PoissonLogNormal distribution.", call. = FALSE)
    }
    lambda <- log(mean) - log(dispersion+1)/2
    sigma2 <- log(dispersion+1)
    sample <- exp(rnorm(n, lambda, sqrt(sigma2)))
    sample <- rpois(n, sample)
  } else if (type == "PowerLaw") {
    if (min(dispersion) <= 0) {
      stop("The dispersion parameter must be larger than 0 for the PowerLaw distribution.", call. = FALSE)
    }
    if ((power < 1) | (power > 2)) {
      stop("The power parameter must be interval [1,2].", call. = FALSE)
    }
    dispNegbin = (dispersion*mean^power - mean)/(mean*mean)
    if (min(dispNegbin) <= 0) {
      stop("For some parameters of the PowerLaw distribution the calculated dispersion parameter of the negative binomial distribution is not positive.", call. = FALSE)
    }
    # Use negative binomial
    s <- dispNegbin*mean
    a <- mean/s
    sample <- rgamma(n, shape=a, scale=s)
    sample <- rpois(n, sample)
  }
  return(sample)
}

######################################################################################################################
ropoissonVariance <- function(n, mean, dispersion=NaN, power=NaN, distribution=c("Poisson", "OverdispersedPoisson", "NegativeBinomial", "PoissonLogNormal", "PowerLaw")) {
  type = match.arg(distribution)
  if (type == "Poisson") {
    variance <- mean
  } else if (type == "OverdispersedPoisson") {
    variance <- dispersion*mean
  } else if (type == "NegativeBinomial") {
    variance <- mean + dispersion*mean*mean
  } else if (type == "PoissonLogNormal") {
    variance <- mean + dispersion*mean*mean
  } else if (type == "PowerLaw") {
    variance <- dispersion*mean^power
  }
  return(variance)
}

######################################################################################################################
ropoissonDispersion <- function(mean, CV, power, distribution=c("Poisson", "OverdispersedPoisson", "NegativeBinomial", "PoissonLogNormal", "PowerLaw")) {
  if (mean <= 0) {
    stop("Mean of distribution must be positive.", call. = FALSE)
  }
  if (CV <= 0) {
    stop("Coefficient of Variation must be positive.", call. = FALSE)
  }
  
  type = match.arg(distribution)
  if (type == "PowerLaw") {
    if ((power < 1) | (power > 2)) {
      stop("The power parameter must be interval [1,2].", call. = FALSE)
    }
  }
  
  if (type == "Poisson") {
    dispersion = NaN
  } else if (type == "OverdispersedPoisson") {
    dispersion <- (CV/100) * (CV/100) * mean
  } else if (type == "NegativeBinomial") {
    dispersion <- (CV/100) * (CV/100) - 1/mean
  } else if (type == "PoissonLogNormal") {
    dispersion <- (CV/100) * (CV/100) - 1/mean
  } else if (type == "PowerLaw") {
    dispersion <- (CV/100) * (CV/100) * mean^(2-power)
  }
  return(dispersion)
}

######################################################################################################################
readDataFile <- function(dataFile) {
  # Read data
  data <- read.csv(dataFile)
  colnames <- colnames(data)
  # Redefine Mod columns to factors
  modifiers = grep("Mod", colnames)
  if (length(modifiers) > 0) {
    for (i in 1:length(modifiers)) {
      data[[modifiers[[i]]]] = as.factor(data[[modifiers[[i]]]])
    } 
  }
  return(data)
}

######################################################################################################################
readSettings <- function(settingsFile) {
  settings <- read.csv(settingsFile, header=FALSE, as.is=TRUE, strip.white=TRUE)
  list = as.list(setNames(nm=settings$V1))
  for (i in 1:nrow(settings)) {
    if (settings$V1[i] != "Endpoint") {
      element <- unlist(strsplit(settings$V2[i], " "))
    } else {
      element <- settings$V2[i]
    }
    isNumeric = suppressWarnings(!is.na(as.numeric(element[1])))
    if (isNumeric) {
      element <- as.numeric(element)
    } else  {
      element <- as.character(element)
    }
    list[[i]] <- element
  }
  # Get transformed limits of concern
  if (list$MeasurementType != "Continuous") {
    list$TransformLocLower <- log(list$LocLower)
    list$TransformLocUpper <- log(list$LocUpper)
  } else {
    list$TransformLocLower <- list$LocLower - list$OverallMean
    list$TransformLocUpper <- list$LocUpper + list$OverallMean
  }
  #  Redefine list$UseWaldTest as logical
  if (list$UseWaldTest == "True") {
    list$UseWaldTest = TRUE
  } else {
    list$UseWaldTest = FALSE
  }
  #  Redefine list$IsOutputSimulatedData as logical
  if (list$IsOutputSimulatedData == "True") {
    list$IsOutputSimulatedData = TRUE
  } else {
    list$IsOutputSimulatedData = FALSE
  }
  # Add directory to list
  list$directory = paste0(dirname(settingsFile), "/")
  return(list)
}

######################################################################################################################
createModelSettings <- function(data, settings) {
  modelSettings <- list()
  colnames <- colnames(data)

  # Create dataframe for prediction; only use GMO columns and Dummy columns
  # This implies that the mean is taken over blocks and also over modifiers (if any)
  predictors = grep("GMO", gsub("Dummy", "GMO", colnames))
  modelSettings$preddata <- head(data[predictors], 2)
  modelSettings$preddata[,] <- 0
  modelSettings$preddata[1] <- c(0,1)

  # Model formulas for H0 and H1; Note that GMO is used to test the comparison
  nterms <- grep("Mean", colnames) - 1
  nameH1 <- colnames[c(2:nterms)]
  if (settings$ExperimentalDesignType == "RandomizedCompleteBlocks") {
    nameH1 <- c(nameH1, "Block") # Add blocking effect
  }

  # Define models to fit
  modelSettings$formulaH1 <- as.formula(paste0("Response ~ ", paste0(nameH1, collapse=" + ")))
  modelSettings$formulaH0 <- update(modelSettings$formulaH1, ~ . - GMO)
  modelSettings$formulaH0_low <- update(modelSettings$formulaH0, ~ . + offset(LowOffset))
  modelSettings$formulaH0_upp <- update(modelSettings$formulaH0, ~ . + offset(UppOffset))
  # Due to an apparent bug in the LSMEANS package formulaH1 is saved as a string and fitted by using as.formula()
  modelSettings$formulaH1 <- paste0("Response ~ ", paste0(nameH1, collapse=" + "))

  # Additional settings
  modelSettings$smallGCI = 0.0001 # Bound on back-transformed values for the LOG-transform

  # Settings for fitting the NB model 
  modelSettings$NBmethod = "optimize"   # method to fit the NB model (MASS, optimize)
  modelSettings$NBlower = 0.001         # lower value for NB-theta when optimize is used to fit the model
  modelSettings$NBupper = 10000         # upper value for NB-theta when optimize is used to fit the model

  return(modelSettings)
}

######################################################################################################################
createSimulationSettings <- function(settings) {
  simulationSettings <- list()

  # Define dispersion parameter
  simulationSettings$dispersion <- ropoissonDispersion(settings$OverallMean, settings$CVComparator, settings$PowerLawPower, settings$Distribution)  
  print(paste0("Dispersion parameter: ", simulationSettings$dispersion))

  # Add blocking effect to model 
  if (settings$ExperimentalDesignType == "RandomizedCompleteBlocks") {
    simulationSettings$sigBlock <- sqrt(log((settings$CVBlocks/100)^2 + 1))
  } else {
    simulationSettings$sigBlock <- 0
  }
  return(simulationSettings)
}

######################################################################################################################
createSimulatedData <- function(data, settings, simulationSettings, blocks, effect) {
  setupSimulatedData <- data
  
  # Expand dataset; add Row and Block factor
  setupSimulatedData <- data[rep(row.names(data), blocks + 0*data[[1]]),]
  setupSimulatedData[["Row"]] <- as.factor(c(1:(blocks*nrow(data))))
  setupSimulatedData[["Block"]] <- as.factor(rep(1:blocks, nrow(data)))
  
  # Add several additional columns to the dataframe
  setupSimulatedData["TransformedMean"] <- linkFunction(data["Mean"], settings$MeasurementType)
  
  # Apply blocking Effect on the transformed scale; use Blom scores
  if (settings$ExperimentalDesignType == "RandomizedCompleteBlocks") {
    blockeff = simulationSettings$sigBlock * qnorm(((1:blocks) - 0.375)/ (blocks + 0.25))
    setupSimulatedData["BlockEffect"] <- blockeff[setupSimulatedData[["Block"]]]
  } else {
    setupSimulatedData["BlockEffect"] <- 0
  }
  setupSimulatedData["LowOffset"] <- setupSimulatedData["GMO"] * settings$TransformLocLower
  setupSimulatedData["UppOffset"] <- setupSimulatedData["GMO"] * settings$TransformLocUpper
  
  # Add extra columns which are used to simulate and fit data
  setupSimulatedData["TransformedEffect"] <- setupSimulatedData["TransformedMean"] + setupSimulatedData["BlockEffect"] + (setupSimulatedData["GMO"] == 1) * effect
  setupSimulatedData["Effect"] <- inverseLinkFunction(setupSimulatedData["TransformedEffect"], settings$MeasurementType)
  setupSimulatedData["Response"]          <- setupSimulatedData["GMO"] * NaN
  setupSimulatedData["Lp"]          <- setupSimulatedData["GMO"] * NaN
  return(setupSimulatedData)
}

######################################################################################################################
simulateResponse <- function(simulatedData, settings, simulationSettings) {
  response <- ropoisson(nrow(simulatedData), simulatedData[["Effect"]], simulationSettings$dispersion, settings$PowerLawPower, settings$Distribution)
  return(response)
}

######################################################################################################################
createEvaluationGrid <- function(LocLower, LocUpper, NumberOfEvaluations) {
  csd <- c(0)
  effect <- c(0)
  if (!is.na(LocLower)) {
    csdTmp <- -1 + rep(0:NumberOfEvaluations)/(NumberOfEvaluations+1)
    effectTmp <- -LocLower*csdTmp
    csd <- c(abs(csdTmp), csd)
    effect <- c(effectTmp, effect)
  } 
  if (!is.na(LocUpper)) {
    csdTmp <- 1 - rep(NumberOfEvaluations:0)/(NumberOfEvaluations+1)
    effectTmp <- LocUpper*csdTmp
    csd <- c(csd, abs(csdTmp))
    effect <- c(effect, effectTmp)
  } 
  return(list(csd=csd, effect=effect))
}

######################################################################################################################
normalAnalysis <- function(data, settings, modelSettings, debugSettings) {
  require(lsmeans)
  require(MASS)
  pvalues <- list(Diff=NaN, Equi=NaN)

  # Fit model
  lmH1 <- lm(as.formula(modelSettings$formulaH1), data=data)
  resDF <- lmH1$df.residual
  resMS <- deviance(lmH1)/resDF
  estiEffect = lmH1$coef[2]
  seEffect = sqrt(vcov(lmH1)[2,2])
  pvalues$Diff <- as.numeric(2*pt(abs(estiEffect)/seEffect, resDF, lower.tail=FALSE))

  pLowerEqui = as.numeric(pt((estiEffect-settings$LocLower)/seEffect, resDF, lower.tail=FALSE))
  pUpperEqui = as.numeric(pt((estiEffect-settings$LocUpper)/seEffect, resDF, lower.tail=TRUE))
  pvalues$Equi = max(pLowerEqui, pUpperEqui) # Both one-sided hypothesis must be rejected

  return(pvalues)

  # Due to the return above this code is not executed
  # It shows that the CGI approach is equivalent to the usual approach in simple situations
  # Obtain predicted means for CMP/GMO and corresponding VCOV
  lsmeans <- lsmeans(lmH1, "GMO", at=modelSettings$preddata)
  meanCMP <- summary(lsmeans)$lsmean[1]
  meanGMO <- summary(lsmeans)$lsmean[2]
  vcovLS = vcov(lsmeans)

  # GCI; this takes account of a possible covariance between predictions
  chi  <- resDF * resMS / rchisq(settings$NumberOfSimulationsGCI, resDF)
  random = mvrnorm(settings$NumberOfSimulationsGCI, c(0,0), vcovLS)*sqrt(chi/resMS)
  random[,1] = meanCMP + random[,1]
  random[,2] = meanGMO + random[,2]
  ratio <- random[,2] - random[,1]

  pLowerCGI = mean(ratio < settings$LocLower)
  pUpperCGI = mean(ratio > settings$LocUpper)
  print(paste0("FIT pLower: ", pLowerEqui))
  print(paste0("FIT pUpper: ", pUpperEqui))
  print(paste0("CGI pLower: ", pLowerCGI))
  print(paste0("CGI pUpper: ", pUpperCGI))
  return(pvalues)
}

######################################################################################################################
logNormalAnalysis <- function(data, settings, modelSettings, debugSettings) {
  # Wald and LR are identical as there is no LR equivalence test
  require(lsmeans)
  require(MASS)
  pvalues <- list(Diff=NaN, Equi=NaN)

  # Fit model
  data["Response"] <- log(data["Response"] + 1)
  lmH1 <- lm(as.formula(modelSettings$formulaH1), data=data)
  resDF <- lmH1$df.residual
  resMS <- deviance(lmH1)/resDF
  estiEffect = lmH1$coef[2]
  seEffect = sqrt(vcov(lmH1)[2,2])
  pvalues$Diff <- as.numeric(2*pt(abs(estiEffect)/seEffect, resDF, lower.tail=FALSE))

  if (settings$NumberOfSimulationsGCI > 0) {
    # Obtain predicted means for CMP/GMO and corresponding VCOV
    lsmeans <- lsmeans(lmH1, "GMO", at=modelSettings$preddata)
    meanCMP <- summary(lsmeans)$lsmean[1]
    meanGMO <- summary(lsmeans)$lsmean[2]
    vcovLS = vcov(lsmeans)
    # GCI; this takes account of a possible covariance between predictions
	# In case any of the draws is Inf (after exponentiation) the corresponding ratio is set to NaN and the pvalue is also set to NaN
    chi  <- resDF * resMS / rchisq(settings$NumberOfSimulationsGCI, resDF)
    random = mvrnorm(settings$NumberOfSimulationsGCI, c(0,0), vcovLS)*sqrt(chi/resMS)
    random[,1] = meanCMP + random[,1]
    random[,2] = meanGMO + random[,2]
    random <- exp(random + chi/2) - 1
    random[random < modelSettings$smallGCI] <- modelSettings$smallGCI
    ratio <- random[,2]/random[,1]
    ratio[(random[,1]==Inf) | (random[,2]==Inf)] = NaN
    pLowerCGI = mean(ratio < settings$LocLower)  # If this is smaller than alfa: reject H0: esti < LocLower
    pUpperCGI = mean(ratio > settings$LocUpper)  # If this is smaller than alfa: reject H0: esti > LocUpper
    pvalues$Equi = max(pLowerCGI, pUpperCGI)     # Both one-sided hypothesis must be rejected
  }
  return(pvalues)

  # Originl code for Generalized confidence interval
  # seCMP = summary(lsmeans)$SE[1]
  # seGMO = summary(lsmeans)$SE[2]
  # repCMP <- resMS / (seCMP^2)
  # repGMO <- resMS / (seGMO^2)
  # chi  <- resDF * resMS / rchisq(settings$NumberOfSimulationsGCI, resDF)
  # rCMP <- rnorm(settings$NumberOfSimulationsGCI, meanCMP, sqrt(chi/repCMP))
  # rGMO <- rnorm(settings$NumberOfSimulationsGCI, meanGMO, sqrt(chi/repGMO))
  # rCMP <- exp(rCMP + chi/2) - 1
  # rGMO <- exp(rGMO + chi/2) - 1
  # rCMP[rCMP < modelSettings$smallGCI] <- modelSettings$smallGCI
  # rGMO[rGMO < modelSettings$smallGCI] <- modelSettings$smallGCI
  # ratio <- rGMO/rCMP
  # quantiles <- quantile(ratio, c(settings$SignificanceLevel, 1 - settings$SignificanceLevel), na.rm=TRUE)
  # print(quantiles)
}

######################################################################################################################
squareRootAnalysis <- function(data, settings, modelSettings, debugSettings) {
  # Wald and LR are identical as there is no LR equivalence test
  require(lsmeans)
  require(MASS)
  pvalues <- list(Diff=NaN, Equi=NaN)

  # Fit model
  data["Response"] <- sqrt(data["Response"])
  lmH1 <- lm(as.formula(modelSettings$formulaH1), data=data)
  resDF <- lmH1$df.residual
  resMS <- deviance(lmH1)/resDF
  estiEffect = lmH1$coef[2]
  seEffect = sqrt(vcov(lmH1)[2,2])
  pvalues$Diff <- as.numeric(2*pt(abs(estiEffect)/seEffect, resDF, lower.tail=FALSE))

  if (settings$NumberOfSimulationsGCI > 0) {
    # Obtain predicted means for CMP/GMO and corresponding VCOV
    lsmeans <- lsmeans(lmH1, "GMO", at=modelSettings$preddata)
    meanCMP <- summary(lsmeans)$lsmean[1]
    meanGMO <- summary(lsmeans)$lsmean[2]
    vcovLS = vcov(lsmeans)
    # GCI; this takes account of a possible covariance between predictions
    chi  <- resDF * resMS / rchisq(settings$NumberOfSimulationsGCI, resDF)
    random = mvrnorm(settings$NumberOfSimulationsGCI, c(0,0), vcovLS)*sqrt(chi/resMS)
    random[,1] = meanCMP + random[,1]
    random[,2] = meanGMO + random[,2]
    random <- random*random + chi
    ratio <- random[,2]/random[,1]
    ratio[ratio==Inf] = NaN
    pLowerCGI = mean(ratio < settings$LocLower)  # If this is smaller than alfa: reject H0: esti < LocLower
    pUpperCGI = mean(ratio > settings$LocUpper)  # If this is smaller than alfa: reject H0: esti > LocUpper
    pvalues$Equi = max(pLowerCGI, pUpperCGI)     # Both one-sided hypothesis must be rejected
  }
  return(pvalues)
}

######################################################################################################################
overdispersedPoissonAnalysis <- function(data, settings, modelSettings, debugSettings) {
  # Fall back to Poisson in case the overdispersion is less than or equal to limitDispersion
  limitDispersion <- 1
  family <- "quasipoisson"
  # Prepare results list and fit H1; default method is DMETHOD=pearson
  pvalues <- list(Diff = c(NaN), Equi = c(NaN), Dispersion=c(NaN))
  glmH1 <- glm(as.formula(modelSettings$formulaH1), family=family, data=data, etastart=TransformedEffect)
  #print(paste0("Number of iteration ", glmH1$iter))
  resDF <- df.residual(glmH1)
  estDispersion <- summary(glmH1)$dispersion
  estiEffect <- as.numeric(glmH1$coef[2])
  seEffect <- sqrt(vcov(glmH1)[2,2])
  if (estDispersion <= limitDispersion) {
    seEffect <- seEffect / sqrt(estDispersion)
    estDispersion <- NaN
    family <- "poisson"
  }
  # estDispersion <- sum(residuals(glmH1,type="pearson")^2)/resDF
  pvalues$Dispersion <- estDispersion
  if (settings$IsOutputSimulatedData) {
    writeLines(paste0("\nOP iRep ", debugSettings$iRep, " iEffect ", debugSettings$iEffect, " Dataset ", debugSettings$iDataset), debugSettings$displayFile)
    writeLines(paste0("  dispersion: ", estDispersion), debugSettings$displayFile)
    writeLines(paste0("  estiEffect: ", estiEffect), debugSettings$displayFile)
    writeLines(paste0("  seEffect:   ", seEffect), debugSettings$displayFile)
  }

  if (settings$UseWaldTest) {  
    # Results based on Wald tests
    if (family == "poisson") {
      pvalues$Diff <- 2*pnorm(abs(estiEffect)/seEffect, lower.tail=FALSE)
      pLowerEqui <- pnorm((estiEffect-settings$TransformLocLower)/seEffect, lower.tail=FALSE)
      pUpperEqui <- pnorm((estiEffect-settings$TransformLocUpper)/seEffect, lower.tail=TRUE)
    } else {
      pvalues$Diff <- 2*pt(abs(estiEffect)/seEffect, resDF, lower.tail=FALSE)
      pLowerEqui <- pt((estiEffect-settings$TransformLocLower)/seEffect, resDF, lower.tail=FALSE)
      pUpperEqui <- pt((estiEffect-settings$TransformLocUpper)/seEffect, resDF, lower.tail=TRUE)
    }
    pvalues$Equi <- max(pLowerEqui, pUpperEqui) # Both one-sided hypothesis must be rejected
  } else {
    # Results based on LR test; denominator based on Pearson statistic
    data[["Lp"]] <- glmH1$linear.predictor
    glmH0 <- glm(modelSettings$formulaH0, family=family, data=data, etastart=Lp)
    if (family == "poisson") {
        pvalues$Diff <- pchisq(deviance(glmH0) - deviance(glmH1), 1, lower.tail=FALSE)
    } else {
      pvalues$Diff <- pf((deviance(glmH0) - deviance(glmH1))/estDispersion, 1, resDF, lower.tail=FALSE)
    }
    # and LR equivalence test
    if ((estiEffect < settings$TransformLocLower) | (estiEffect > settings$TransformLocUpper)) {
      pvalues$Equi <- 2
    } else {
      # LR equivalence test
      glmH0low <- glm(modelSettings$formulaH0_low, family=family, data=data, etastart=Lp)
      glmH0upp <- glm(modelSettings$formulaH0_upp, family=family, data=data, etastart=Lp)
      if (family == "poisson") {
        pvalLow <- pchisq(deviance(glmH0low) - deviance(glmH1), 1, lower.tail=FALSE)
        pvalUpp <- pchisq(deviance(glmH0upp) - deviance(glmH1), 1, lower.tail=FALSE)
      } else {
        pvalLow <- pf((deviance(glmH0low) - deviance(glmH1))/estDispersion, 1, resDF, lower.tail=FALSE)
        pvalUpp <- pf((deviance(glmH0upp) - deviance(glmH1))/estDispersion, 1, resDF, lower.tail=FALSE)
      }
      pvalues$Equi <- max(pvalLow, pvalUpp)/2
    }
  }
  return(pvalues)
}

######################################################################################################################
negativeBinomialAnalysis <- function(data, settings, modelSettings, debugSettings) {
  require(MASS)

  # Prepare results list and fit H1
  pvalues <- list(Diff = c(NaN), Equi = c(NaN), Dispersion=c(NaN))
  glmH1 <- fitNB(as.formula(modelSettings$formulaH1), data, etastart=TransformedEffect, method=modelSettings$NBmethod, lower=modelSettings$NBlower, upper=modelSettings$NBupper)
  # glmH1 <- glm.nb(as.formula(modelSettings$formulaH1), data=data, link=log, etastart=data[["TransformedEffect"]])
  resDF <- df.residual(glmH1)
  pvalues$Dispersion = glmH1$theta
  estiEffect <- glmH1$coef[2]
  seEffect <- sqrt(vcov(glmH1)[2,2])
  if (settings$IsOutputSimulatedData) {
    writeLines(paste0("\nNB iRep ", debugSettings$iRep, " iEffect ", debugSettings$iEffect, " Dataset ", debugSettings$iDataset), debugSettings$displayFile)
    writeLines(paste0("  dispersion: ", glmH1$theta), debugSettings$displayFile)
    writeLines(paste0("  estiEffect: ", estiEffect), debugSettings$displayFile)
    writeLines(paste0("  seEffect:   ", seEffect), debugSettings$displayFile)
  }

  if (settings$UseWaldTest) {  
    # Results based on Wald tests
    pvalues$Diff <- as.numeric(2*pt(abs(estiEffect)/seEffect, resDF, lower.tail=FALSE))
    pLowerEqui = as.numeric(pt((estiEffect-settings$TransformLocLower)/seEffect, resDF, lower.tail=FALSE))
    pUpperEqui = as.numeric(pt((estiEffect-settings$TransformLocUpper)/seEffect, resDF, lower.tail=TRUE))
    pvalues$Equi = max(pLowerEqui, pUpperEqui) # Both one-sided hypothesis must be rejected
  } else {
    # Results based on LR test
    data[["Lp"]] = glmH1$linear.predictor
    glmH0 <- fitNB(modelSettings$formulaH0, data, etastart=Lp, method=modelSettings$NBmethod, lower=modelSettings$NBlower, upper=modelSettings$NBupper)
    # glmH0 <- glm.nb(modelSettings$formulaH0, data=data, link=log, etastart=etastart)
    pvalues$Diff <- as.numeric(pchisq(-2*logLik(glmH0) + 2*logLik(glmH1), 1, lower.tail=FALSE))
    # and LR equivalence test
    if ((estiEffect < settings$TransformLocLower) | (estiEffect > settings$TransformLocUpper)) {
      pvalues$Equi <- 2
    } else {
      # LR equivalence test
      #glmH0low <- glm.nb(modelSettings$formulaH0_low, data=data, link=log, etastart=etastart)
      #glmH0upp <- glm.nb(modelSettings$formulaH0_upp, data=data, link=log, etastart=etastart)
      glmH0low <- fitNB(modelSettings$formulaH0_low, data, etastart=Lp, method=modelSettings$NBmethod, lower=modelSettings$NBlower, upper=modelSettings$NBupper)
      glmH0upp <- fitNB(modelSettings$formulaH0_upp, data, etastart=Lp, method=modelSettings$NBmethod, lower=modelSettings$NBlower, upper=modelSettings$NBupper)
      pvalLow <- pchisq(-2*(logLik(glmH0low) - logLik(glmH1)), 1, lower.tail=FALSE)
      pvalUpp <- pchisq(-2*(logLik(glmH0upp) - logLik(glmH1)), 1, lower.tail=FALSE)
      pvalues$Equi <- max(pvalLow, pvalUpp)/2
    }
  }
  return(pvalues)
}

######################################################################################################################
fitNB <- function(model, data, etastart, method=c("MASS", "optimize"), lower=0.001, upper=10000) {
  if (method == "MASS") {
    glm <- glm.nb(model, data=data, link=log, etastart=etastart)
  } else {
    min = optimize(fitNBhelper, lower=log(lower), upper=log(upper), model=model, data=data)
    theta = exp(min$minimum)
    glm <- glm.nb(model, data=data, link=log, init.theta=theta)
  }
  return(glm)
}

######################################################################################################################
fitNBhelper <- function(model, data, theta) {
  agg = exp(theta)
  glm = glm(model, family=negative.binomial(agg), data=data)
  -2*logLik(glm)
}

######################################################################################################################
monteCarloPowerAnalysis <- function(data, settings, modelSettings, blocks, effect, debugSettings) {

  DEBUG = TRUE

  # Global settings
  # debugSettings$iRep            counting Reps loop (integer)
  # debugSettings$iEffect         counting Effects loop (integer)
  # debugSettings$iDataset        counting Datasets loop (integer)
  #
  # Local settings
  # debugSettings$displayFile     File connection to display the fit of models

  # Prepare for debugging, i.e. create directory to write files to
  if (settings$IsOutputSimulatedData) {
    localDir = paste0(settings$directory)
    localDir = paste0(localDir, settings$ComparisonId, "-Endpoint", "/")
    dir.create(localDir, showWarnings=FALSE)
    localDir = paste0(localDir, "Rep", str_pad(debugSettings$iRep, 2, side="left", "0"), "/")
    dir.create(localDir, showWarnings=FALSE)
    localDir = paste0(localDir, "Effect", str_pad(debugSettings$iEffect, 2, side="left", "0"), "/")
    dir.create(localDir, showWarnings=FALSE)
    unlink(paste0(localDir, "*.csv"))
    debugSettings$displayFile = file(paste0(localDir, "00-DisplayFit.txt"), open="wt")
  }

  nanalysis = length(settings$AnalysisMethods)
  nrow = settings$NumberOfSimulatedDataSets
  pValues = list(
    Diff  = matrix(nrow=nrow, ncol=nanalysis, dimnames=list(NULL, settings$AnalysisMethods)),
    Equi  = matrix(nrow=nrow, ncol=nanalysis, dimnames=list(NULL, settings$AnalysisMethods)),
    Extra = matrix(nrow=nrow, ncol=2, dimnames=list(NULL, c("OPdisp", "NBtheta")))
  )

  # Setup simulation settings
  simulationSettings <- createSimulationSettings(settings)
  simulatedData <- createSimulatedData(data, settings, simulationSettings, blocks, effect)

  # Do looping over simulations 
  ndigits = ceiling(log10(settings$NumberOfSimulatedDataSets) + 0.0001)
  for (k in 1:settings$NumberOfSimulatedDataSets) {
    debugSettings$iDataset = k
    # simulate data
    simulatedData[["Response"]] <- simulateResponse(simulatedData, settings, simulationSettings)
    if (settings$IsOutputSimulatedData) {
      csvFile = str_pad(k, ndigits, side="left", "0")      
      csvFile = paste0(localDir, "Data-", csvFile, ".csv")
      write.csv(simulatedData, csvFile, row.names=FALSE)
    }
    # Fit models
    if (!is.na(match("LogNormal", settings$AnalysisMethods))) {
      if (DEBUG) print(paste(k, "LN"))
      result <- logNormalAnalysis(simulatedData, settings, modelSettings, debugSettings)
      pValues$Diff[k, "LogNormal"] <- result$Diff
      pValues$Equi[k, "LogNormal"] <- result$Equi
    }
    if (!is.na(match("SquareRoot", settings$AnalysisMethods))) {
      if (DEBUG) print(paste(k, "SQ"))
      result <- squareRootAnalysis(simulatedData, settings, modelSettings, debugSettings)
      pValues$Diff[k, "SquareRoot"] <- result$Diff
      pValues$Equi[k, "SquareRoot"] <- result$Equi
    }
    # Fit overdispersed Poisson anyway to get the overdispersion parameter
    if ((!is.na(match("OverdispersedPoisson", settings$AnalysisMethods))) | (!is.na(match("NegativeBinomial", settings$AnalysisMethods)))) {
      if (DEBUG) print(paste(k, "OP"))
      result <- overdispersedPoissonAnalysis(simulatedData, settings, modelSettings, debugSettings)
      if (!is.na(match("OverdispersedPoisson", settings$AnalysisMethods))) {
        pValues$Diff[k, "OverdispersedPoisson"] <- result$Diff
        pValues$Equi[k, "OverdispersedPoisson"] <- result$Equi
        pValues$Extra[k, "OPdisp"] <- result$Dispersion
      }
      if (!is.na(match("NegativeBinomial", settings$AnalysisMethods))) {
        if (DEBUG) print(paste(k, "NB"))
        if (!is.na(result$Dispersion)) {
          result <- negativeBinomialAnalysis(simulatedData, settings, modelSettings, debugSettings)
        }
        pValues$Diff[k, "NegativeBinomial"] <- result$Diff
        pValues$Equi[k, "NegativeBinomial"] <- result$Equi
        pValues$Extra[k, "NBtheta"] <- result$Dispersion
      }
    }
  }

  if (settings$IsOutputSimulatedData) {
    print(pValues)
    if (settings$UseWaldTest) {
      csvFile = paste0(localDir, "00-PvaluesWald.csv")
    } else {
      csvFile = paste0(localDir, "00-PvaluesLR.csv")
    }
    write.csv(pValues, csvFile, row.names=FALSE)
  }

  return(pValues)
}

######################################################################################################################
runPowerAnalysis <- function(data, settings) {
  
  # Compute evaluation grid
  evaluationGrid <- createEvaluationGrid(settings$TransformLocLower, settings$TransformLocUpper, settings$NumberOfEvaluationPoints)
  effect <- evaluationGrid$effect
  neffect <- length(effect)
  nreplication <- length(settings$NumberOfReplications)
  
  # Define structures to save results
  nanalysis <- length(settings$AnalysisMethods)
  nrow <- nreplication * neffect
  results = list(
    Effect = matrix(nrow=nrow, ncol=4, dimnames=list(NULL, c("Effect", "TransformedEffect", "CSD", "NumberOfReplications"))),
    Diff = matrix(nrow=nrow, ncol=nanalysis, dimnames=list(NULL, paste("Diff",   settings$AnalysisMethods, sep=""))), 
    Equi = matrix(nrow=nrow, ncol=nanalysis, dimnames=list(NULL, paste("Equi",   settings$AnalysisMethods, sep=""))), 
    EquiWD = matrix(nrow=nrow, ncol=nanalysis, dimnames=list(NULL, paste("EquiWD", settings$AnalysisMethods, sep="")))
    )
  
  results$Effect[,"TransformedEffect"] <- rep(effect, nreplication)
  results$Effect[,"CSD"] <- rep(evaluationGrid$csd, nreplication) 
  results$Effect[,"NumberOfReplications"] <- rep(settings$NumberOfReplications, each=neffect)
  if (settings$MeasurementType != "Continuous") {
    results$Effect[,"Effect"] <- exp(results$Effect[,"TransformedEffect"])
  } else {
    results$Effect[,"Effect"] <- results$Effect[,"TransformedEffect"]
  }

  # Create model settings
  modelSettings <- createModelSettings(data, settings)

  tel <- 0
  for (i in 1:nreplication) {
    for (j in 1:neffect) {
      pValues <- monteCarloPowerAnalysis(data, settings, modelSettings, settings$NumberOfReplications[i], effect[j])
      tel <- tel + 1
      results$Diff[tel,] <- colSums(pValues$Diff < settings$SignificanceLevel) / settings$NumberOfSimulatedDataSets
      results$Equi[tel,] <- colSums(pValues$Equi < settings$SignificanceLevel) / settings$NumberOfSimulatedDataSets
      results$EquiWD[tel,] <- colSums(pValues$EquiWD < settings$SignificanceLevel) / settings$NumberOfSimulatedDataSets      
    }
  }

  # Combine results into a single dataframe
  df <- cbind(results$Effect, results$Diff, results$Equi, results$EquiWD)

  return(df)
}
