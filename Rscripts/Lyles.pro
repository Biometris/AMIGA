PROCEDURE 'L1CREATE'
OPTION    'ISAVE' ; MODE=p ; TYPE='pointer' ; \
          SET=yes ; DECLARED=yes ; PRESENT=yes
PARAMETER 'MUCOMPARATOR', 'MEAN', 'COMPARISON', 'DUMMY', 'MODIFIERS' ; \
          MODE=p ; \
          TYPE='scalar', 2('variate'), 'pointer', 'factor' ; \
          SET=yes ; DECLARED=no ; PRESENT=no

DUMMY     Endpoint, ComparisonId, NumberOfInteractions, NumberOfModifiers, \
          Block, MainPlot, SubPlot, Variety, Ranking, Spraying, Mean, \
          Comparison ; ISAVE[]

TXCONSTRU [tsave] ISAVE
CALCULATE posfactor[1,2] = POSITION('Variety','Mean' ; tsave)  - (0,1)
VARIATE   ifactor ; !(posfactor[1]...posfactor[2]) ; DECIMALS=0
CALCULATE nfactor = NVALUES(ifactor)
GROUPS    [REDEFINE=yes] ISAVE[#ifactor]

" Redefine ordering of factor labels of Variety; this ensures that dum[1]
  defines the parameter of interest "
CALCULATE nLevelsVariety = nlevels(Variety)
IF nLevelsVariety.GT.2
    GETATTRIB [ATTRIBUTE=labels] Variety ; SAVE=labVariety
    SUBSET    [labVariety[].NI.!t(GMO,Comparator)] labVariety[] ; newlabels
    TEXT      newlabels ; !t(GMO,Comparator,#newlabels)
  ELSE
    TEXT    newlabels ; !t(GMO,Comparator)
ENDIF
FACAMEND  Variety  ; newlabels
\TABULATE  [CLASS=Variety ; print=count]

" Define comparison "
VARIATE   COMPARISON ; (Comparison.IN.'IncludeGMO') - \
          (Comparison.IN.'IncludeComparator') ; decimals=0
RESTRICT  Mean ; ABS(COMPARISON )
CALCULATE MUCOMPARATOR = MEAN(Mean)
RESTRICT  Mean

" Determine interaction factors and create dummies for Interaction*Variety "
CALCULATE nInteractions = MEAN(NumberOfInteractions)
VARIATE   cond ; !(1...#nfactor)
SUBSET    [((cond-1).LE.nInteractions)] ifactor ; intfactor
POINTER   [VALUES=ISAVE[#intfactor]] intfactors
FACPRODUC intfactors ; Interactions
CALCULATE DUMMY[1] = COMPARISON.IN.1
if SUM(COMPARISON.eq.0)
  SUBSET    [COMPARISON.EQ.0 ; SETLEVELS=yes] Interactions ; subInteractions
  GETATTRIB [ATTRIBUTE=levels,labels] subInteractions ; SAVE=levels
  SCALAR    counter ; 1
  FOR ll=#levels['levels']
    CALCULATE counter = counter + 1
    CALCULATE DUMMY[counter] = Interactions.IN.ll
  ENDFOR
ENDIF
CALCULATE ndum,ndum1 = NVALUES(DUMMY) - (0,1)
VARIATE   [MODIFY=yes] DUMMY[] ; decimals=0
IF 0  " Printing of dummies for interaction "
  CALCULATE nn = NVALUES(intfactors)+1
  PRINT     intfactors[], comparison, DUMMY[] ; FIELD=#nn(12), #ndum(7)
ENDIF

" Determine Modifiers "
CALCULATE nModifiers = MEAN(NumberOfModifiers)
IF nModifiers
  SUBSET    [ifactor.NI.intfactor] ifactor ; modfactor
  POINTER   [VALUES=ISAVE[#modfactor]] modfactors
  FACPRODUC modfactors ; MODIFIERS
  TABULATE  [CLASS=MODIFIERS] Mean ; MEAN=meanModifier
  IF VARIANCE(meanModifier).eq.0
    " IT Is NOT a true modifier "
    CALCULATE nModifiers = 0
  ENDIF
  DELETE    [REDEFINE=yes] meanModifier
ENDIF
IF (nModifiers.eq.0)
  FACTOR    [LEVELS=1] MODIFIERS ; 1 + 0*DUMMY[1]
ENDIF
ENDPROCEDURE

"============================================================================="
"============================================================================="
"============================================================================="

PROCEDURE 'L2LYLES'
OPTION    'DISTRIBUTION', 'POWERLAW', 'DESIGN', 'SIGNIFICANCELEVEL', \
          'MUCOMPARATOR', 'CVCOMPARATOR', 'CVBLOCK', \
          'MEAN', 'COMPARISON', 'DUMMY', 'MODIFIERS', \
          'LOCLOWER', 'LOCUPPER', 'ANALYSIS' ; \
          MODE=t,p,t,p,  3(p),  4(p),  2(p),t  ; \
          TYPE=*, 'scalar', *, 'scalar', \
              3('scalar'), \
              2('variate'), 'pointer', 'factor', \
              2('scalar'), * ; \
          SET=yes ; DECLARED=yes ; PRESENT=yes ; \
          VALUES=!t(POISSON, OPOISSON, POWER, NEGATIVEBINOMIAL), *, \
            !T(RANDOMIZED,BLOCK,SPLITPLOT), *, 3(*),4(*),\
            2(*), !t(LN,SQ,PO,OP,NB) ; \
          DEFAULT='NEGATIVEBINOMIAL',  1.5, 'RANDOMIZED', 0.05, \
            3(*),  4(*),  2(*), !t(LN,SQ,PO,OP,NB) ; \
          LIST=4(no), 3(no), 4(no), 2(no),yes
PARAMETER 'RATIO', 'NREPLICATES', 'NONCENTRALITY', 'POWER', 'DF' ; MODE=p ; \
          TYPE=2('scalar'), 2('variate'), 'scalar' ; \
          SET=yes ; DECLARED=2(yes,no),no ; PRESENT=2(yes,no),no

SCALAR    mis
SCALAR    minimumProbSum ; 0.9999

CALLS     CNTPROBABILITY

" Create Design for number of reps "
SCALAR    iiRatio, iiReps ; RATIO, NREPLICATES
CALCULATE iiplots = iiReps * (nplots = NVALUES(MEAN))
FACTOR    [NVALUES=iiplots ; LEVELS=iiReps] qBlock
FACTOR    [NVALUES=iiplots ; LEVELS=nplots] qPlot
GENERATE  qBlock, qPlot
POINTER   [NVALUES=NVALUES(DUMMY)] qdum, zdum
VARIATE   [NVALUES=iiplots] qdum[], qmean, qcomparison, qsig2, qvar
EQUATE    MEAN, COMPARISON, DUMMY[] ; qmean, qcomparison, qdum[]

" Apply Ratio effect"
CALCULATE qmean = ((1-qdum[1]) + qdum[1]*iiRatio) * qmean

" Deal with blocking random effect "
IF (DESIGN.eqs.'RANDOMIZED')
    TEXT      design ; 'RANDOMIZED'
    CALCULATE sigBlock = 0
  ELSIF DESIGN.eqs.'BLOCK'
    IF (CVBLOCK.le.0)
        TEXT      design ; 'RANDOMIZED'
        CALCULATE sigBlock = 0
      ELSE
        TEXT      design ; 'BLOCK'
        CALCULATE sigBlock = SQRT(LOG((CVBLOCK/100)**2 + 1))
    ENDIF
  ELSIF DESIGN.eqs.'SPLITPLOT'
    FAULT   [EXPL=!t('Misspecification of design')]
    TEXT      design ; 'splitplot'
  ELSE
    FAULT   [EXPL=!t('Misspecification of design')]
ENDIF
" Apply Blocking effect by means of Blom scores "
IF design.eqs.'BLOCK'
  VARIATE   [VALUES=1...#iiReps] blockeff
  CALCULATE blockeff = EXP(sigBlock*NED((blockeff-0.375)/(iiReps+0.25)))
  CALCULATE qmean = qmean * NEWLEVELS(qBlock ; blockeff)
ENDIF

" Deal with distribution and calculate overdispersion factor "
TEXT      dist ; DISTRIBUTION
IF DISTRIBUTION.eqs.'NEGATIVE'
    CALCULATE sig2NB = (CVCOMPARATOR/100)**2 - 1/MUCOMPARATOR
    CALCULATE qsig2 = sig2NB
    CALCULATE qvar = qmean + qsig2*qmean*qmean
  ELSIF DISTRIBUTION.eqs.'OPOISSON'
    CALCULATE sig2OP = (CVCOMPARATOR/100)**2 * MUCOMPARATOR
    CALCULATE qsig2 = sig2OP
    CALCULATE qvar = qsig2*qmean
  ELSIF DISTRIBUTION.eqs.'POWER'
    TEXT      dist ; 'NEGATIVEBINOMIAL'
    CALCULATE sig2PW = (CVCOMPARATOR/100)**2 * MUCOMPARATOR**(2-POWERLAW)
    CALCULATE qsig2 = sig2PW*qmean**(POWERLAW-2) - 1/qmean
    CALCULATE qvar = sig2PW*qmean**POWERLAW
  ELSIF DISTRIBUTION.eqs.'POISSON'
    TEXT      dist ; 'poisson'
    CALCULATE qsig2 = 1
    CALCULATE qvar = qmean
  ELSE
    FAULT     [DIAG=fault ; expl=!t('Wrong setting of DISTRIBUTION')]
ENDIF

" Looping over DIFFERENT values in qmean to create synthetic dataset"
GROUPS    qmean ; fMean ; LEVELS=levMean
CALCULATE nlevMean = NVALUES(levMean)
VARIATE   [NVALUES=nlevMean] nobs, sumww, factor ; DECI=0,4,0
VARIATE   [NVALUES=iiplots] eeLN, vvLN, eeSQ, vvSQ
POINTER   [NVALUES=iiplots] ww, yy

FOR [NTIMES=nlevMean ; INDEX=jj]
  SCALAR    kmean ; (levMean)$[jj]
  RESTRICT  qsig2,qvar ; qmean.eq.kmean
  CALCULATE ksig2 = MEAN(qsig2)
  CALCULATE ksd = SQRT(MEAN(qvar))
  RESTRICT  qsig2, qvar
  FOR ff=5,7,9,11,15
    " Use different factors to ensure that the sum of the probs equals 1 "
    CALCULATE factor$[jj] = ff
    CALCULATE lowcount = BOUND(floor(kmean - ff*ksd) ; 0 ; mis)
    CALCULATE uppcount = CEILING(kmean + ff*ksd)
    CALCULATE nobs$[jj] = uppcount - lowcount + 1
    VARIATE   [VALUES=#lowcount...#uppcount] yydum, wwdum
    CNTPROBAB [DIST=#dist] yydum ; MEAN=kmean ; DISP=ksig2 ; PROB=wwdum
    CALCULATE sumww$[jj] = (ksumww = SUM(wwdum))
    EXIT      [CONTROL=for] (ksumww.GT.minimumProbSum)
  ENDFOR
  " Copy values "
  RESTRICT  qmean ; qmean.EQ.kmean ; SAVESET=saveset
  RESTRICT  qmean
  CALCULATE yy[#saveset] = yydum
  CALCULATE ww[#saveset] = wwdum
  " For LN and SQ "
  CALCULATE ee1 = SUM(wwdum * LOG(yydum+1))
  CALCULATE vv1 = SUM(wwdum * (LOG(yydum+1)-ee1)**2)
  CALCULATE (eeLN)$[saveset] = ee1
  CALCULATE (vvLN)$[saveset] = vv1
  CALCULATE ee1 = SUM(wwdum * SQRT(yydum))
  CALCULATE vv1 = SUM(wwdum * (SQRT(yydum)-ee1)**2)
  CALCULATE (eeSQ)$[saveset] = ee1
  CALCULATE (vvSQ)$[saveset] = vv1
ENDFOR
VARIATE   response, weight ; !(#yy), !(#ww)

" Expand design and define model "
CALCULATE fullNobs = NEWLEVELS(fMean ; nobs)
FEXPAND   !(1...#iiplots) ; NOBS=fullNobs ; FACTOR=id
FEXPAND   qdum[] ; NOBS=fullNobs ; VARIATE=zdum[]
FCLASSIFI [OUT=modelH1] zdum[]

IF NLEVELS(MODIFIERS).GT.1
  DUPLICATE MODIFIERS ; qModifiers ; VALUES=!((#MODIFIERS)#iiReps)
  FEXPAND   !(#qModifiers) ; NOBS=fullNobs ; FACTOR=zModifiers
  FCLASSIFI [OUT=modelH1] #modelH1 + zModifiers
ENDIF

IF design.EQS.'BLOCK'
  FEXPAND   !(#qBlock) ; NOBS=fullNobs ; FACTOR=zBlock
  FCLASSIFI [OUT=modelH1] #modelH1 + zBlock
ENDIF

" Define models; ensure that zdum[1] is last parameter "
FCLASSIFI [OUT=modelH0] #modelH1 - zdum[1]
FCLASSIFI [OUT=modelH1] #modelH0 + zdum[1]

" Calculate offsets for equivalence testing "
CALCULATE loglowerLOC,logupperLOC = LOG(LOCLOWER, LOCUPPER)
VARIATE   lowOffset,uppOffset ; (loglowerLOC,logupperLOC) * (zdum[1].eq.1)

SCALAR    LNnc,SQnc,POnc,OPnc,NBnc, LNnce,SQnce,POnce,OPnce,NBnce
SCALAR    powLN,powSQ,powPO,powOP,powNB, powLNe,powSQe,powPOe,powOPe,powNBe
CALCULATE critvalChi = EDCHI(1-SIGNIFICANCELEVEL ; 1)

" Ready to fit the models "
IF 'LN'.IN.ANALYSIS  " LN: logarithmic transformation "
  VARIATE   tresponse ; LOG(response+1)
  CALCULATE meanVariance = MEAN(vvLN)
  MODEL     [DIST=normal ; WEIGHT=weight] tresponse
  TERMS     #modelH1
  FIT       [PRINT=* ; NOMES=lev,res,disp] #modelH0
  RKEEP     DEV=Ddev0 ; DF=df0 ; TDF=tdf0
  ADD       [PRINT=* ; NOMES=lev,res,disp] zdum[1]
  RKEEP     DEV=dev1 ; DF=df1 ; TDF=tdf1

  " Degrees of freedom for residual; critical F "
  CALCULATE iidf = iiplots - (nParameters = tdf1 - df1 + 1)
  CALCULATE critvalF = EDF(1-SIGNIFICANCELEVEL ; 1 ; iidf)
  CALCULATE LNnc = (Dnoncentral = BOUND((Ddev0-dev1)/meanVariance ; 0 ; mis))
  CALCULATE powLN = CUF(critvalF ; 1 ; iidf ; Dnoncentral)
ENDIF

IF 'SQ'.IN.ANALYSIS  " SQ: squared root transformation "
  VARIATE   tresponse ; SQRT(response)
  CALCULATE meanVariance = MEAN(vvSQ)
  MODEL     [DIST=normal ; WEIGHT=weight] tresponse
  TERMS     #modelH1
  FIT       [PRINT=* ; NOMES=lev,res,disp] #modelH0
  RKEEP     DEV=Ddev0 ; DF=df0 ; TDF=tdf0
  ADD       [PRINT=* ; NOMES=lev,res,disp] zdum[1]
  RKEEP     DEV=dev1 ; DF=df1 ; TDF=tdf1

  " Degrees of freedom for residual; critical F "
  CALCULATE iidf = iiplots - (nParameters = tdf1 - df1 + 1)
  CALCULATE critvalF = EDF(1-SIGNIFICANCELEVEL ; 1 ; iidf)
  CALCULATE SQnc = (Dnoncentral = BOUND((Ddev0-dev1)/meanVariance; 0; mis))
  CALCULATE powSQ = CUF(critvalF ; 1 ; iidf ; Dnoncentral)
ENDIF

IF SUM(!t('PO','OP').IN.ANALYSIS)  " PO: Poisson "
  MODEL     [DIST=poisson ; WEIGHT=weight] response
  TERMS     #modelH1
  FIT       [PRINT=* ; NOMES=lev,res,disp] #modelH0
  RKEEP     DEV=Ddev0 ; DF=df0 ; TDF=tdf0
  ADD       [PRINT=* ; NOMES=lev,res,disp] zdum[1]
  RKEEP     DEV=dev1 ; DF=df1 ; TDF=tdf1 ; ESTI=esti
  CALCULATE POnc = (Dnoncentral = BOUND(Ddev0 - dev1 ; 0 ; mis))
  CALCULATE powPO = CUCHI(critvalChi ; 1 ; Dnoncentral)

  " PO: equivalence "
  CALCULATE effGMO = esti$[nParameters]
  IF (effGMO.LE.0)
      MODEL     [DIST=poisson ; WEIGHT=weight ; OFFSET=lowOffset] response
    ELSE
      MODEL     [DIST=poisson ; WEIGHT=weight ; OFFSET=uppOffset] response
  ENDIF
  FIT       [PRINT=*] #modelH0
  RKEEP     DEV=Edev0
  CALCULATE POnce = (Enoncentral = bound(Edev0 - dev1 ; 0 ; mis))
  CALCULATE powPOe = CUCHI(critvalChi ; 1 ; Enoncentral)

  " OP "
  CALCULATE meanVariance = MEAN(qvar/qmean)
  CALCULATE iidf = iiplots - (nParameters = tdf1 - df1 + 1)
  CALCULATE critvalF = EDF(1-SIGNIFICANCELEVEL ; 1 ; iidf)
  CALCULATE OPnc = (Dnoncentral = BOUND((Ddev0-dev1)/meanVariance; 0; mis))
  CALCULATE powOP = CUF(critvalF ; 1 ; iidf ; Dnoncentral)
  CALCULATE OPnce = (Enoncentral = BOUND((Edev0-dev1)/meanVariance; 0; mis))
  CALCULATE powOPe = CUF(critvalF ; 1 ; iidf ; Enoncentral)
ENDIF

IF 'NB'.IN.ANALYSIS  " NB: negative binomial "
  CALCULATE Ddev0,Edev0,dev1 = mis
  MODEL     [DIST=negative ; LINK=log ; WEIGHT=weight] response
  R2NEGBIN  [PRINT=* ; _2LOG=Ddev0 ; NOMES=lev,res,disp,warn] #modelH0
  R2NEGBIN  [PRINT=* ; _2LOG=dev1 ; NOMES=lev,res,disp,warn] #modelH1
  RKEEP     EXIT=exit ; ESTI=esti
  CALCULATE NBnc = (Dnoncentral = BOUND(Ddev0 - dev1 ; 0 ; mis))
  CALCULATE powNB = CUCHI(critvalChi ; 1 ; Dnoncentral)

  " NB: equivalence "
  CALCULATE effGMO = esti$[nParameters]
  IF (effGMO.LE.0)
      MODEL     [DIST=negative ; LINK=log ; WEIGHT=weight ; OFFSET=lowOffset] \
                response
    ELSE
      MODEL     [DIST=negative ; LINK=log ; WEIGHT=weight ; OFFSET=uppOffset] \
                response
  ENDIF
  R2NEGBIN  [PRINT=* ; _2LOG=Edev0 ; NOMES=lev,res,disp,warn] #modelH0
  CALCULATE NBnce = (Enoncentral = BOUND(Edev0 - dev1 ; 0 ; mis))
  CALCULATE powNBe = CUCHI(critvalChi ; 1 ; Enoncentral)
ENDIF

VARIATE   [NVALUES=!t(LNd,SQd,POd,OPd,NBd,LNe,SQe,POe,OPe,NBe)] \
          NONCENTRALITY,POWER ; \
          !(LNnc,SQnc,POnc,OPnc,NBnc, LNnce,SQnce,POnce,OPnce,NBnce), \
          !(powLN,powSQ,powPO,powOP,powNB, powLNe,powSQe,powPOe,powOPe,powNBe)
SCALAR    DF ; iidf
ENDPROCEDURE

return
