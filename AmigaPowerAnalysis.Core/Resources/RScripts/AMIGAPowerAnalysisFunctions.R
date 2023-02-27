######################################################################################################################
# Definition of endpoints
DefineEndpoint <- function(name, overallMean, locLower, locUpper, modifier, 
  diffAnalysis = c("OverdispersedPoisson", "NegativeBinomial", "LogNormal", "SquareRoot", "LogPlusM", "Gamma", "Normal"), 
  equiAnalysis = c("OverdispersedPoisson", "NegativeBinomial", "LogNormal", "SquareRoot", "LogPlusM", "Gamma", "Normal")) {
  chkArg = match.arg(diffAnalysis)
  chkArg = match.arg(equiAnalysis)
  # Define type of test, i.e. twosided or onesided
  testType = "twosided"
  naLower = is.na(locLower)
  naUpper = is.na(locUpper)
  if ((naLower) && (naUpper)) stop("Both LocLower and LocUpper are missing; this is not allowed.", call. = FALSE)
  if (naLower) testType = "right"
  if (naUpper) testType = "left"
  if (testType == "twosided") {
    if (locLower >= locUpper) {
      fault = paste0("\nlocLower must be smaller than locUpper. The values provides are locLower=", locLower, " and locUpper=", locUpper, ".\n\n")
      cat(fault)
      stop("")
    }
  }
  # define transform of limits of concern
  if (diffAnalysis == "Normal") {
    # The transformed values are differences with respect to the overallMean
    transformLocLower = locLower*overallMean - overallMean
    transformLocUpper = locUpper*overallMean - overallMean
  } else {
    transformLocLower = log(locLower)
    transformLocUpper = log(locUpper)
  }
  # define NULL object for modifier
  # if (length(modifier)==1) {
  #  if (modifier == "NULL") modifier=NULL
  # }
  # Define endpoints settings
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
# Rename endpoints names so that names coincide with names read from CSV
renameEndpoints <- function(endpoints) {
  # Check whether names read from CSV are different; if so modify
  endpointNames = c()
  for (endpoint in endpoints) {
    if (is.null(endpoint)) next 
    endpointNames <- c(endpointNames, endpoint$name)
  }
  newName = make.names(endpointNames, unique=TRUE)
  differentNames = which(newName != endpointNames)
  if (length(differentNames)) {
    cat(paste0("\nThe following endPoint names have been modified so that they coincide whith names in the datafile\n\n"))
    df <- data.frame(endpointNames, newName)
    print(df[differentNames,])
    cat("\n")
    count <- 0
    countName <- 0
    newEndpoints <- list()
    for (endpoint in endpoints) {
      count = count + 1
      if (is.null(endpoint)) next
      newEndpoints[[count]] <- endpoint
      countName <- countName + 1
      newEndpoints[[count]]$name <- newName[countName]
    }
    return(newEndpoints)
  } else {
    return(endpoints)
  }
}

######################################################################################################################
# Read data frame 
readDataFile <- function(settings, endpoints, checkData=TRUE) {
  if (settings$dataDirectory == "") {
    file <- settings$dataFile
  } else {
    file <- file.path(settings$dataDirectory, settings$dataFile)
  }
  cat(paste0("\nReading data from file ", file, ".\n"))
  if (!file.exists(file)) {
    cat(paste0("dataFile '", file, "' does not exist.\n\n"))
    stop("")
  }

  # Read data; redefine factors and define endpoints 
  data <- read.csv(file)
  data <- data[, !(names(data) %in% "FrequencyReplicate")]
  nFactors <- length(settings$factors)+2
  elemFactors <- seq(1, nFactors)
  for (i in elemFactors) {
    data[,i] <- as.factor(data[,i])
  }
  # Check that all names in endpoints have a corresponding column in data
  elemEndpoint <- seq(nFactors+1, ncol(data))
  dataNames <- names(data)
  endpointNames <- dataNames[elemEndpoint]
  errors <- FALSE
  for (endpoint in endpoints) {
    if (is.null(endpoint)) next
    if (!is.element(endpoint$name, endpointNames)) {
      if (!errors) {
        errors <- TRUE
        cat(paste0("The endpoints below are not found in dataFile '", settings$dataFile, "'\n"))
      }
      cat(paste(" ", endpoint$name, "\n"))
    }
  }
  if (errors) {
    cat("\n")
    stop("")
  }
  # Check that there is data for all endpoints 
  if (checkData) {
    errors <- FALSE
    for (endpoint in endpoints) {
    if (is.null(endpoint)) next
      if(is.factor(data[[endpoint$name]])) {
        if (!errors) {
          errors <- TRUE
          cat(paste0("The endpoints below are converted to factors while reading dataFile '", settings$dataFile, "'.\n"))
          cat(paste0("Check that the dataFile contains numbers for these endpoints.\n"))

        }
        cat(paste(" ", endpoint$name, "\n"))
      }
    }
    if (errors) {
      cat("\n")
      stop("")
    }
  }
  # Print (part of) data and return
  ncol = min(nFactors + 3, ncol(data))
  cat("The first few rows and columns of the data:\n\n")
  print(head(data[seq(1:ncol)], nlevels(data[["Plot"]])))
  cat("\n")
  return(data)
}

######################################################################################################################
# Read contrast file 
readContrastFile<- function(settings, endpoints) {
  cat("\n")
  if (settings$dataDirectory == "") {
    file <- settings$contrastFile
  } else {
    file <- file.path(settings$dataDirectory, settings$contrastFile)
  }
  cat(paste0("Reading contrasts from file ", file, ".\n"))
  if (!file.exists(file)) {
    cat(paste0("contrastFile '", file, "' does not exist.\n\n"))
    stop("")
  }
  # Read contrasts file 
  contrasts <- read.csv(file)
  # Check that all names in endpoints have a corresponding column in contrasts
  nFactors <- length(settings$factors)
  elemEndpoint <- seq(nFactors+1, ncol(contrasts))
  contrastsNames <- names(contrasts)
  endpointNames <- contrastsNames[elemEndpoint]
  errors <- FALSE
  for (endpoint in endpoints) {
    if (is.null(endpoint)) next
    if (!is.element(endpoint$name, endpointNames)) {
      if (!errors) {
        errors <- TRUE
        cat(paste0("The endpoints below are not found in contrastFile '", settings$contrastFile, "'\n"))
      }
      cat(paste("  ", endpoint$name, "\n"))
    }
  }
  if (errors) {
    cat("\n")
    stop("")
  }
  # Check that there is data for all contrasts 
  errors <- FALSE
  for (endpoint in endpoints) {
    if (is.null(endpoint)) next
    if(is.factor(contrasts[[endpoint$name]])) {
      if (!errors) {
        errors <- TRUE
        cat(paste0("The contrasts for endpoints below are converted to factors while reading contrastFile '", settings$contrastFile, "'.\n"))
        cat(paste0("Check that the contrastFile contains numbers for these endpoints.\n"))

      }
      cat(paste(" ", endpoint$name, "\n"))
    }
  }
  if (errors) {
    cat("\n")
    stop("")
  }
  # Print (part of) contrasts and return
  ncol = min(nFactors + 3, ncol(contrasts))
  cat("The first few columns of the contrasts. Numbers -1 and 0 define the comparison\n")
  cat("of interest between the Test variety (-1) and the Comparator (0):\n\n")
  print(contrasts[seq(1:ncol)])
  cat("\n")
  return(contrasts)
}

######################################################################################################################
# Create dataframe for the specific endpoint
createDataFrame <- function(data, contrasts, settings, endpoint) {
  # Create dataframe with the relevant columns
  nFactors <- length(settings$factors) + 2
  newdata <- data[c(1:nFactors)]
  newdata[[endpoint$name]] = data[[endpoint$name]]

  # Add column Test to dataframe for testing the contrast
  contrast <- contrasts[[endpoint$name]] + 1
  tmp <- as.numeric(contrast==0)
  newdata[["Test"]] <- tmp[newdata[["Plot"]]]
  dataNames <- names(data)
  # Add factor Additional to dataframe for additional means
  if (max(contrast) > 1) {
    contrast[contrast==0] = 1
    newdata[["AddTmp"]] <- contrast[newdata[["Plot"]]]
    # add Correct labels to Additional. First obtain factors that define Additional
    # if (is.null(endpoint$modifier)) {
    if (endpoint$modifier[1] == "-") {
      defineAdditional <- settings$factors
    } else {
      sModifiers <- strsplit(endpoint$modifier,"*")[[1]]
      sModifiers <- sModifiers[sModifiers!="*"]
      defineAdditional <- settings$factors[!is.element(settings$factors, sModifiers)]
    }
    # define interaction of these factors and modify labels
    elems <- is.element(dataNames, defineAdditional)
    if (length(elems[elems==TRUE]) > 1) {
      interaction = interaction(as.list(newdata[,elems]))
    } else {
      interaction = newdata[,elems]
    }
    table <- tapply(newdata[["AddTmp"]], interaction, mean)
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
    newdata[["Additional"]] <- factor(newLabels[newdata[["AddTmp"]]], levels=newLabels[xx])
  }
  # Add columns for LR equivalence test 
  newdata["lowOffset"] = newdata[["Test"]]*endpoint$transformLocLower 
  newdata["uppOffset"] = newdata[["Test"]]*endpoint$transformLocUpper
  return(newdata)
}

######################################################################################################################
# Create models to fit for Wald and LR testing
createModels <- function(data, settings, endpoint) {
  dataNames <- names(data)
  modelTerms <- c("Test")
  if (is.element("Additional", dataNames)) {
    modelTerms <- c(modelTerms, "Additional")
  }
  # if (length(endpoint$modifier) > 0) {
  if (endpoint$modifier[1] != "-") {
    modelTerms <- c(modelTerms, endpoint$modifier)
  }
  if (settings$design == "RandomizedCompleteBlocks") {
    modelTerms <- c(modelTerms, "Block")
  }
  modelH1 = as.formula(paste0(endpoint$name, " ~ ", paste0(modelTerms, collapse=" + ")))
  modelH0 <- update(modelH1, ~ . - Test)
  modelH0lower <- update(modelH0, ~ . + offset(lowOffset))
  modelH0upper <- update(modelH0, ~ . + offset(uppOffset))
  models <- c(H1=modelH1, H0=modelH0, H0lower=modelH0lower, H0upper=modelH0upper)
  return(models)
}

######################################################################################################################
# Check Additional Means and Modifiers by fitting a multiplicative loglinear model
checkFitModel <- function(data, settings, endpoint, models, meanData, printFittedModel) {
  cat("\n")
  # Insert the mean data column as Endpoint respons
  data[[endpoint$name]] = meanData[["Mean"]][data[["Plot"]]]

  glmH1 <- glm(models[["H1"]], family="poisson", data=data)
  if (printFittedModel) {
    printcol <- seq(1, length(settings$factors)+2)
    printcol <- c(printcol, match(c("Test", "AddTmp", endpoint$name), names(data)))
    cat("\nMeans as specified in Simulation Tool:\n\n")
    print(head(data[,printcol], n=nlevels(data[["Plot"]])))
    print(summary(glmH1))
  }

  mult <- 1
  # if (!is.null(endpoint$modifier)) { 
  if (endpoint$modifier[1] != "-") { 
    # Modifier means
    lsmeans <- summary(lsmeans(glmH1, as.formula(paste("~",endpoint$modifier))))
    lstCol <- ncol(lsmeans)-4
    submean <- lsmeans[, seq(1,lstCol)]
    submean[[lstCol]] <- exp(submean[[lstCol]] - submean[[lstCol]][1] + glmH1$coef[1])
    colnames(submean)[lstCol] <- "Modifier"
    submean[["Value"]] <- submean[["Modifier"]] /endpoint$overallMean
    print(submean)
    mult <- mean(submean[["Modifier"]]/submean[["Modifier"]][1])
  }
  # Additional means 
  esti <- as.vector(glmH1$coef)
  esti[seq(2,length(esti))] <- esti[seq(2,length(esti))] + esti[1]
  estiNames <- as.vector(row.names(summary(glmH1)$coef))
  estiNames <- sub("(Intercept)", "Comparator", estiNames, fixed=TRUE)
  include <- (substring(estiNames,1,10) == "Comparator") | (substring(estiNames,1,4) == "Test") | (substring(estiNames,1,10) == "Additional") 
  estiAdditional <- data.frame(Parameter=estiNames[include], AddMeanTool=mult*exp(esti[include]), AddMeanInput=exp(esti[include]))
  print(estiAdditional)
}

######################################################################################################################
# Simulate data; function used for testing
simulateData <- function(data, settings) {
  set.seed(123457)
  nFactors <- length(settings$factors)+2
  elemEndpoint <- seq(nFactors+1, ncol(data))
  for (i in elemEndpoint) {
    mean <- 4+4*(i-nFactors-1)
    dispersion = 2
    data[[i]] <- ropoisson(nrow(data), mean, dispersion, distribution="OverdispersedPoisson")
  }
  return(data)
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

###########################################################################################################################
# Printing of caption
printCaption <- function(caption, style="=", nEmptylines=0) {
  cat(paste0("\n", caption, "\n", paste(replicate(nchar(caption), style), collapse = ""), "\n"))
  cat(rep("\n", nEmptylines))
}

###########################################################################################################################
# Consice printing of results
printResults <- function(endpoint, diffResults, equiResults) {
  field <- 12
  deci  <- 3
  fstr <- paste0("%", field, "s")
  fdbl <- paste0("%", field, ".", deci, "f")

  formatC <- "g"

  printCaption(paste0("Results for endpoint ", endpoint$name, " with LOCs ", 
      trimws(formatC(endpoint$locLower, digits=4, format=formatC)), " and ", 
      trimws(formatC(endpoint$locUpper, digits=4, format=formatC))), "-")
  cat("\n")
  cat(paste0("           ", sprintf(fstr , paste0("Diff-", diffResults$model)),  sprintf(fstr, paste0("Equi-", equiResults$model)), "\n"))
  cat(paste0(" pMean Test", formatC(diffResults$meanTest, digits=4, width=field, format=formatC), 
      formatC(equiResults$meanTest, digits=4, width=field, format=formatC), "\n"))
  cat(paste0(" pMean Comp", formatC(diffResults$meanComparator, digits=4, width=field, format=formatC), 
      formatC(equiResults$meanComparator, digits=4, width=field, format=formatC), "\n"))
  cat(paste0("      Ratio", formatC(diffResults$estimate, digits=4, width=field, format=formatC), 
      formatC(equiResults$estimate, digits=4, width=field, format=formatC), "\n"))
  cat(paste0("     CIleft", sprintf(fdbl, diffResults$ciDiffLeft),  sprintf(fdbl, equiResults$ciEquiLeft)),  "\n")
  cat(paste0("    CIright", sprintf(fdbl, diffResults$ciDiffRight), sprintf(fdbl, equiResults$ciEquiRight)), "\n")
  cat(paste0("      pWald", sprintf(fdbl, diffResults$pDiffWald),   sprintf(fdbl, equiResults$pEquiWald)),   "\n")
  cat(paste0("        pLR", sprintf(fdbl, diffResults$pDiffLR),     sprintf(fdbl, equiResults$pEquiLR)),     "\n")
}

###########################################################################################################################
# printing of results for all endpoints
printSummary <- function(endpoints, settings, diffAnalysis, equiAnalysis) {
  # Collect all the relevant info in a single dataFrame
  ep   <- do.call(rbind.data.frame, endpoints)
  diff <- do.call(rbind.data.frame, diffAnalysis)
  equi <- do.call(rbind.data.frame, equiAnalysis)
  if (settings$testMethodSummary == "Wald") {  # Wald
    df <- data.frame(endpoint=ep$name, locLower=ep$locLower, locUpper=ep$locUpper, 
      diffPval=diff$pDiffWald, equiPval=equi$pEquiWald)
  } else {      # LR
    df <- data.frame(endpoint=ep$name, locLower=ep$locLower, locUpper=ep$locUpper, 
      diffPval=diff$pDiffLR, equiPval=equi$pEquiLR)
  }
  cat("\n")
  print(df, row.names=FALSE)
  cat("\n")
}

###########################################################################################################################
# Plot of confidence interval for Difference and Equivalence testing for all endpoints
plotSummary <- function(endpoints, settings, diffAnalysis, equiAnalysis, filename, type="png", numberInOnePlot=10, width=600, height=800, pointsize=12, extend=2) {
  if (!is.element(type, c("png", "jpeg", "tiff"))) {
    cat(paste0("\n***** Fault: option type must be set ot either png, jpeg or tiff\n\n"))
    stop("")
  }
  # Number of endpoints in one frame
  nPlots <- numberInOnePlot
  # Define labels along the x-axis 
  xmarks <- 2^c(1,2,3,4,5,6,7)
  xlabels <- as.character(xmarks)
  xlabels <- c(paste0("1/", xlabels), "1", xlabels)
  xmarks <- c(1/xmarks, 1, xmarks)
  order <- order(xmarks)
  xlabels <- xlabels[order]
  xmarks <- log(xmarks[order])

  # Read data and create initial plot

  nData <- length(diffAnalysis)
  nPlots <- min(nData, nPlots)
  nFrames <- nData %/% nPlots
  if (nFrames*nPlots < nData) nFrames = nFrames+1

  # Collect all the relevant info in a single dataFrame
  ep   <- do.call(rbind.data.frame, endpoints)
  diff <- do.call(rbind.data.frame, diffAnalysis)
  equi <- do.call(rbind.data.frame, equiAnalysis)

  if (length(is.na(ep$locLower)[is.na(ep$locLower)]) == length(ep$locLower)) {
    # No Lower Limits of Concern
    locMin = NaN
  } else {
    locMin <- log(min(ep$locLower, na.rm=TRUE)) - log(extend)
  }
  if (length(is.na(ep$locUpper)[is.na(ep$locUpper)]) == length(ep$locUpper)) {
    # No Upper Limits of Concern
    locMax = NaN
  } else {
    locMax <- log(max(ep$locUpper, na.rm=TRUE)) + log(extend)
  }
  if (is.na(locMin)) locMin = -locMax
  if (is.na(locMax)) locMax = -locMin

  iFrame <- 0
  for (i in 1:nData) {
    # j is number of plot in current frame
    iPlot = i %% nPlots
    if (iPlot == 0) iPlot=nPlots
    # Start a new plot and save the old one
    if (iPlot == 1) {
      # Create and save plotfile
      iFrame = iFrame + 1
      if (iFrame > 1) dev.off()
      if (type == "png") {
        png(paste0(filename, "-", iFrame, ".", type), width=width, height=height, pointsize=pointsize)
      } else if (type == "jpeg") {
        jpeg(paste0(filename, "-", iFrame, ".", type), width=width, height=height, pointsize=pointsize, quality=100)
      } else if (type == "tiff") {
        tiff(paste0(filename, "-", iFrame, ".", type), width=width, height=height, pointsize=pointsize, compression="none")
      } else if (type == "pdf") {
        pdf(paste0(filename, "-", iFrame, ".", type), width=width, height=height, pointsize=pointsize)
      } else {
        stop("Wrong filetype")
      }
      # Start new plot
      if (iFrame < nFrames) {
        ylim <- nPlots
      } else {
        ylim <- nData %% nPlots
        if (ylim == 0) ylim <- nPlots
      }
      plot(c(0,0), c(0,0), type="n", yaxt="n", xaxt="n", xaxs = "i", yaxs = "i", ann=FALSE, xlim=c(locMin,locMax), ylim=c(0,ylim))
      axis(1, at=xmarks, labels=xlabels)
      # Vertical line for no difference
      segments(0, -1, 0, ylim+1, col="Black",lwd=2)
      # Horizontal lines to separate the endpoints
      ylim1 = ylim - 1
      for (k in 1:ylim1) {
        segments(-10, ylim-k, 10, ylim-k, col="darkgrey")
      }
    }
    # Vertical lines to display the limits of concern
    locLow <- log(ep[["locLower"]][i])
    locUpp <- log(ep[["locUpper"]][i])
    if (is.na(locLow) || is.na(locUpp)) {
      lwd=3 ; lty=1 ; col="red"
    } else {
      lwd=2 ; lty=1 ; col="red"
    }
    for (ll in c(locLow,locUpp)) {
      if (!is.na(ll)) segments(ll, ylim-iPlot+0.05, ll, ylim-iPlot+0.75, col=col, lwd=lwd, lty=lty)
    }
    # Confidence limits and points
    ypos1 <- ylim - iPlot + 0.5
    ypos2 <- ylim - iPlot + 0.15
    ypos3 <- ylim - iPlot + 0.85
    points(log(diff[["estimate"]][i]), ypos1, col="Black", pch=21, bg="Black")
    points(log(diff[["estimate"]][i]), ypos2, col="Red",   pch=21, bg="Red")
    if (is.na(locLow) || is.na(locUpp)) {
     angle  <-15
     length <-0.2 
    } else {
     angle  <-90
     length <-0.1
    }
    arrows(log(diff[["ciDiffLeft"]][i]), ypos1, log(diff[["ciDiffRight"]][i]), ypos1, col="Black", code=3, length=length, angle=angle)
    arrows(log(equi[["ciEquiLeft"]][i]), ypos2, log(equi[["ciEquiRight"]][i]), ypos2, col="Red", code=3, length=0.2, angle=15)
    # text
    text(locMin + 0*(locMax-locMin), ypos3, ep[["name"]][i], pos=4)
    if (settings$testMethodSummary == "Wald") {  # Wald
      tDiff = paste0(diff[["model"]][i], "  ", sprintf("%5.3f", diff[["pDiffWald"]][i]))
      tEqui = paste0(equi[["model"]][i], "  ", sprintf("%5.3f", equi[["pEquiWald"]][i]))
    } else {      # LR
      tDiff = paste0(diff[["model"]][i], "  ", sprintf("%5.3f", diff[["pDiffLR"]][i]))
      tEqui = paste0(equi[["model"]][i], "  ", sprintf("%5.3f", equi[["pEquiLR"]][i]))
    }
    posX = log(diff[["estimate"]][i])
    if (posX > 0.9*locMax) {
      posX = 0.5*locMax
    }
    if (posX < 0.9*locMin) {
      posX = 0.5*locMin
    }
    text(posX, ypos1, tDiff, col="Black", pos=3, cex=0.9)
    text(posX, ypos2, tEqui, col="Red",   pos=3, cex=0.9)
  }
  devoff <- dev.off()
}

###########################################################################################################################
# Predictions for Comparator and Test
predictedMeans <- function(data, fittedModel, transformation=c("None", "Log")) {
  if (transformation == "None") {
    # Predictions by lsmeans on linear predictor scale
    if (is.element("Additional", names(data))) {
      lsmeans <- lsmeans(fittedModel, ~ Test + Additional, at=list(Test=c(0,1), Additional=".Comparison"))
    } else {
      lsmeans <- lsmeans(fittedModel, ~ Test, at=list(Test=c(0,1)))
    }
    results <- list(meanComparator=summary(lsmeans)$lsmean[1], meanTest=summary(lsmeans)$lsmean[2], vcov=vcov(lsmeans))
  } else {
    # Predictions with averaging done on the original scale; no vcov
    if (is.element("Additional", names(data))) {
      refGrid <- summary(ref.grid(fittedModel, at=list(Test=c(0,1), Additional=".Comparison")))
    } else {
      refGrid <- summary(ref.grid(fittedModel, at=list(Test=c(0,1))))
    }
    lsmeans <- aggregate(exp(prediction) ~ Test, data=refGrid ,mean)
    results <- list(meanComparator=lsmeans[[2]][1], meanTest=lsmeans[[2]][2], vcov=NULL)
  }
  return(results)
}

###########################################################################################################################
# Fits ordinary normal regression model
normalAnalysis <- function(data, settings, endpoint, models, printmodel=TRUE) {

  results <- list(model="normal regression model", 
      ciDiffLeft=NaN, ciDiffRight=NaN, pDiffWald=NaN, pDiffLR=NaN, 
      ciEquiLeft=NaN, ciEquiRight=NaN, pEquiWald=NaN, pEquiLR=NaN, 
      estimate=NaN, meanComparator=NaN, meanTest=NaN)

  # Fit model
  glmH1 <- lm(models[["H1"]], data=data)
  if (printmodel) {
    printCaption(paste0("NO: Normal regression model for ", endpoint$name), "-")
    print(summary(glmH1))
  }
  resDF <- glmH1$df.residual
  resMS <- deviance(glmH1)/resDF
  estiEffect = as.numeric(glmH1$coef[2])
  seEffect = sqrt(vcov(glmH1)[2,2])

  # Wald Interval for difference and equivalence testing 
  intervals <- twoSidedInterval(estiEffect, seEffect, resDF, endpoint$testType, settings$significanceLevel)
  results$ciDiffLeft  <- intervals$leftDiff
  results$ciDiffRight <- intervals$rightDiff
  results$ciEquiLeft  <- intervals$leftEqui
  results$ciEquiRight <- intervals$rightEqui

  # Wald pValues
  results$pDiffWald <- waldDiffPvalue(estiEffect, seEffect, resDF, endpoint$testType)
  results$pDiffLR   <- results$pDiffWald
  results$pEquiWald <- waldEquiPvalue(estiEffect, seEffect, resDF, endpoint$testType, endpoint$transformLocLower, endpoint$transformLocUpper)
  results$pEquiLR   <- results$pEquiWald

  # Obtain predictions 
  lsmeans <- predictedMeans(data, glmH1, "None")
  results$meanComparator = lsmeans$meanComparator
  results$meanTest = lsmeans$meanTest

  endpoint$locLower = endpoint$transformLocLower
  endpoint$locUpper = endpoint$transformLocUpper
  printResults(results, endpoint)

  # CI-interval is on the difference scale. Scale back to multiplicate scale
  results$ciDiffLeft  = (results$meanComparator + results$ciDiffLeft )/results$meanComparator
  results$ciDiffRight = (results$meanComparator + results$ciDiffRight)/results$meanComparator
  results$ciEquiLeft  = (results$meanComparator + results$ciEquiLeft )/results$meanComparator
  results$ciEquiRight = (results$meanComparator + results$ciEquiRight)/results$meanComparator
  results$estimate    = results$meanTest/results$meanComparator
  return(results)
}
###########################################################################################################################
# Fits the LN model for counts and returns P-values and Confidence intervals
logNormalAnalysis <- function(data, settings, endpoint, models, printmodel=TRUE) {

  numberOfSimulationsCGI <- 100000  # Number of simulations for GCI   
  smallGCI <- 0.0001                # Bound on back-transformed values

  results <- list(model="LN", 
      ciDiffLeft=NaN, ciDiffRight=NaN, pDiffWald=NaN, pDiffLR=NaN, 
      ciEquiLeft=NaN, ciEquiRight=NaN, pEquiWald=NaN, pEquiLR=NaN,
      estimate=NaN, meanComparator=NaN, meanTest=NaN)

  # Wald and LR are identical as there is no LR equivalence test

  # Transform data and fit model under H1
  data[endpoint$name] <- log(data[endpoint$name] + 1)
  glmH1 <- lm(models[["H1"]], data=data)
  if (printmodel) {
    printCaption(paste0("LN: log(y+1) regression model for ", endpoint$name), "-")
    print(summary(glmH1))
  }
  resDF <- glmH1$df.residual
  resMS <- deviance(glmH1)/resDF
  estiEffect = as.numeric(glmH1$coef[2])
  seEffect = sqrt(vcov(glmH1)[2,2])

  # Wald and LR pValues are the same
  results$pDiffWald <- waldDiffPvalue(estiEffect, seEffect, resDF, endpoint$testType)
  results$pDiffLR   <- results$pDiffWald

  # Obtain predictions 
  lsmeans <- predictedMeans(data, glmH1, "None")
  meanCMP <- lsmeans$meanComparator
  meanTST <- lsmeans$meanTest
  vcovLS <- lsmeans$vcov
  # GCI; this takes account of a possible covariance between predictions
    # In case any of the draws is Inf (after exponentiation) the corresponding ratio is set to NaN and the pvalue is also set to NaN
  chi  <- resDF * resMS / rchisq(numberOfSimulationsCGI, resDF)
  random = mvrnorm(numberOfSimulationsCGI, c(0,0), vcovLS)*sqrt(chi/resMS)
  random[,1] = meanCMP + random[,1]
  random[,2] = meanTST + random[,2]
  random <- exp(random + chi/2) - 1
  results$meanComparator = median(random[,1], na.rm=TRUE)
  results$meanTest = median(random[,2], na.rm=TRUE)
  random[random < smallGCI] <- smallGCI
  ratio <- random[,2]/random[,1]
  ratio[(random[,1]==Inf) || (random[,2]==Inf)] = NaN
  results$pEquiWald = cgiEquiPvalue(ratio, endpoint$locLower, endpoint$locUpper, endpoint$testType)
  results$pEquiLR = results$pEquiWald

  # Intervals 
  intervals <- twoSidedGCIInterval(ratio, endpoint$testType, settings$significanceLevel)
  results$ciDiffLeft  <- intervals$leftDiff
  results$ciDiffRight <- intervals$rightDiff
  results$ciEquiLeft  <- intervals$leftEqui
  results$ciEquiRight <- intervals$rightEqui
  results$estimate    <- median(ratio, na.rm=TRUE)
  return(results)
}

######################################################################################################################
squareRootAnalysis <- function(data, settings, endpoint, models, printmodel=TRUE) {

  numberOfSimulationsCGI <- 100000  # Number of simulations for GCI   
  smallGCI <- 0.0001                # Bound on back-transformed values

  results <- list(model="SQ", 
      ciDiffLeft=NaN, ciDiffRight=NaN, pDiffWald=NaN, pDiffLR=NaN, 
      ciEquiLeft=NaN, ciEquiRight=NaN, pEquiWald=NaN, pEquiLR=NaN,
      estimate=NaN, meanComparator=NaN, meanTest=NaN)

  # Wald and LR are identical as there is no LR equivalence test

  # Transform data and fit model
  data[endpoint$name] <- sqrt(data[endpoint$name])
  glmH1 <- lm(models[["H1"]], data=data)
  if (printmodel) {
    printCaption(paste0("SQ: squareRoot regression model for ", endpoint$name), "-")
    print(summary(glmH1))
  }
  resDF <- glmH1$df.residual
  resMS <- deviance(glmH1)/resDF
  estiEffect = as.numeric(glmH1$coef[2])
  seEffect = sqrt(vcov(glmH1)[2,2])

  # Wald and LR pValues are the same
  results$pDiffWald <- waldDiffPvalue(estiEffect, seEffect, resDF, endpoint$testType)
  results$pDiffLR   <- results$pDiffWald

  # Obtain predictions 
  lsmeans <- predictedMeans(data, glmH1, "None")
  meanCMP <- lsmeans$meanComparator
  meanTST <- lsmeans$meanTest
  vcovLS <- lsmeans$vcov

  # GCI; this takes account of a possible covariance between predictions
    # In case any of the draws is Inf (after exponentiation) the corresponding ratio is set to NaN and the pvalue is also set to NaN
  chi  <- resDF * resMS / rchisq(numberOfSimulationsCGI, resDF)
  random = mvrnorm(numberOfSimulationsCGI, c(0,0), vcovLS)*sqrt(chi/resMS)
  random[,1] = meanCMP + random[,1]
  random[,2] = meanTST + random[,2]
  random <- random*random + chi
  results$meanComparator = median(random[,1], na.rm=TRUE)
  results$meanTest = median(random[,2], na.rm=TRUE)
  ratio <- random[,2]/random[,1]
  ratio[ratio==Inf] = NaN
  results$pEquiWald = cgiEquiPvalue(ratio, endpoint$locLower, endpoint$locUpper, endpoint$testType)
  results$pEquiLR = results$pEquiWald

  # Intervals 
  intervals <- twoSidedGCIInterval(ratio, endpoint$testType, settings$significanceLevel)
  results$ciDiffLeft  <- intervals$leftDiff
  results$ciDiffRight <- intervals$rightDiff
  results$ciEquiLeft  <- intervals$leftEqui
  results$ciEquiRight <- intervals$rightEqui
  results$estimate    <- median(ratio, na.rm=TRUE)
  return(results)
}

###########################################################################################################################
# Fits the OP model for counts and returns P-values and Confidence intervals
overdispersedPoissonAnalysis <- function(data, settings, endpoint, models, printmodel=TRUE) {

  results <- list(model="OP", 
      ciDiffLeft=NaN, ciDiffRight=NaN, pDiffWald=NaN, pDiffLR=NaN, 
      ciEquiLeft=NaN, ciEquiRight=NaN, pEquiWald=NaN, pEquiLR=NaN,
      estimate=NaN, meanComparator=NaN, meanTest=NaN)
  limitDispersion <- 1
  family <- "quasipoisson"

  # Fit model 
  glmH1 <- glm(models[["H1"]], family=family, data=data)
  if (printmodel) {
    printCaption(paste0("OP: overdispersedPoisson loglinear regression model for ", endpoint$name), "-")
    print(summary(glmH1))
  }

  # Prepare for Wald tests and confidence intervals 
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

  # Wald Interval for difference and equivalence testing; always two-sided
  intervals <- twoSidedInterval(estiEffect, seEffect, resDF, endpoint$testType, settings$significanceLevel)
  results$ciDiffLeft  <- exp(intervals$leftDiff)
  results$ciDiffRight <- exp(intervals$rightDiff)
  results$ciEquiLeft  <- exp(intervals$leftEqui)
  results$ciEquiRight <- exp(intervals$rightEqui)
  results$estimate    <- exp(estiEffect)

  # Wald pValues
  results$pDiffWald <- waldDiffPvalue(estiEffect, seEffect, resDF, endpoint$testType)
  results$pEquiWald <- waldEquiPvalue(estiEffect, seEffect, resDF, endpoint$testType, endpoint$transformLocLower, endpoint$transformLocUpper)

  # Obtain predictions 
  lsmeans <- predictedMeans(data, glmH1, "Log")
  results$meanComparator = lsmeans$meanComparator
  results$meanTest = lsmeans$meanTest

  # LR test for Difference 
  if (lrDiffDo(estiEffect, 0, endpoint$testType)) {
    glmH0 <- glm(models[["H0"]], family=family, data=data)
    devDiff = deviance(glmH0) - deviance(glmH1)
    results$pDiffLR <- lrDiffPvalue(devDiff, estDispersion, resDF, endpoint$testType)
  } else {
    results$pDiffLR <- results$pDiffWald
  }

  # LR test for Equivalence 
  if ((endpoint$testType == "twosided") || (endpoint$testType == "left")) {
    glmH0lower <- glm(models[["H0lower"]], family=family, data=data)
    devDiffLower <- deviance(glmH0lower) - deviance(glmH1)
  } else {
    devDiffLower = NA
  }

  if ((endpoint$testType == "twosided") || (endpoint$testType == "right")) {
    glmH0upper <- glm(models[["H0upper"]], family=family, data=data)
    devDiffUpper <- deviance(glmH0upper) - deviance(glmH1)
  } else {
    devDiffUpper = NA
  }

  results$pEquiLR <- lrEquiPvalue(devDiffLower, devDiffUpper, estDispersion, resDF, endpoint$testType)
  return(results)
}
###########################################################################################################################
# Fits the NB model for counts and returns P-values and Confidence intervals
negativeBinomialAnalysis <- function(data, settings, endpoint, models, printmodel=TRUE) {

  results <- list(model="NB", 
      ciDiffLeft=NaN, ciDiffRight=NaN, pDiffWald=NaN, pDiffLR=NaN, 
      ciEquiLeft=NaN, ciEquiRight=NaN, pEquiWald=NaN, pEquiLR=NaN,
      estimate=NaN, meanComparator=NaN, meanTest=NaN)

  # First fit the overdispersed Poisson to check whether we have overdispersion
  glmH1 <- glm(models[["H1"]], family="quasipoisson", data=data)
  estDispersion <- summary(glmH1)$dispersion
  if (estDispersion <= 1) {
    cat("\n***** Warning: According to the overdispersed Poisson model there is no overdispersion.")
    cat("\nThe negative binomial model is therefor not fitted and results are set to missing.\n")
    return(results)
  }

  # Fit model 
  glmH1 <- fitNB(models[["H1"]], data, "optimize")
  if (printmodel) {
    printCaption(paste0("NB: negative binomial loglinear regression model for ", endpoint$name), "-")
    print(summary(glmH1))
  }

  # Prepare for Wald tests and confidence intervals 
  resDF <- df.residual(glmH1)
  estDispersion = glmH1$theta
  estiEffect <- as.numeric(glmH1$coef[2])
  seEffect <- sqrt(vcov(glmH1)[2,2])

  # Wald Interval for difference and equivalence testing 
  intervals <- twoSidedInterval(estiEffect, seEffect, resDF, endpoint$testType, settings$significanceLevel)
  results$ciDiffLeft  <- exp(intervals$leftDiff)
  results$ciDiffRight <- exp(intervals$rightDiff)
  results$ciEquiLeft  <- exp(intervals$leftEqui)
  results$ciEquiRight <- exp(intervals$rightEqui)
  results$estimate    <- exp(estiEffect)

  # Wald pValues
  results$pDiffWald <- waldDiffPvalue(estiEffect, seEffect, resDF, endpoint$testType)
  results$pEquiWald <- waldEquiPvalue(estiEffect, seEffect, resDF, endpoint$testType, endpoint$transformLocLower, endpoint$transformLocUpper)

  # Obtain predictions 
  lsmeans <- predictedMeans(data, glmH1, "Log")
  results$meanComparator = lsmeans$meanComparator
  results$meanTest = lsmeans$meanTest

  # LR tests 
  if (lrDiffDo(estiEffect, 0, endpoint$testType)) {
    glmH0 <- fitNB(models[["H0"]], data, "optimize")
    devDiff = -2 * as.numeric(logLik(glmH0) - logLik(glmH1))
    results$pDiffLR <- lrDiffPvalue(devDiff, 1, resDF, endpoint$testType)
  } else {
    results$pDiffLR <- results$pDiffWald
  }

  # LR test for Equivalence 
  if ((endpoint$testType == "twosided") || (endpoint$testType == "left")) {
    glmH0lower <- fitNB(models[["H0lower"]], data=data, "optimize")
    devDiffLower <- -2 * as.numeric(logLik(glmH0lower) - logLik(glmH1))
  } else {
    devDiffLower = NA
  }
  if ((endpoint$testType == "twosided") || (endpoint$testType == "right")) {
    glmH0upper <- fitNB(models[["H0upper"]], data=data, "optimize")
    devDiffUpper <- -2 * as.numeric(logLik(glmH0upper) - logLik(glmH1))
  } else {
    devDiffUpper = NA
  }
  results$pEquiLR <- lrEquiPvalue(devDiffLower, devDiffUpper, 1, resDF, endpoint$testType)
  return(results)
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

###########################################################################################################################
# Fits the Gamma model for non-negative data and returns P-values and Confidence intervals
gammaAnalysis <- function(data, settings, endpoint, models, printmodel=TRUE) {

  results <- list(model="log(y+1)-transformed counts", 
      ciDiffLeft=NaN, ciDiffRight=NaN, pDiffWald=NaN, pDiffLR=NaN, 
      ciEquiLeft=NaN, ciEquiRight=NaN, pEquiWald=NaN, pEquiLR=NaN,
      estimate=NaN, meanComparator=NaN, meanTest=NaN)

  # Fit model 
  glmH1 <- glm(models[["H1"]], family=Gamma(link=log), data=data)
  if (printmodel) {
    printCaption(paste0("GM: gamma loglinear regression model for ", endpoint$name), "-")
    print(summary(glmH1))
  }

  # Prepare for Wald tests and confidence intervals 
  resDF <- df.residual(glmH1)
  estDispersion <- summary(glmH1)$dispersion
  estiEffect <- as.numeric(glmH1$coef[2])
  seEffect <- sqrt(vcov(glmH1)[2,2])

  # Wald Interval for difference and equivalence testing 
  intervals <- twoSidedInterval(estiEffect, seEffect, resDF, endpoint$testType, settings$significanceLevel)
  results$ciDiffLeft  <- exp(intervals$leftDiff)
  results$ciDiffRight <- exp(intervals$rightDiff)
  results$ciEquiLeft  <- exp(intervals$leftEqui)
  results$ciEquiRight <- exp(intervals$rightEqui)
  results$estimate    <- exp(estiEffect)

  # Wald pValues
  results$pDiffWald <- waldDiffPvalue(estiEffect, seEffect, resDF, endpoint$testType)
  results$pEquiWald <- waldEquiPvalue(estiEffect, seEffect, resDF, endpoint$testType, endpoint$transformLocLower, endpoint$transformLocUpper)

  # Obtain predictions 
  lsmeans <- predictedMeans(data, glmH1, "Log")
  results$meanComparator = lsmeans$meanComparator
  results$meanTest = lsmeans$meanTest

  # LR tests 
  if (lrDiffDo(estiEffect, 0, endpoint$testType)) {
    glmH0 <- glm(models[["H0"]], family=Gamma(link=log), data=data)
    devDiff = deviance(glmH0) - deviance(glmH1)
    results$pDiffLR <- lrDiffPvalue(devDiff, estDispersion, resDF, endpoint$testType)
  } else {
    results$pDiffLR <- results$pDiffWald
  }

  # LR test for Equivalence 
  if ((endpoint$testType == "twosided") || (endpoint$testType == "left")) {
    glmH0lower <- glm(models[["H0lower"]], family=Gamma(link=log), data=data)
    devDiffLower <- deviance(glmH0lower) - deviance(glmH1)
  } else {
    devDiffLower = NA
  }
  if ((endpoint$testType == "twosided") || (endpoint$testType == "right")) {
    glmH0upper <- glm(models[["H0upper"]], family=Gamma(link=log), data=data)
    devDiffUpper <- deviance(glmH0upper) - deviance(glmH1)
  } else {
    devDiffUpper = NA
  }
  results$pEquiLR <- lrEquiPvalue(devDiffLower, devDiffUpper, estDispersion, resDF, endpoint$testType)
  return(results)
}

###########################################################################################################################
# Fits the logNormal model to nonnegative data and returns P-values and Confidence intervals
logPlusMAnalysis <- function(data, settings, endpoint, models, printmodel=TRUE) {

  results <- list(model="lognormal regression model", 
      ciDiffLeft=NaN, ciDiffRight=NaN, pDiffWald=NaN, pDiffLR=NaN, 
      ciEquiLeft=NaN, ciEquiRight=NaN, pEquiWald=NaN, pEquiLR=NaN, 
      estimate=NaN, meanComparator=NaN, meanTest=NaN)

  # Transform data and fit model
  data[endpoint$name] <- log(data[endpoint$name])
  glmH1 <- lm(models[["H1"]], data=data)
  if (printmodel) {
    printCaption(paste0("LN: lognormal regression model for ", endpoint$name), "-")
    print(summary(glmH1))
  }
  resDF <- glmH1$df.residual
  resMS <- deviance(glmH1)/resDF
  estiEffect = as.numeric(glmH1$coef[2])
  seEffect = sqrt(vcov(glmH1)[2,2])

  # Wald Interval for difference and equivalence testing 
  intervals <- twoSidedInterval(estiEffect, seEffect, resDF, endpoint$testType, settings$significanceLevel)
  results$ciDiffLeft  <- exp(intervals$leftDiff)
  results$ciDiffRight <- exp(intervals$rightDiff)
  results$ciEquiLeft  <- exp(intervals$leftEqui)
  results$ciEquiRight <- exp(intervals$rightEqui)
  results$estimate    <- exp(estiEffect)

  # Wald & LR pValues
  results$pDiffWald <- waldDiffPvalue(estiEffect, seEffect, resDF, endpoint$testType)
  results$pDiffLR   <- results$pDiffWald
  results$pEquiWald <- waldEquiPvalue(estiEffect, seEffect, resDF, endpoint$testType, endpoint$transformLocLower, endpoint$transformLocUpper)
  results$pEquiLR   <- results$pEquiWald

  # Obtain predictions 
  lsmeans <- predictedMeans(data, glmH1, "None")
  results$meanComparator = exp(lsmeans$meanComparator + resMS/2)
  results$meanTest = exp(lsmeans$meanTest + resMS/2)
  return(results)
}

######################################################################################################################
# Returns the pvalue for the CGI equivalence test
cgiEquiPvalue <- function(ratio, locLower, locUpper, testType) {
  if (testType == "twosided") {
    pLowerCGI = mean(ratio < locLower, na.rm=TRUE)  # If this is smaller than alfa: reject H0: esti < LocLower
    pUpperCGI = mean(ratio > locUpper, na.rm=TRUE)  # If this is smaller than alfa: reject H0: esti > LocUpper
    return(max(pLowerCGI, pUpperCGI))               # Both one-sided hypothesis must be rejected
  } else if (testType == "left") {
    return(mean(ratio < locLower, na.rm=TRUE))      # If this is smaller than alfa: reject H0: esti < LocLower
  } else { # right
    return(mean(ratio > locUpper, na.rm=TRUE))      # If this is smaller than alfa: reject H0: esti > LocUpper
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
# Returns the confidence interval for the estimate (Wald test)
waldDiffInterval <- function(estiEffect, seEffect, resDF, testType, significanceLevel) {
  if (resDF < 0) {
    # Normal distribution
    if (testType == "twosided") {
      tval <- qnorm(1 - significanceLevel/2)
      return(list(left=estiEffect - tval*seEffect, right=estiEffect + tval*seEffect))
    } else if (testType == "left") {
      tval <- qnorm(1 - significanceLevel)
      return(list(left=NaN, right=estiEffect + tval*seEffect))
    } else { # right
      tval <- qnorm(1 - significanceLevel)
      return(list(left=estiEffect - tval*seEffect, right=NaN))
    }
  } else {
    # Student distribution
    if (testType == "twosided") {
      tval <- qt(1 - significanceLevel/2, resDF)
      return(list(left=estiEffect - tval*seEffect, right=estiEffect + tval*seEffect))
    } else if (testType == "left") {
      tval <- qt(1 - significanceLevel, resDF)
      return(list(left=NaN, right=estiEffect + tval*seEffect))
    } else { # right
      tval <- qt(1 - significanceLevel, resDF)
      return(list(left=estiEffect - tval*seEffect, right=NaN))
    }
  }
}

######################################################################################################################
# Always retuns a two-sided interval Wald interval
twoSidedInterval <- function(estiEffect, seEffect, resDF, testType, significanceLevel) {
  if (resDF < 0) {
    # Normal distribution
    if (testType == "twosided") {
      tDiff <- qnorm(1 - significanceLevel/2)
      tEqui <- qnorm(1 - significanceLevel)
    } else {
      tDiff <- qnorm(1 - significanceLevel)
      tEqui <- tDiff
    }
  } else {
    # Student distribution
    if (testType == "twosided") {
      tDiff <- qt(1 - significanceLevel/2, resDF)
      tEqui <- qt(1 - significanceLevel, resDF)
    } else {
      tDiff <- qt(1 - significanceLevel, resDF)
      tEqui <- tDiff
    }
  }
  return(list(leftDiff=estiEffect - tDiff*seEffect, rightDiff=estiEffect + tDiff*seEffect, 
              leftEqui=estiEffect - tEqui*seEffect, rightEqui=estiEffect + tEqui*seEffect))
}

######################################################################################################################
# Always retuns a two-sided interval Wald interval
twoSidedGCIInterval <- function(sample, testType, significanceLevel) {
  if (testType == "twosided") {
    quan1 <- as.numeric(quantile(sample, probs=c(significanceLevel/2, 1-significanceLevel/2), na.rm=TRUE))
    quan2 <- as.numeric(quantile(sample, probs=c(significanceLevel,   1-significanceLevel),   na.rm=TRUE))
  } else {
    quan1 <- as.numeric(quantile(sample, probs=c(significanceLevel,   1-significanceLevel),   na.rm=TRUE))
    quan2 <- quan1
  }
  return(list(leftDiff=quan1[1], rightDiff=quan1[2], leftEqui=quan2[1], rightEqui=quan2[2]))
}


######################################################################################################################
# Returns the confidence interval for the estimate (Wald test)
waldEquiInterval <- function(estiEffect, seEffect, resDF, testType, significanceLevel) {
  if (resDF < 0) {
    # Normal distribution
    tval <- qnorm(1 - significanceLevel)
    if (testType == "twosided") {
      return(list(left=estiEffect - tval*seEffect, right=estiEffect + tval*seEffect))
    } else if (testType == "left") {
      return(list(left=estiEffect - tval*seEffect, right=NaN))
    } else { # right
      return(list(left=NaN, right=estiEffect + tval*seEffect))
    }
  } else {
    # Student distribution
    tval <- qt(1 - significanceLevel, resDF)
    if (testType == "twosided") {
      return(list(left=estiEffect - tval*seEffect, right=estiEffect + tval*seEffect))
    } else if (testType == "left") {
      return(list(left=estiEffect - tval*seEffect, right=NaN))
    } else { # right
      return(list(left=NaN, right=estiEffect + tval*seEffect))
    }
  }
}




