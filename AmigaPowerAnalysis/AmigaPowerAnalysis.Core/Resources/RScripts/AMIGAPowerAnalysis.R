LinkFunction <- function(data, measurementType = c("Count", "Fraction", "Nonnegative", "Continuous")) {
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

InverseLinkFunction <- function(data, measurementType = c("Count", "Fraction", "Nonnegative", "Continuous")) {
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

ropoisson <- function(n, mean, dispersion=NaN, power=NaN, distribution=c("Poisson", "OverdispersedPoisson", "NegativeBinomial", "PoissonLogNormal", "PowerLaw")) {
  
  stopifnot(n == as.integer(n), n >= 1, mean >= 0)
  if ((length(mean) != 1) && (length(mean) != n)) {
    stop("The length of mean must equal 1 or the value of n.", call. = FALSE)
  }
  if ((length(dispersion) != 1) && (length(dispersion) != n)) {
    stop("The length of dispersion must equal 1 or the value of n.", call. = FALSE)
  }
  mean[mean==0] = 1.0e-200
  if (min(mean) < 0) {
    stop("The mean must be positive.", call. = FALSE)
  }
  type = match.arg(distribution)
  if (type == "Poisson") {
    sample = rpois(n, mean)
  } else if (type == "OverdispersedPoisson") {
    if (min(dispersion) <= 1) {
      stop("The dispersion parameter must be larger than 1 for the OverdispersedPoisson distribution.", call. = FALSE)
    }
    s = dispersion - 1
    a = mean/s
    sample = rgamma(n, shape=a, scale=s)
    sample = rpois(n, sample)
  } else if (type == "NegativeBinomial") {
    if (min(dispersion) <= 0) {
      stop("The dispersion parameter must be larger than 0 for the NegativeBinomial distribution.", call. = FALSE)
    }
    s = dispersion*mean
    a = mean/s
    sample = rgamma(n, shape=a, scale=s)
    sample = rpois(n, sample)
  } else if (type == "PoissonLogNormal") {
    if (min(dispersion) <= 0) {
      stop("The dispersion parameter must be larger than 0 for the PoissonLogNormal distribution.", call. = FALSE)
    }
    lambda = log(mean) - log(dispersion+1)/2
    sigma2 = log(dispersion+1)
    sample = exp(rnorm(n, lambda, sqrt(sigma2)))
    sample = rpois(n, sample)
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
    s = dispNegbin*mean
    a = mean/s
    sample = rgamma(n, shape=a, scale=s)
    sample = rpois(n, sample)
  }
  return(sample)
}

ropoissonVariance <- function(n, mean, dispersion=NaN, power=NaN, distribution=c("Poisson", "OverdispersedPoisson", "NegativeBinomial", "PoissonLogNormal", "PowerLaw")) {
  type = match.arg(distribution)
  if (type == "Poisson") {
    variance = mean
  } else if (type == "OverdispersedPoisson") {
    variance = dispersion*mean
  } else if (type == "NegativeBinomial") {
    variance = mean + dispersion*mean*mean
  } else if (type == "PoissonLogNormal") {
    variance = mean + dispersion*mean*mean
  } else if (type == "PowerLaw") {
    variance = dispersion*mean^power
  }
  return(variance)
}

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
    dispersion = (CV/100) * (CV/100) * mean
  } else if (type == "NegativeBinomial") {
    dispersion = (CV/100) * (CV/100) - 1/mean
  } else if (type == "PoissonLogNormal") {
    dispersion = (CV/100) * (CV/100) - 1/mean
  } else if (type == "PowerLaw") {
    dispersion = (CV/100) * (CV/100) * mean^(2-power)
  }
  return(dispersion)
}

readDataFile <- function(dataFile) {
  # Read data
  data = read.csv(dataFile)
  colnames = colnames(data)
  
  # Redefine Mod columns to factors
  modifiers = grep("Mod", colnames)
  if (length(modifiers) > 0) {
    for (i in 1:length(modifiers)) {
      data[[modifiers[[i]]]] = as.factor(data[[modifiers[[i]]]])
    } 
  }
  
  return(data)
}

readSettings <- function(settingsFile) {
  settings = read.csv(settingsFile, header=FALSE, as.is=TRUE, strip.white=TRUE)
  nsettings = nrow(settings)
  list = as.list(setNames(nm=settings$V1))
  for (i in 1:nsettings) {
    if (settings$V1[i] != "Endpoint") {
      element = unlist(strsplit(settings$V2[i], " "))
    } else {
      element = settings$V2[i]
    }
    isNumeric = suppressWarnings(!is.na(as.numeric(element[1])))
    if (isNumeric) {
      element = as.numeric(element)
    } else  {
      element = as.character(element)
    }
    list[[i]] = element
  }
  
  # Get transformed limits of concern
  if (list$MeasurementType != "Continuous") {
    list$TransformLocLower = log(list$LocLower)
    list$TransformLocUpper = log(list$LocUpper)
  } else {
    list$TransformLocLower = list$LocLower - list$OverallMean
    list$TransformLocUpper = list$LocUpper + list$OverallMean
  }

  return(list)
}

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
  modelSettings$formulaH1 <- as.formula(paste("Response ~ ", paste(nameH1, collapse="+")))
  modelSettings$formulaH0 <- update(modelSettings$formulaH1, ~ . - GMO)
  modelSettings$formulaH0_low <- update(modelSettings$formulaH0, ~ . + offset(LowOffset))
  modelSettings$formulaH0_upp <- update(modelSettings$formulaH0, ~ . + offset(UppOffset))

  # Test settings
  modelSettings$SignificanceLevel = settings$SignificanceLevel
  modelSettings$nGCI = 100  # Number of draws for Generalized Confidence Interval
  modelSettings$smallGCI = 0.0001  # Bound on back-transformed values for the LOG-transform
  
  return(modelSettings)
}

createSimulationSettings <- function(settings) {
  simulationSettings <- list()
  
  # Define dispersion parameter
  simulationSettings$dispersion <- ropoissonDispersion(settings$OverallMean, settings$CVComparator, settings$PowerLawPower, settings$Distribution)  
  
  # Add blocking effect to model 
  if (settings$ExperimentalDesignType == "RandomizedCompleteBlocks") {
    simulationSettings$sigBlock <- sqrt(log((settings$CVBlocks/100)^2 + 1))
  } else {
    simulationSettings$sigBlock <- 0
  }
  
  return(simulationSettings)
}

createSimulatedDataTemplate <- function(data, settings, simulationSettings, blocks) {
  setupSimulatedData <- data
  
  # Expand dataset and modify Block factor  
  setupSimulatedData <- data[rep(row.names(data), blocks + 0*data[[1]]),]
  setupSimulatedData[["Block"]] <- as.factor(rep(1:blocks, nrow(data)))
  
  # Add several additional columns to the dataframe
  setupSimulatedData["TransformedMean"] <- LinkFunction(data["Mean"], settings$MeasurementType)
  
  # Apply blocking Effect on the transformed scale; use Blom scores
  if (settings$ExperimentalDesignType == "RandomizedCompleteBlocks") {
    blockeff = simulationSettings$sigBlock * qnorm(((1:blocks) - 0.375)/ (blocks + 0.25))
    setupSimulatedData["BlockEffect"] <- blockeff[setupSimulatedData[["Block"]]]
  } else {
    setupSimulatedData["BlockEffect"] <- 0
  }
  setupSimulatedData["LowOffset"] <- setupSimulatedData["GMO"] * settings$TransformLocLower
  setupSimulatedData["UppOffset"] <- setupSimulatedData["GMO"] * settings$TransformLocUpper
  
  return(setupSimulatedData)
}

simulateData <- function(data, settings, simulationSettings, effect) {
  data["TransformedEffect"] <- data["TransformedMean"] + data["BlockEffect"] + (data["GMO"] == 1) * effect
  data["Effect"] <- InverseLinkFunction(data["TransformedEffect"], settings$MeasurementType)
  data["Response"] <- ropoisson(nrow(data), data[["Effect"]], simulationSettings$dispersion, settings$PowerLawPower, settings$Distribution)
  return(data)
}

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

logNormalAnalysis <- function(data, settings, modelSettings) {
  require(lsmeans)
  
  data["Response"] <- log(data["Response"] + 1)
  lmH1 <- lm(modelSettings$formulaH1, data=data)
  pval <- 2*pt(abs(lmH1$coef[2])/sqrt(vcov(lmH1)[2,2]), lmH1$df.residual, lower.tail=FALSE)
  resDF <- df.residual(lmH1)
  resMS <- deviance(lmH1)/resDF

  lsmeans <- lsmeans(lmH1, "GMO", at=modelSettings$preddata)
  meanCMP <- summary(lsmeans)$lsmean[1]
  meanGMO <- summary(lsmeans)$lsmean[2]
  repCMP <- resMS / (summary(lsmeans)$SE[1]^2)
  repGMO <- resMS / (summary(lsmeans)$SE[2]^2)

  # Generalized confidence interval
  chi  <- resDF * resMS / rchisq(modelSettings$nGCI, resDF)
  rCMP <- rnorm(modelSettings$nGCI, meanCMP, sqrt(chi/repCMP))
  rGMO <- rnorm(modelSettings$nGCI, meanGMO, sqrt(chi/repGMO))
  rCMP <- exp(rCMP + chi/2) - 1
  rGMO <- exp(rGMO + chi/2) - 1
  rCMP[rCMP < modelSettings$smallGCI] <- modelSettings$smallGCI
  rGMO[rGMO < modelSettings$smallGCI] <- modelSettings$smallGCI
  ratio <- rGMO/rCMP

  # For very small draws from the Chi-Distribution rCMP and rGMO can be out of bounds
  quantiles <- quantile(ratio, c(modelSettings$SignificanceLevel/2, 1 - modelSettings$SignificanceLevel / 2), na.rm=TRUE)
  
  pvalues <- list()
  pvalues$Diff <- as.numeric(pval)
  pvalues$Equi <- 1 - as.numeric((quantiles[1] > settings$LocLower) & (quantiles[2] < settings$LocUpper))
  pvalues$EquiWD <- NA
  
  return(pvalues)
}

squareRootAnalysis <- function(data, settings, modelSettings) {
  require(lsmeans)

  data["Response"] <- sqrt(data["Response"])
  lmH1 <- lm(modelSettings$formulaH1, data=data)
  pval <- 2*pt(abs(lmH1$coef[2])/sqrt(vcov(lmH1)[2,2]), lmH1$df.residual, lower.tail=FALSE)
  resDF <- df.residual(lmH1)
  resMS <- deviance(lmH1)/resDF

  lsmeans <- lsmeans(lmH1, "GMO", at=modelSettings$preddata)
  meanCMP <- summary(lsmeans)$lsmean[1]
  meanGMO <- summary(lsmeans)$lsmean[2]
  repCMP <- resMS / (summary(lsmeans)$SE[1]^2)
  repGMO <- resMS / (summary(lsmeans)$SE[2]^2)
  
  # Generalized confidence interval
  chi  <- resDF * resMS / rchisq(modelSettings$nGCI, resDF)
  rCMP <- rnorm(modelSettings$nGCI, meanCMP, sqrt(chi/repCMP))
  rGMO <- rnorm(modelSettings$nGCI, meanGMO, sqrt(chi/repGMO))
  rCMP <- rCMP*rCMP + chi
  rGMO <- rGMO*rGMO + chi
  ratio <- rGMO/rCMP
  
  # For very small draws from the Chi-Distribution rCMP and rGMO can be out of bounds
  quantiles <- quantile(ratio, c(modelSettings$SignificanceLevel/2, 1-modelSettings$SignificanceLevel/2), na.rm=TRUE)
  
  pvalues <- list()
  pvalues$Diff <- as.numeric(pval)
  pvalues$Equi <- 1 - as.numeric((quantiles[1] > settings$LocLower) & (quantiles[2] < settings$LocUpper))
  pvalues$EquiWD <- NA
  
  return(pvalues)
}

overdispersedPoissonAnalysis <- function(data, settings, modelSettings) {
  glmH0 <- glm(modelSettings$formulaH0, family="quasipoisson", data=data, mustart=data[["Response"]])
  glmH1 <- glm(modelSettings$formulaH1, family="quasipoisson", data=data, mustart=data[["Response"]])

  # Prepare results list
  pvalues <- list()
  
  # Difference test
  df1 <- df.residual(glmH1)
  estDispersion <- sum(residuals(glmH1,type="pearson")^2)/df1
  pval <- pf((deviance(glmH0) - deviance(glmH1))/estDispersion, 1, df1, lower.tail=FALSE)
  pvalues$Diff <- as.numeric(pval)
  
  #if (k == 1) {
    qt <- abs(qt(modelSettings$SignificanceLevel/2, df1, lower.tail=TRUE))
  #}

  # Equivalence test 
  estiEffect <- glmH1$coef[2]
  if ((estiEffect < settings$TransformLocLower) | (estiEffect > settings$TransformLocUpper)) {
    pvalues$Equi <- 1
    pvalues$EquiWD <- 1
  } else {
    # LR equivalence test
    glmH0low <- glm(modelSettings$formulaH0_low, family="quasipoisson", data=data, mustart=data[["Response"]])
    glmH0upp <- glm(modelSettings$formulaH0_upp, family="quasipoisson", data=data, mustart=data[["Response"]])
    pvalLow <- pf((deviance(glmH0low) - deviance(glmH1))/estDispersion, 1, df1, lower.tail=FALSE)
    pvalUpp <- pf((deviance(glmH0upp) - deviance(glmH1))/estDispersion, 1, df1, lower.tail=FALSE)
    pvalues$Equi <- max(pvalLow, pvalUpp)

    # Wald equivalence test
    seEffect <- sqrt(vcov(glmH1)[2,2])
    lower <- estiEffect - qt * seEffect
    upper <- estiEffect + qt * seEffect
    pvalues$EquiWD <- 1 - as.numeric((lower > settings$TransformLocLower) & (upper < settings$TransformLocUpper))
  }
  
  return(pvalues)
}

negativeBinomialAnalysis <- function(data, settings, modelSettings) {
  require(MASS)

  glmH0 <- glm.nb(modelSettings$formulaH0, data=data, link=log, mustart=data[["Response"]])
  glmH1 <- glm.nb(modelSettings$formulaH1, data=data, link=log, mustart=data[["Response"]])
  pval <- pchisq(-2*(logLik(glmH0) - logLik(glmH1)), 1, lower.tail=FALSE)

  pvalues <- list()  
  pvalues$Diff <- as.numeric(pval)

  estiEffect <- glmH1$coef[2]
  if ((estiEffect < settings$TransformLocLower) | (estiEffect > settings$TransformLocUpper)) {
    pvalues$Equi <- 1
  } else {
    glmH0low <- glm.nb(modelSettings$formulaH0_low, data=data, link=log, mustart=data[["Response"]])
    glmH0upp <- glm.nb(modelSettings$formulaH0_upp, data=data, link=log, mustart=data[["Response"]])
    pvalLow <- pchisq(deviance(glmH0low) - deviance(glmH1), 1, lower.tail=FALSE)
    pvalUpp <- pchisq(deviance(glmH0upp) - deviance(glmH1), 1, lower.tail=FALSE)
    pval <- max(pvalLow, pvalUpp)
    pvalues$Equi <- as.numeric(pval)
  }
  pvalues$EquiWD <- NA
  
  return(pvalues)
}

monteCarloPowerAnalysis <- function(data, settings, modelSettings, blocks, effect) {
  nanalysis = length(settings$AnalysisMethods)
  nrow = settings$NumberOfSimulatedDataSets
  pValues = list(
    Diff   = matrix(nrow=nrow, ncol=nanalysis, dimnames=list(NULL, settings$AnalysisMethods)),
    Equi = matrix(nrow=nrow, ncol=nanalysis, dimnames=list(NULL, settings$AnalysisMethods)),
    EquiWD = matrix(nrow=nrow, ncol=nanalysis, dimnames=list(NULL, settings$AnalysisMethods)))
  
  # Setup simulation settings
  simulationSettings <- createSimulationSettings(settings)
  simulatedDataTemplate <- createSimulatedDataTemplate(data, settings, simulationSettings, blocks);
  
  # Do looping over simulations 
  for (k in 1:settings$NumberOfSimulatedDataSets) {
    simulatedData <- simulateData(simulatedDataTemplate, settings, simulationSettings, effect)
    if (!is.na(match("LogNormal", settings$AnalysisMethods))) {
      result <- logNormalAnalysis(simulatedData, settings, modelSettings)
      pValues$Diff[k, "LogNormal"] <- result$Diff
      pValues$Equi[k, "LogNormal"] <- result$Equi
      pValues$EquiWD[k, "LogNormal"] <- result$EquiWD
    }
    if (!is.na(match("SquareRoot", settings$AnalysisMethods))) {
      result <- squareRootAnalysis(simulatedData, settings, modelSettings)
      pValues$Diff[k, "SquareRoot"] <- result$Diff
      pValues$Equi[k, "SquareRoot"] <- result$Equi
      pValues$EquiWD[k, "SquareRoot"] <- result$EquiWD
    }
    if (!is.na(match("OverdispersedPoisson", settings$AnalysisMethods))) {
      result <- overdispersedPoissonAnalysis(simulatedData, settings, modelSettings)
      pValues$Diff[k, "OverdispersedPoisson"] <- result$Diff
      pValues$Equi[k, "OverdispersedPoisson"] <- result$Equi
      pValues$EquiWD[k, "OverdispersedPoisson"] <- result$EquiWD
    }
    if (!is.na(match("NegativeBinomial", settings$AnalysisMethods))) {
      result <- negativeBinomialAnalysis(simulatedData, settings, modelSettings)
      pValues$Diff[k, "NegativeBinomial"] <- result$Diff
      pValues$Equi[k, "NegativeBinomial"] <- result$Equi
      pValues$EquiWD[k, "NegativeBinomial"] <- result$EquiWD
    }
  }
  return(pValues)
}

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
    EquiWD = matrix(nrow=nrow, ncol=nanalysis, dimnames=list(NULL, paste("EquiWD", settings$AnalysisMethods, sep=""))))
  
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
  
  print(df)
  return(df)
}
