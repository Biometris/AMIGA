######################################################################################################################
# Define list with settings specific for each endpoint
DefineEndpoint <- function(name, overallMean, locLower, locUpper, modifier, diffAnalysis, equiAnalysis) {
  testType = "twosided"
  naLower = is.na(locLower)
  naUpper = is.na(locUpper)
  if ((naLower) && (naUpper)) stop("Both LocLower and LocUpper are missing; this is not allowed.", call. = FALSE)
  if (naLower) testType = "right"
  if (naUpper) testType = "left"
  if (diffAnalysis == "Normal") {
    transformLocLower = locLower*overallMean - overallMean
    transformLocUpper = locUpper*overallMean - overallMean
  } else {
    transformLocLower = log(locLower)
    transformLocUpper = log(locUpper)
  }
  list <- list(
    name = name, 
    overallMean = overallMean,
    locLower = locLower, 
    locUpper = locUpper, 
    transformLocLower = transformLocLower,
    transformLocUpper = transformLocUpper,
    testType = testType, 
    modifier = modifier, 
    diffAnalysis = diffAnalysis, 
    equiAnalysis = equiAnalysis
  )
  return(list)
}

######################################################################################################################
# Read data frame 
readDataFrame <- function(globalSettings, endpoints) {
  # Read data; redefine factors and define endpoints 
  data <- read.csv(globalSettings$dataFile)
  data <- data[, !(names(data) %in% "FrequencyReplicate")]
  nFactors <- length(globalSettings$factors)+2
  elemFactors <- seq(1, nFactors)
  for (i in elemFactors) {
    data[,i] <- as.factor(data[,i])
  }
  # Check that all endPoints in endpoints have a corresponding column in data
  elemEndpoint <- seq(nFactors+1, ncol(data))
  dataNames <- names(data)
  endpointNames <- dataNames[elemEndpoint]
  errors <- FALSE
  for (endpoint in endpoints) {
    if (!is.element(endpoint$name, endpointNames)) {
      if (!errors) {
        errors <- TRUE
        cat(paste0("\n", "The following endpoints are not find in file '", globalSettings$dataFile, "'\n"))
      }
      cat(paste("  ", endpoint$name, "\n"))
    }
  }
  if (errors) {
    cat("\n")
    stop("", call.=FALSE)
  }
  return(data)
}

######################################################################################################################
# Create extra columns in dataframe
createDataFrame <- function(data, contrast, settings) {
  dataNames <- names(data)
  contrast <- contrast + 1
  tmp <- as.numeric(contrast==0)
  data[["Test"]] <- tmp[data[["Plot"]]]
  # Add factor for additional means to dataframe
  if (max(contrast) > 1) {
    contrast[contrast==0] = 1
    data[["AddTmp"]] <- contrast[data[["Plot"]]]
    # add Correct labels to Additional. First obtain factors that define Additional
    if (is.null(settings$ModifierModel)) {
      defineAdditional <- settings$Factors
    } else {
      sModifiers <- strsplit(settings$ModifierModel,"*")[[1]]
      sModifiers <- sModifiers[sModifiers!="*"]
      defineAdditional <- settings$Factors[!is.element(settings$Factors, sModifiers)]
    }
    # define interaction of these factors and modify labels
    elems <- is.element(dataNames, defineAdditional)
    if (length(elems[elems==TRUE]) > 1) {
      interaction = interaction(as.list(data[,elems]))
    } else {
      interaction = data[,elems]
    }
    table <- tapply(data[["AddTmp"]], interaction, mean)
    newLevels <- as.numeric(table)
    newLabels <- c("Comparison", row.names(table)[newLevels>1])
    newLevels <- c(1, newLevels[newLevels>1])
    newLabels <- paste0(".", newLabels[order(newLevels)])
    # Define order of levels: Comparison, Test, Comparator, remaining levels
    order <- sub(".", "4.", newLabels, fixed=TRUE)
    order <- sub("4.Comparison", "1.Comparison", order, fixed=TRUE)
    order <- sub("4.Test", "2.Test", order, fixed=TRUE)
    order <- sub("4.Comparator", "3.Comparator", order, fixed=TRUE)
    xx <- order(order)
    data[["Additional"]] <- factor(newLabels[data[["AddTmp"]]], levels=newLabels[xx])
  }
  # Add columns for LR equivalence test 
  data["lowOffset"] = data[["Test"]]*settings$TransformLocLower
  data["uppOffset"] = data[["Test"]]*settings$TransformLocUpper
  return(data)
}

######################################################################################################################
# Create models to fit for Wald and LR testing
createModels <- function(data, settings) {
  dataNames <- names(data)
  modelTerms <- c("Test")
  if (is.element("Additional", dataNames)) {
    modelTerms <- c(modelTerms, "Additional")
  }
  if (length(settings$ModifierModel) > 0) {
    modelTerms <- c(modelTerms, settings$ModifierModel)
  }
  if (settings$Design == "RandomizedCompleteBlocks") {
    modelTerms <- c(modelTerms, "Block")
  }
  modelH1 = as.formula(paste0(settings$EndPoint, " ~ ", paste0(modelTerms, collapse=" + ")))
  modelH0 <- update(modelH1, ~ . - Test)
  modelH0low <- update(modelH0, ~ . + offset(lowOffset))
  modelH0upp <- update(modelH0, ~ . + offset(uppOffset))
  models <- c(H1=modelH1, H0=modelH0, H0low=modelH0low, H0upp=modelH0upp)
  return(models)
}

######################################################################################################################
# Check Additional Means and Modifiers by fitting a multiplicative loglinear model
checkFitModel <- function(data, settings, models, meanData) {
  # Insert the mean data column as Endpoint respons
  data[[settings$EndPoint]] = meanData[["Mean"]][data[["Plot"]]]
  cat("\n")
  print(head(data[,seq(1,ncol(data)-2)], n=nlevels(data[["Plot"]])))

  glmH1 <- glm(models[["H1"]], family="poisson", data=data)
  print(summary(glmH1))
  mult <- 1
  if (!is.null(settings$ModifierModel)) { 
    # Modifier means
    lsmeans <- summary(lsmeans(glmH1, as.formula(paste("~",settings$ModifierModel))))
    lstCol <- ncol(lsmeans)-4
    submean <- lsmeans[, seq(1,lstCol)]
    submean[[lstCol]] <- exp(submean[[lstCol]] - submean[[lstCol]][1] + glmH1$coef[1])
    colnames(submean)[lstCol] <- "Modifier"
    cat("\n")
    print(submean)
    mult <- mean(submean[[lstCol]]/submean[[lstCol]][1])
  }
  # Additional means 
  esti <- as.vector(glmH1$coef)
  esti[seq(2,length(esti))] <- esti[seq(2,length(esti))] + esti[1]
  estiNames <- as.vector(row.names(summary(glmH1)$coef))
  estiNames <- sub("(Intercept)", "Comparator", estiNames, fixed=TRUE)
  include <- (substring(estiNames,1,10) == "Comparator") | (substring(estiNames,1,4) == "Test") | (substring(estiNames,1,10) == "Additional") 
  estiAdditional <- data.frame(Parameter=estiNames[include], AddMeanTool=mult*exp(esti[include]), AddMeanInput=exp(esti[include]))
  cat("\n")
  print(estiAdditional)
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
