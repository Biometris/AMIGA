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
# 6. A onesided equivalence test with alfa/2 need NOT to be equivalent with a twosided 
#     equivalence test with alfa. This is because BOTH hypotheses must be rejected.
#     Consider the case in which the deviance differences for the two-sided test equals
#     4.51 and 2.42. The first has p-value 0.017, the second 0.051. So the two-sided
#     equivalence test is not rejected, while one of the onesided tests is rejected.
#     Note that two- and onesided difference tests are equivalent.
#

######################################################################################################################
# Link function for the different measurementTypes 
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
# Inverse of link function for the different measurementTypes 
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
# returns random draws from a Count distributions
ropoisson <- function(n, mean, dispersion=NaN, power=NaN, 
        distribution=c("Poisson", "OverdispersedPoisson", "NegativeBinomial", "PoissonLogNormal", "PowerLaw"), 
        excessZero=0.0) {
  stopifnot(n == as.integer(n), n >= 1, mean >= 0)
  if ((length(mean) != 1) && (length(mean) != n)) {
    stop("The length of mean must equal 1 or the value of n.", call. = FALSE)
  }
  if ((length(dispersion) != 1) && (length(dispersion) != n)) {
    stop("The length of dispersion must equal 1 or the value of n.", call. = FALSE)
  }
  if ((excessZero < 0.0) || (excessZero >= 100.0)) {
    stop("The ExcessZero parameter must be a percentage in the interval [0,100).", call. = FALSE)
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
  } else if (type == "PowerLaw") { # Powerlaw by negative binomial
    if (min(dispersion) <= 0) {
      stop("The dispersion parameter must be larger than 0 for the PowerLaw distribution.", call. = FALSE)
    }
    if ((power < 1) || (power > 2)) {
      stop("The power parameter must be interval [1,2].", call. = FALSE)
    }
    dispNegbin = (dispersion*mean^power - mean)/(mean*mean)
    if (min(dispNegbin) <= 0) { # use Poisson as limiting distribution
      sample <- rpois(n, mean)
    } else { # Use negative binomial distribution
      s <- dispNegbin*mean
      a <- mean/s
      sample <- rgamma(n, shape=a, scale=s)
      sample <- rpois(n, sample)
    }
  }
  # Add excess Zero
  if (excessZero > 0.0) {
    zero <- 100*runif(n)
    sample[zero<excessZero] <- 0
  }
  return(sample)
}

######################################################################################################################
# returns random draws from a Nonnegative distributions
rnonnegative <- function(n, mean, cv, distribution=c("LogNormal", "Gamma")) {
  stopifnot(n == as.integer(n), n >= 1, mean >= 0)
  if ((length(mean) != 1) && (length(mean) != n)) {
    stop("The length of mean must equal 1 or the value of n.", call. = FALSE)
  }
  if ((length(cv) != 1) && (length(cv) != n)) {
    stop("The length of dispersion must equal 1 or the value of n.", call. = FALSE)
  }
  if (min(cv) <= 0) {
    stop("The cv must be positive.", call. = FALSE)
  }
  mean[mean==0] <- 1.0e-200
  if (min(mean) < 0) {
    stop("The mean must be positive.", call. = FALSE)
  }
  type = match.arg(distribution)
  if (type == "LogNormal") {
    dispersion <- log((cv/100)^2 + 1)
    logmean    <- log(mean) - dispersion/2
    sample <- rlnorm(n, logmean , sqrt(dispersion))
  } else if (type == "Gamma") {
    shape  <- (100/cv)^2
    scale  <- mean/shape
    sample <- rgamma(n, shape=shape, scale=scale)
  }
  return(sample)
}

######################################################################################################################
# returns the variance for a Count distributions
ropoissonVariance <- function(n, mean, dispersion=NaN, power=NaN, 
    distribution=c("Poisson", "OverdispersedPoisson", "NegativeBinomial", "PoissonLogNormal", "PowerLaw")) {
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
# returns the Dispersion parameter for a Count distributions
ropoissonDispersion <- function(mean, CV, power, 
    distribution=c("Poisson", "OverdispersedPoisson", "NegativeBinomial", "PoissonLogNormal", "PowerLaw", "Normal", "LogNormal")) {
  if ((mean <= 0) && (distribution != "Normal")) {
    stop("Mean of distribution must be positive.", call. = FALSE)
  }
  if (CV <= 0) {
    stop("Coefficient of Variation must be positive.", call. = FALSE)
  }
  
  type = match.arg(distribution)
  if (type == "PowerLaw") {
    if ((power < 1) || (power > 2)) {
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
  } else if (type == "PowerLaw") {  # The value of alpha in the variance function
    dispersion <- (CV/100) * (CV/100) * mean^(2-power)
  } else if (type == "Normal") {
    dispersion <- ((CV/100) * mean)^2
  } else if (type == "LogNormal") { # Variance on the log-scale!
    dispersion <- log((CV/100)^2 + 1)
  } else {
    stop(paste0("Distribution '", distribution, "' not implemented in function ropoissonDispersion."), call.=FALSE)
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
  # Sub dataframe: First check that essential columns are present
  if (is.element("Comparison", colnames)) {
    colComparison <- max(grep("Comparison", colnames))
  } else {
    stop("The DataFile must contain a column 'Comparison'.", call. = FALSE)
  }
  if (is.element("Constant", colnames)) {
    colConstant <- max(grep("Constant", colnames))
  } else {
    stop("The DataFile must contain a column 'Constant'.", call. = FALSE)
  }
  if (is.element("Test", colnames)) {
    colTest <- max(grep("Test", colnames))
  } else {
    stop("The DataFile must contain a column 'Test'.", call. = FALSE)
  }
  if (((colComparison+1) != colConstant) || ((colConstant+1) != colTest)) {
    stop("The DataFile must contain the sequence of columns Comparison, Constant and Test.", call. = FALSE)
  }
  # Return columns from Test onwards
  ncolnames = length(colnames)
  return(data[rep(colConstant:ncolnames)])
}

######################################################################################################################
readSettings <- function(settingsFile) {
  settings <- read.csv(settingsFile, header=FALSE, as.is=TRUE, strip.white=TRUE, na.strings=c("NA","NaN"))
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

  # Get transformed limits of concern taking account of one/twosided
  if ((is.na(list$LocLower)) && (is.na(list$LocUpper))) {
    stop("Both LocLower and LocUpper are missing; this is not allowed.", call. = FALSE)
  }
  list$TestType = "twosided"
  if (is.na(list$LocLower)) {
    list$TransformLocLower <- NA
    list$TestType = "right"
  } else {
    if (list$MeasurementType != "Continuous") {
      list$TransformLocLower <- log(list$LocLower)
    } else {
      list$TransformLocLower <- list$LocLower*list$OverallMean - list$OverallMean
    }
  }
  if (is.na(list$LocUpper)) {
    list$TransformLocUpper <- NA
    list$TestType = "left"
  } else {
    if (list$MeasurementType != "Continuous") {
      list$TransformLocUpper <- log(list$LocUpper)
    } else {
      list$TransformLocUpper = list$LocUpper*list$OverallMean - list$OverallMean
    }
  }

  #  Redefine some to logical 
  isLogical <- c("UseWaldTest","IsOutputSimulatedData")
  match = match(isLogical, names(list))
  for (imatch in match) {
    if (list[[imatch]] == "True") {
      list[[imatch]] = TRUE
    } else {
      list[[imatch]] = FALSE
    }
  }

  # Add directory to list
  list$directory = paste0(dirname(settingsFile), "/")

  # Add additional settings (previously simulationSettings)
  list$dispersion <- ropoissonDispersion(list$OverallMean, list$CVComparator, list$PowerLawPower, list$Distribution)
  if (list$ExperimentalDesignType == "CompletelyRandomized") {
    list$sigBlock <- 0
  } else {
    list$sigBlock <- sqrt(log((list$CVBlocks/100)^2 + 1))
  }

  # Add extra settings
  list$AnalysisMethods <- unique(c(list$AnalysisMethodsDifferenceTests, list$AnalysisMethodsEquivalenceTests))
  list$DoLNdiff = !is.na(match("LogNormal",            list$AnalysisMethodsDifferenceTests))
  list$DoSQdiff = !is.na(match("SquareRoot",           list$AnalysisMethodsDifferenceTests))
  list$DoOPdiff = !is.na(match("OverdispersedPoisson", list$AnalysisMethodsDifferenceTests))
  list$DoNBdiff = !is.na(match("NegativeBinomial",     list$AnalysisMethodsDifferenceTests))
  list$DoNOdiff = !is.na(match("Normal",               list$AnalysisMethodsDifferenceTests))
  list$DoLOdiff = !is.na(match("LogPlusM",             list$AnalysisMethodsDifferenceTests))
  list$DoGMdiff = !is.na(match("Gamma",                list$AnalysisMethodsDifferenceTests))
  list$DoLNequi = !is.na(match("LogNormal",            list$AnalysisMethodsEquivalenceTests))
  list$DoSQequi = !is.na(match("SquareRoot",           list$AnalysisMethodsEquivalenceTests))
  list$DoOPequi = !is.na(match("OverdispersedPoisson", list$AnalysisMethodsEquivalenceTests))
  list$DoNBequi = !is.na(match("NegativeBinomial",     list$AnalysisMethodsEquivalenceTests))
  list$DoNOequi = !is.na(match("Normal",               list$AnalysisMethodsEquivalenceTests))
  list$DoLOequi = !is.na(match("LogPlusM",             list$AnalysisMethodsEquivalenceTests))
  list$DoGMequi = !is.na(match("Gamma",                list$AnalysisMethodsEquivalenceTests))
  return(list)
}

######################################################################################################################
createModelSettings <- function(data, settings) {
  modelSettings <- list()
  modelSettings$ndata = nrow(data)
  colnames <- colnames(data)

  # Create dataframe for prediction; only use Test column and Dummy columns
  # This implies that the mean is taken over blocks and also over modifiers (if any)
  predictors = grep("Test", gsub("Dummy", "Test", colnames))
  modelSettings$preddata <- head(data[predictors], 2)
  modelSettings$preddata[,] <- 0
  modelSettings$preddata[1] <- c(0,1)

  # Model formulas for H0 and H1; Note that the Test column is used to test the comparison
  nterms <- grep("Mean", colnames) - 1
  nameH1 <- colnames[c(2:nterms)]
  if (settings$ExperimentalDesignType != "CompletelyRandomized") {
    nameH1 <- c(nameH1, "Block") # Add blocking effect
  }

  # Define models to fit
  modelSettings$formulaH1 <- as.formula(paste0("Response ~ ", paste0(nameH1, collapse=" + ")))
  modelSettings$formulaH0 <- update(modelSettings$formulaH1, ~ . - Test)
  modelSettings$formulaH0_low <- update(modelSettings$formulaH0, ~ . + offset(LowOffset))
  modelSettings$formulaH0_upp <- update(modelSettings$formulaH0, ~ . + offset(UppOffset))
  # Due to an apparent bug in the LSMEANS package formulaH1 is saved as a string and fitted by using as.formula()
  modelSettings$formulaH1 <- paste0("Response ~ ", paste0(nameH1, collapse=" + "))

  # Additional settings
  modelSettings$smallGCI = 0.0001       # Bound on back-transformed values for the LOG-transform

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

  # Add blocking effect to model 
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    simulationSettings$sigBlock <- 0
  } else {
    simulationSettings$sigBlock <- sqrt(log((settings$CVBlocks/100)^2 + 1))
  }
  return(simulationSettings)
}

######################################################################################################################
createSimulatedDataOld <- function(data, settings, blocks, effect) {
  setupSimulatedData <- data
  
  # Expand dataset; add Row and Block factor
  setupSimulatedData <- data[rep(row.names(data), blocks + 0*data[[1]]),]
  setupSimulatedData[["Row"]] <- as.factor(c(1:(blocks*nrow(data))))
  setupSimulatedData[["Block"]] <- as.factor(rep(1:blocks, nrow(data)))
  
  # Add several additional columns to the dataframe
  setupSimulatedData["TransformedMean"] <- linkFunction(data["Mean"], settings$MeasurementType)
  
  # Apply blocking Effect on the transformed scale; use Blom scores
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    setupSimulatedData["BlockEffect"] <- 0
  } else {
    blockeff = settings$sigBlock * qnorm(((1:blocks) - 0.375)/ (blocks + 0.25))
    setupSimulatedData["BlockEffect"] <- blockeff[setupSimulatedData[["Block"]]]
  }
  setupSimulatedData["LowOffset"] <- setupSimulatedData["Test"] * settings$TransformLocLower
  setupSimulatedData["UppOffset"] <- setupSimulatedData["Test"] * settings$TransformLocUpper
  
  # Add extra columns which are used to simulate and fit data
  setupSimulatedData["TransformedEffect"] <- setupSimulatedData["TransformedMean"] + setupSimulatedData["BlockEffect"] + (setupSimulatedData["Test"] == 1) * effect
  setupSimulatedData["Effect"] <- inverseLinkFunction(setupSimulatedData["TransformedEffect"], settings$MeasurementType)
  setupSimulatedData["Response"]          <- setupSimulatedData["Test"] * NaN
  setupSimulatedData["Lp"]          <- setupSimulatedData["Test"] * NaN
  return(setupSimulatedData)
}

######################################################################################################################
createSimulatedData <- function(data, settings, blocks, effect) {

  # Expand dataset and modify rownames
  ndata = nrow(data)  
  setupSimulatedData <- untable(data, num=blocks)
  f1 <- rep(c(1:ndata), blocks)
  f2 <- rep(c(1:blocks), each=ndata)
  rownames <- paste0(str_pad(f2, 1+log10(blocks+0.001), side="left", "0"), "-", str_pad(f1, 1+log10(ndata+0.001), side="left", "0"))
  row.names(setupSimulatedData) <- rownames

  # add Row and Block factors
  setupSimulatedData[["Row"]] <- as.factor(c(1:(blocks*ndata)))
  setupSimulatedData[["Block"]] <- as.factor(rep(1:blocks, each=ndata))
  
  # Add several additional columns to the dataframe
  transformedMean <- linkFunction(setupSimulatedData["Mean"], settings$MeasurementType)
  
  # Apply blocking Effect on the transformed scale; use Blom scores
  if (settings$ExperimentalDesignType == "CompletelyRandomized") {
    blockEffect <- 0
  } else {
    blomScores <- settings$sigBlock * qnorm(((1:blocks) - 0.375)/ (blocks + 0.25))
    blockEffect <- blomScores[setupSimulatedData[["Block"]]]
  }
  setupSimulatedData["LowOffset"] <- setupSimulatedData["Test"] * settings$TransformLocLower
  setupSimulatedData["UppOffset"] <- setupSimulatedData["Test"] * settings$TransformLocUpper
  
  # Add extra columns which are used to simulate and fit data
  setupSimulatedData["Lp"] <- transformedMean + blockEffect + (setupSimulatedData["Test"] == 1) * effect
  setupSimulatedData["Effect"] <- inverseLinkFunction(setupSimulatedData["Lp"], settings$MeasurementType)
  setupSimulatedData["Response"]    <- setupSimulatedData["Test"] * NaN
  if (FALSE) {
    cat(paste0("\neffect: ", effect, "\n"))
    print(data)
    cat("\n")
    print(setupSimulatedData)
    cat("\n")
  }
  return(setupSimulatedData)
}

######################################################################################################################
simulateResponse <- function(simulatedData, settings) {
  if (settings$MeasurementType == "Count") {
    response <- ropoisson(nrow(simulatedData), simulatedData[["Effect"]], settings$dispersion, settings$PowerLawPower, settings$Distribution, settings$ExcessZeroesPercentage)
  } else if (settings$MeasurementType == "Nonnegative") {
    response <- rnonnegative(nrow(simulatedData), simulatedData[["Effect"]], settings$CVComparator, settings$Distribution)
  } else {
    stop(paste("MeasurementType", settings$MeasurementType, "in function simulateResponse is not implemented.", call. = FALSE))
  }
  return(response)
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
  estiEffect = as.numeric(lmH1$coef[2])
  seEffect = sqrt(vcov(lmH1)[2,2])
  if (settings$DoNOdiff) {
    pvalues$Diff <- waldDiffPvalue(estiEffect, seEffect, resDF, settings$TestType)
  }
  if (settings$DoNOequi) {
    pvalues$Equi <- waldEquiPvalue(estiEffect, seEffect, resDF, settings$TestType, settings$TransformLocLower, settings$TransformLocUpper)
  }
  return(pvalues)

  # Due to the return above this code is not executed
  # It shows that the CGI approach is equivalent to the usual approach in simple situations
  # Obtain predicted means for CMP/Test and corresponding VCOV
  lsmeans <- lsmeans(lmH1, "Test", at=modelSettings$preddata)
  meanCMP <- summary(lsmeans)$lsmean[1]
  meanTST <- summary(lsmeans)$lsmean[2]
  vcovLS = vcov(lsmeans)

  # GCI; this takes account of a possible covariance between predictions
  chi  <- resDF * resMS / rchisq(settings$NumberOfSimulationsGCI, resDF)
  random = mvrnorm(settings$NumberOfSimulationsGCI, c(0,0), vcovLS)*sqrt(chi/resMS)
  random[,1] = meanCMP + random[,1]
  random[,2] = meanTST + random[,2]
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

  # Fit model; must be done for DIFF and EQUI tests
  data["Response"] <- log(data["Response"] + 1)
  lmH1 <- lm(as.formula(modelSettings$formulaH1), data=data)
  resDF <- lmH1$df.residual
  resMS <- deviance(lmH1)/resDF
  if (settings$DoLNdiff) {
    estiEffect = as.numeric(lmH1$coef[2])
    seEffect = sqrt(vcov(lmH1)[2,2])
    pvalues$Diff <- waldDiffPvalue(estiEffect, seEffect, resDF, settings$TestType)
  }

  if ((settings$NumberOfSimulationsGCI > 0) && (settings$DoLNequi)) {
    # Obtain predicted means for CMP/Test and corresponding VCOV
    lsmeans <- lsmeans(lmH1, "Test", at=modelSettings$preddata)
    meanCMP <- summary(lsmeans)$lsmean[1]
    meanTST <- summary(lsmeans)$lsmean[2]
    vcovLS = vcov(lsmeans)
    # GCI; this takes account of a possible covariance between predictions
  	# In case any of the draws is Inf (after exponentiation) the corresponding ratio is set to NaN and the pvalue is also set to NaN
    chi  <- resDF * resMS / rchisq(settings$NumberOfSimulationsGCI, resDF)
    random = mvrnorm(settings$NumberOfSimulationsGCI, c(0,0), vcovLS)*sqrt(chi/resMS)
    random[,1] = meanCMP + random[,1]
    random[,2] = meanTST + random[,2]
    random <- exp(random + chi/2) - 1
    random[random < modelSettings$smallGCI] <- modelSettings$smallGCI
    ratio <- random[,2]/random[,1]
    ratio[(random[,1]==Inf) || (random[,2]==Inf)] = NaN
    pvalues$Equi = cgiEquiPvalue(ratio, settings$LocLower, settings$LocUpper, settings$TestType)
  }
  return(pvalues)

  # Originl code for Generalized confidence interval
  # seCMP = summary(lsmeans)$SE[1]
  # seTST = summary(lsmeans)$SE[2]
  # repCMP <- resMS / (seCMP^2)
  # repTST <- resMS / (seTST^2)
  # chi  <- resDF * resMS / rchisq(settings$NumberOfSimulationsGCI, resDF)
  # rCMP <- rnorm(settings$NumberOfSimulationsGCI, meanCMP, sqrt(chi/repCMP))
  # rTST <- rnorm(settings$NumberOfSimulationsGCI, meanTST, sqrt(chi/repTST))
  # rCMP <- exp(rCMP + chi/2) - 1
  # rTST <- exp(rTST + chi/2) - 1
  # rCMP[rCMP < modelSettings$smallGCI] <- modelSettings$smallGCI
  # rTST[rTST < modelSettings$smallGCI] <- modelSettings$smallGCI
  # ratio <- rTST/rCMP
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
  if (settings$DoSQdiff) {
    estiEffect = as.numeric(lmH1$coef[2])
    seEffect = sqrt(vcov(lmH1)[2,2])
    pvalues$Diff <- waldDiffPvalue(estiEffect, seEffect, resDF, settings$TestType)
  }

  if ((settings$NumberOfSimulationsGCI > 0) && (settings$DoSQequi)) {
    # Obtain predicted means for CMP/Test and corresponding VCOV
    lsmeans <- lsmeans(lmH1, "Test", at=modelSettings$preddata)
    meanCMP <- summary(lsmeans)$lsmean[1]
    meanTST <- summary(lsmeans)$lsmean[2]
    vcovLS = vcov(lsmeans)
    # GCI; this takes account of a possible covariance between predictions
    chi  <- resDF * resMS / rchisq(settings$NumberOfSimulationsGCI, resDF)
    random = mvrnorm(settings$NumberOfSimulationsGCI, c(0,0), vcovLS)*sqrt(chi/resMS)
    random[,1] = meanCMP + random[,1]
    random[,2] = meanTST + random[,2]
    random <- random*random + chi
    ratio <- random[,2]/random[,1]
    ratio[ratio==Inf] = NaN
    pvalues$Equi = cgiEquiPvalue(ratio, settings$LocLower, settings$LocUpper, settings$TestType)
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
  glmH1 <- glm(as.formula(modelSettings$formulaH1), family=family, data=data, etastart=Lp)
  #print(paste0("Number of iteration ", glmH1$iter))
  resDF <- df.residual(glmH1)
  estDispersion <- summary(glmH1)$dispersion
  estiEffect <- as.numeric(glmH1$coef[2])
  seEffect <- sqrt(vcov(glmH1)[2,2])
  if (estDispersion <= limitDispersion) {
    seEffect <- seEffect / sqrt(estDispersion)
    estDispersion <- NaN
    family <- "poisson"
    resDF  <- -1   # ensures using the Normal and Chi-squared instead of Student and Fisher
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
    if (settings$DoOPdiff) {
      pvalues$Diff <- waldDiffPvalue(estiEffect, seEffect, resDF, settings$TestType)
    }
    if (settings$DoOPequi) {
      pvalues$Equi <- waldEquiPvalue(estiEffect, seEffect, resDF, settings$TestType, settings$TransformLocLower, settings$TransformLocUpper)
    }
  } else {
    # Results based on LR test; denominator based on Pearson statistic
    data[["Lp"]] <- glmH1$linear.predictor
    if (settings$DoOPdiff) {
      if (lrDiffDo(estiEffect, 0, settings$TestType)) {
        glmH0 <- glm(modelSettings$formulaH0, family=family, data=data, etastart=Lp)
        devDiff = deviance(glmH0) - deviance(glmH1)
        pvalues$Diff <- lrDiffPvalue(devDiff, estDispersion, resDF, settings$TestType)
      } else {
        pvalues$Diff <- 2
      }
    }
    # and LR equivalence test
    if (settings$DoOPequi) {
      if (lrEquiDo(estiEffect, settings$TransformLocLower, settings$TransformLocUpper, settings$TestType)) {
        # LR equivalence test
        if ((settings$TestType == "twosided") || (settings$TestType == "left")) {
          glmH0low <- glm(modelSettings$formulaH0_low, family=family, data=data, etastart=Lp)
          devDiffLower <- deviance(glmH0low) - deviance(glmH1)
        } else {
          devDiffLower <- NA
        }
        if ((settings$TestType == "twosided") || (settings$TestType == "right")) {
          glmH0upp <- glm(modelSettings$formulaH0_upp, family=family, data=data, etastart=Lp)
          devDiffUpper <- deviance(glmH0upp) - deviance(glmH1)
        } else {
          devDiffUpper <- NA
        }
        pvalues$Equi <- lrEquiPvalue(devDiffLower, devDiffUpper, estDispersion, resDF, settings$TestType)
      } else {
        pvalues$Equi <- 2
      }
    }
  }
  return(pvalues)
}

######################################################################################################################
negativeBinomialAnalysis <- function(data, settings, modelSettings, debugSettings) {
  require(MASS)

  # Prepare results list and fit H1
  pvalues <- list(Diff = c(NaN), Equi = c(NaN), Dispersion=c(NaN))
  glmH1 <- fitNB(as.formula(modelSettings$formulaH1), data, etastart=Lp, method=modelSettings$NBmethod, lower=modelSettings$NBlower, upper=modelSettings$NBupper)
  # glmH1 <- glm.nb(as.formula(modelSettings$formulaH1), data=data, link=log, etastart=Lp)
  resDF <- df.residual(glmH1)
  pvalues$Dispersion = glmH1$theta
  estiEffect <- as.numeric(glmH1$coef[2])
  seEffect <- sqrt(vcov(glmH1)[2,2])
  if (settings$IsOutputSimulatedData) {
    writeLines(paste0("\nNB iRep ", debugSettings$iRep, " iEffect ", debugSettings$iEffect, " Dataset ", debugSettings$iDataset), debugSettings$displayFile)
    writeLines(paste0("  dispersion: ", glmH1$theta), debugSettings$displayFile)
    writeLines(paste0("  estiEffect: ", estiEffect), debugSettings$displayFile)
    writeLines(paste0("  seEffect:   ", seEffect), debugSettings$displayFile)
  }
  if (settings$UseWaldTest) {  
    # Results based on Wald tests
    if (settings$DoNBdiff) {
      pvalues$Diff <- waldDiffPvalue(estiEffect, seEffect, resDF, settings$TestType)
    }
    if (settings$DoNBequi) {
      pvalues$Equi <- waldEquiPvalue(estiEffect, seEffect, resDF, settings$TestType, settings$TransformLocLower, settings$TransformLocUpper)
    }
  } else {
    # Results based on LR test
    data[["Lp"]] = glmH1$linear.predictor
    if (settings$DoNBdiff) {
      if (lrDiffDo(estiEffect, 0, settings$TestType)) {
        glmH0 <- fitNB(modelSettings$formulaH0, data, etastart=Lp, method=modelSettings$NBmethod, lower=modelSettings$NBlower, upper=modelSettings$NBupper)
        # glmH0 <- glm.nb(modelSettings$formulaH0, data=data, link=log, etastart=etastart)
        # cat(paste0("LikH0: ", round(10000*as.numeric(logLik(glmH0)))/10000, "   LikH1: ", round(10000*as.numeric(logLik(glmH0)))/10000))
        devDiff = -2 * as.numeric(logLik(glmH0) - logLik(glmH1))
        pvalues$Diff <- lrDiffPvalue(devDiff, 1, resDF, settings$TestType)
      } else {
        pvalues$Diff <- 2
      }
    }
    # and LR equivalence test
    if (settings$DoNBequi) {
      if (lrEquiDo(estiEffect, settings$TransformLocLower, settings$TransformLocUpper, settings$TestType)) {
        # LR equivalence test
        if ((settings$TestType == "twosided") || (settings$TestType == "left")) {
          glmH0low <- fitNB(modelSettings$formulaH0_low, data, etastart=Lp, method=modelSettings$NBmethod, lower=modelSettings$NBlower, upper=modelSettings$NBupper)
          devDiffLower <- -2 * as.numeric(logLik(glmH0low) - logLik(glmH1))
        } else {
          devDiffLower <- NA
        }
        if ((settings$TestType == "twosided") || (settings$TestType == "right")) {
          glmH0upp <- fitNB(modelSettings$formulaH0_upp, data, etastart=Lp, method=modelSettings$NBmethod, lower=modelSettings$NBlower, upper=modelSettings$NBupper)
          devDiffUpper <- -2 * as.numeric(logLik(glmH0upp) - logLik(glmH1))
        } else {
          devDiffUpper <- NA
        }
        pvalues$Equi <- lrEquiPvalue(devDiffLower, devDiffUpper, 1, resDF, settings$TestType)
        # cat(paste0("\nDevLO: ", round(10000*devDiffLower)/10000, "   DevUP: ", round(10000*devDiffUpper)/10000, "\n\n"))
      } else {
        pvalues$Equi <- 2
        # cat(paste0("\nDevLO: ", NA, "   DevUP: ", NA, "\n\n"))
      }
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
gammaAnalysis <- function(data, settings, modelSettings, debugSettings) {
  # Prepare results list and fit H1; default method is DMETHOD=pearson
  pvalues <- list(Diff = c(NaN), Equi = c(NaN), Dispersion=c(NaN))
  glmH1 <- glm(as.formula(modelSettings$formulaH1), family=Gamma(link=log), data=data, etastart=Lp)
  #print(paste0("Number of iteration ", glmH1$iter))
  resDF <- glmH1$df.residual
  estDispersion <- summary(glmH1)$dispersion
  estiEffect <- as.numeric(glmH1$coef[2])
  seEffect <- sqrt(vcov(glmH1)[2,2]) 
  # estDispersion <- sum(residuals(glmH1,type="pearson")^2)/resDF
  pvalues$Dispersion <- estDispersion
  if (settings$IsOutputSimulatedData) {
    writeLines(paste0("\nGM iRep ", debugSettings$iRep, " iEffect ", debugSettings$iEffect, " Dataset ", debugSettings$iDataset), debugSettings$displayFile)
    writeLines(paste0("  dispersion: ", estDispersion), debugSettings$displayFile)
    writeLines(paste0("  estiEffect: ", estiEffect), debugSettings$displayFile)
    writeLines(paste0("  seEffect:   ", seEffect), debugSettings$displayFile)
  }

  if (settings$UseWaldTest) {  
    # Results based on Wald tests
    if (settings$DoGMdiff) {
      pvalues$Diff <- waldDiffPvalue(estiEffect, seEffect, resDF, settings$TestType)
    }
    if (settings$DoGMequi) {
      pvalues$Equi <- waldEquiPvalue(estiEffect, seEffect, resDF, settings$TestType, settings$TransformLocLower, settings$TransformLocUpper)
    }
  } else {
    # Results based on LR test; denominator based on Pearson statistic
    data[["Lp"]] <- glmH1$linear.predictor
    if (settings$DoGMdiff) {
      if (lrDiffDo(estiEffect, 0, settings$TestType)) {
        glmH0 <- glm(modelSettings$formulaH0, family=Gamma(link=log), data=data, etastart=Lp)
        devDiff = glmH0$deviance - glmH1$deviance
        pvalues$Diff <- lrDiffPvalue(devDiff, estDispersion, resDF, settings$TestType)
      } else {
        pvalues$Diff <- 2
      }
    }
    # and LR equivalence test
    if (settings$DoGMequi) {
      if (lrEquiDo(estiEffect, settings$TransformLocLower, settings$TransformLocUpper, settings$TestType)) {
        # LR equivalence test
        if ((settings$TestType == "twosided") || (settings$TestType == "left")) {
          glmH0low <- glm(modelSettings$formulaH0_low, family=Gamma(link=log), data=data, etastart=Lp)
          devDiffLower <- glmH0low$deviance - glmH1$deviance
        } else {
          devDiffLower <- NA
        }
        if ((settings$TestType == "twosided") || (settings$TestType == "right")) {
          glmH0upp <- glm(modelSettings$formulaH0_upp, family=Gamma(link=log), data=data, etastart=Lp)
          devDiffUpper <- glmH0upp$deviance - glmH1$deviance
        } else {
          devDiffUpper <- NA
        }
        pvalues$Equi <- lrEquiPvalue(devDiffLower, devDiffUpper, estDispersion, resDF, settings$TestType)
      } else {
        pvalues$Equi <- 2
      }
    }
  }
  return(pvalues)
}

######################################################################################################################
# Returns the pvalue for the CGI equivalence test
cgiEquiPvalue <- function(ratio, locLower, locUpper, testType) {
  if (testType == "twosided") {
    pLowerCGI = mean(ratio < locLower)  # If this is smaller than alfa: reject H0: esti < LocLower
    pUpperCGI = mean(ratio > locUpper)  # If this is smaller than alfa: reject H0: esti > LocUpper
    return(max(pLowerCGI, pUpperCGI))   # Both one-sided hypothesis must be rejected
  } else if (testType == "left") {
    return(mean(ratio < locLower))      # If this is smaller than alfa: reject H0: esti < LocLower
  } else { # right
    return(mean(ratio > locUpper))      # If this is smaller than alfa: reject H0: esti > LocUpper
  }
}

######################################################################################################################
# Returns the pvalue for the Wald Difference test 
waldDiffPvalue <- function(estiEffect, seEffect, resDF, testType) {
  if (resDF < 0) {
    # Normal distribution
    if (testType == "twosided") {
      return(2*pnorm(abs(estiEffect)/seEffect, lower.tail=FALSE))
    } else if (testType == "left") {
      return(pnorm(estiEffect/seEffect, lower.tail=TRUE))
    } else { # right
      return(pnorm(estiEffect/seEffect, lower.tail=FALSE))
    }
  } else {
    # Student distribution
    if (testType == "twosided") {
      return(2*pt(abs(estiEffect)/seEffect, resDF, lower.tail=FALSE))
    } else if (testType == "left") {
      return(pt(estiEffect/seEffect, resDF, lower.tail=TRUE))
    } else { # right
      return(pt(estiEffect/seEffect, resDF, lower.tail=FALSE))
    }
  }
}

######################################################################################################################
# Returns the pvalue for the Wald Equivalence test 
waldEquiPvalue <- function(estiEffect, seEffect, resDF, testType, locLower, locUpper) {
  if (resDF < 0) {
    # Normal distribution
    if (testType == "twosided") {
      pLowerEqui <- pnorm((estiEffect-locLower)/seEffect, lower.tail=FALSE)
      pUpperEqui <- pnorm((estiEffect-locUpper)/seEffect, lower.tail=TRUE)
      return(max(pLowerEqui, pUpperEqui)) # Both one-sided hypothesis must be rejected
    } else if (testType == "left") {
      return(pnorm((estiEffect-locLower)/seEffect, lower.tail=FALSE))
    } else { # right
      return(pnorm((estiEffect-locUpper)/seEffect, lower.tail=TRUE))
    }
  } else {
    # Student distribution
    if (testType == "twosided") {
      pLowerEqui <- pt((estiEffect-locLower)/seEffect, resDF, lower.tail=FALSE)
      pUpperEqui <- pt((estiEffect-locUpper)/seEffect, resDF, lower.tail=TRUE)
      return(max(pLowerEqui, pUpperEqui)) # Both one-sided hypothesis must be rejected
    } else if (testType == "left") {
      return(pt((estiEffect-locLower)/seEffect, resDF, lower.tail=FALSE))
    } else { # right
      return(pt((estiEffect-locUpper)/seEffect, resDF, lower.tail=TRUE))
    }
  }
}

######################################################################################################################
# Returns whether it is usefull to perform the Likelihood Ratio Difference test
lrDiffDo <- function(estiEffect, nullValue, testType) {
  if (testType == "twosided") {
    return(TRUE)
  } else if (testType == "left") {
    return(estiEffect < nullValue)
  } else { # right
    return(estiEffect > nullValue)
  }
}

######################################################################################################################
# Returns the pvalue for the Likelihood Ratio Difference test 
lrDiffPvalue <- function(devDiff, dispersion, resDF, testType) {
  if (resDF < 0) {
    # Normal distribution
    if (testType == "twosided") {
      return(pchisq(devDiff, 1, lower.tail=FALSE))
    } else if (testType == "left") {
      return(pchisq(devDiff, 1, lower.tail=FALSE)/2)
    } else { # right
      return(pchisq(devDiff, 1, lower.tail=FALSE)/2)
    }
  } else {
    # Student distribution
    if (testType == "twosided") {
      return(pf(devDiff/dispersion, 1, resDF, lower.tail=FALSE))
    } else if (testType == "left") {
      return(pf(devDiff/dispersion, 1, resDF, lower.tail=FALSE)/2)
    } else { # right
      return(pf(devDiff/dispersion, 1, resDF, lower.tail=FALSE)/2)
    }
  }
}

######################################################################################################################
# Returns whether it is usefull to perform the Likelihood Ratio Equivalence test
lrEquiDo <- function(estiEffect, locLower, locUpper, testType) {
  if (testType == "twosided") {
    return((estiEffect > locLower) && (estiEffect < locUpper))
  } else if (testType == "left") {
    return(estiEffect > locLower)
  } else { # right
    return(estiEffect < locUpper)
  }
}

######################################################################################################################
# Returns the pvalue for the Likelihood Ratio Equivalence test 
lrEquiPvalue <- function(devDiffLower, devDiffUpper, dispersion, resDF, testType) {
  if (resDF < 0) {
    # Normal distribution
    if (testType == "twosided") {
      pvalLow <- pchisq(devDiffLower, 1, lower.tail=FALSE)/2
      pvalUpp <- pchisq(devDiffUpper, 1, lower.tail=FALSE)/2
      return(max(pvalLow, pvalUpp))
    } else if (testType == "left") {
      return(pchisq(devDiffLower, 1, lower.tail=FALSE)/2)
    } else { # right
      return(pchisq(devDiffUpper, 1, lower.tail=FALSE)/2)
    }
  } else {
    # Student distribution
    if (testType == "twosided") {
      pvalLow <- pf(devDiffLower/dispersion, 1, resDF, lower.tail=FALSE)/2
      pvalUpp <- pf(devDiffUpper/dispersion, 1, resDF, lower.tail=FALSE)/2
      return(max(pvalLow, pvalUpp))
    } else if (testType == "left") {
      return(pf(devDiffLower/dispersion, 1, resDF, lower.tail=FALSE)/2)
    } else { # right
      return(pf(devDiffUpper/dispersion, 1, resDF, lower.tail=FALSE)/2)
    }
  }
}

######################################################################################################################
monteCarloPowerAnalysis <- function(data, settings, modelSettings, blocks, effect, debugSettings) {

  DEBUG = FALSE

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

  nrow = settings$NumberOfSimulatedDataSets
  nDiffTests <- length(settings$AnalysisMethodsDifferenceTests)
  nEquiTests <- length(settings$AnalysisMethodsEquivalenceTests)
  pValues <- list(
    Diff  = matrix(nrow=nrow, ncol=nDiffTests, dimnames=list(NULL, settings$AnalysisMethodsDifferenceTests)),
    Equi  = matrix(nrow=nrow, ncol=nEquiTests, dimnames=list(NULL, settings$AnalysisMethodsEquivalenceTests)),
    Extra = matrix(nrow=nrow, ncol=2, dimnames=list(NULL, c("OPdisp", "NBtheta")))
  )

  # Setup simulation Data
  simulatedData <- createSimulatedData(data, settings, blocks, effect)

  # Do looping over simulations 
  ndigits = ceiling(log10(settings$NumberOfSimulatedDataSets) + 0.0001)
  for (k in 1:settings$NumberOfSimulatedDataSets) {
    debugSettings$iDataset = k
    # simulate data
    simulatedData[["Response"]] <- simulateResponse(simulatedData, settings)
    if (settings$IsOutputSimulatedData) {
      csvFile = str_pad(k, ndigits, side="left", "0")      
      csvFile = paste0(localDir, "Data-", csvFile, ".csv")
      write.csv(simulatedData, csvFile, row.names=FALSE)
    }
    # Fit models
    if ((settings$DoLNdiff) || (settings$DoLNequi)) {
      if (DEBUG) cat(paste("LN ", k, "\n"))
      result <- logNormalAnalysis(simulatedData, settings, modelSettings, debugSettings)
      if (settings$DoLNdiff) pValues$Diff[k, "LogNormal"] <- result$Diff
      if (settings$DoLNequi) pValues$Equi[k, "LogNormal"] <- result$Equi
    }
    if ((settings$DoSQdiff) || (settings$DoSQequi)) {
      if (DEBUG) cat(paste("SQ ", k, "\n"))
      result <- squareRootAnalysis(simulatedData, settings, modelSettings, debugSettings)
      if (settings$DoSQdiff) pValues$Diff[k, "SquareRoot"] <- result$Diff
      if (settings$DoSQequi) pValues$Equi[k, "SquareRoot"] <- result$Equi
    }
    # Fit overdispersed Poisson anyway to get the overdispersion parameter
    if ((settings$DoOPdiff) || (settings$DoOPequi) || (settings$DoNBdiff) || (settings$DoNBequi)) {
      if (DEBUG) cat(paste("OP ", k, "\n"))
      result <- overdispersedPoissonAnalysis(simulatedData, settings, modelSettings, debugSettings)
      pValues$Extra[k, "OPdisp"] <- result$Dispersion
      if ((settings$DoOPdiff) || (settings$DoOPequi)) {
        if (settings$DoOPdiff) pValues$Diff[k, "OverdispersedPoisson"] <- result$Diff
        if (settings$DoOPequi) pValues$Equi[k, "OverdispersedPoisson"] <- result$Equi
      }
      if ((settings$DoNBdiff) || (settings$DoNBequi)) {
        if (DEBUG) cat(paste("NB ", k, "\n"))
        if (!is.na(result$Dispersion)) {
          result <- negativeBinomialAnalysis(simulatedData, settings, modelSettings, debugSettings)
        }
        if (settings$DoNBdiff) pValues$Diff[k, "NegativeBinomial"] <- result$Diff
        if (settings$DoNBequi) pValues$Equi[k, "NegativeBinomial"] <- result$Equi
        pValues$Extra[k, "NBtheta"] <- result$Dispersion
      }
    }
    # Gamma for Nonnegative data
    if ((settings$DoGMdiff) || (settings$DoGMequi)) {
      if (DEBUG) cat(paste("GM ", k, "\n"))
      result <- gammaAnalysis(simulatedData, settings, modelSettings, debugSettings)
      if (settings$DoGMdiff) pValues$Diff[k, "Gamma"] <- result$Diff
      if (settings$DoGMequi) pValues$Equi[k, "Gamma"] <- result$Equi
    }
  }
  # LogNormal for Nonnegative data (simulated bij LogNormal) can be done exactly
  if ((settings$DoLOdiff) || (settings$DoLOequi)) {
    if (DEBUG) cat(paste("LO ", "exact calculation", "\n"))
    resultsLogNormal <- exactPowerAnalysisHelper(data, settings, modelSettings, blocks, effect)
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

  if ((FALSE) || (DEBUG)) {
    # Print raw pValues
    printPvaluesSimulate(pValues, settings, blocks, effect)
  }

  # Return power values; special for LogNormal
  pValues$Diff = as.matrix(colMeans(pValues$Diff < settings$SignificanceLevel, na.rm=TRUE))
  pValues$Equi = as.matrix(colMeans(pValues$Equi < settings$SignificanceLevel, na.rm=TRUE))
  if (settings$DoLOdiff) pValues$Diff["LogPlusM", 1] <- resultsLogNormal[1, "powerDiff"]
  if (settings$DoLOequi) pValues$Equi["LogPlusM", 1] <- resultsLogNormal[1, "powerEqui"]

  return(pValues)
}

######################################################################################################################
# Function to display the raw pvalues for each dataset in a concise format
printPvaluesSimulate <- function(pValues, settings, blocks, effect) {
  pVal = cbind(pValues$Diff, pValues$Equi)
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
  print(round(100000*pVal)/100000)
}
replaceFirst <- function(colnames, pattern, replacement) {
  match = match(pattern, colnames)
  colnames[match[1]] <- replacement
  return(colnames)
}

######################################################################################################################
# Redundant function
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

######################################################################################################################
# Redundant function
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


