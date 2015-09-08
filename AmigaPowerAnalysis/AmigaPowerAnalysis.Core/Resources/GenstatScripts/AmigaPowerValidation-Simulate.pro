"
  OP and NB equivalence tests return pval=2 in case the estimate is outside 
  the LOC interval. Such values (either returned by R or GenStat) are not
  involved in the comparison. Values of 2 are in effect in the range [0.5,1]
"
PROCEDURE 'AM1_POWER_VALIDATION_SIMULATE'
OPTION    'PRINT', 'TEST' ; MODE=t,p ; TYPE=*,'scalar' ; \
          VALUES=!t(DETAILS,PVALUES,OP,NB),* ; DEFAULT='',0 ; \
          LIST=yes,no
PARAMETER 'DIRECTORY', 'ENDPOINT', 'REPS', 'EFFECTS', 'DATASETS', \
          'MAXDIFF' ; MODE=p ; SET=yes ; \
          TYPE='text','scalar', 3('variate'), 'pointer' ; \
          DECLARED=5(yes),no ; PRESENT=5(yes),no

CALLS     AM2_GET2SUBDIRS
txconstru [tEndpoint] ENDPOINT ; deci=0

" Import settings "
txconstru [settingsFile] DIRECTORY, ENDPOINT, '-Settings.csv' ; deci=0
enquire   channel=-1 ; name=settingsFile ; exist=found
fault     [expl=!t('settingsFile not found for endpoint ', #tEndpoint)] found.eq.0
import    [print=*] settingsFile ; isave=settings
subset    [settings[1].in.!t(LocLower, LocUpper, SignificanceLevel, \
          NumberOfEvaluationPoints, NumberOfSimulationsGCI, \
          NumberOfSimulatedDataSets)] settings[2] ; tset2
read      [print=* ; channel=tset2 ; setn=yes] set2
scalar    locLower, locUpper, alfa, nEffects, nGCI, nDatasets ; \
          #set2 ; deci=3(*),3(0)
calculate nEffects = 2*nEffects + 3
subset    [settings[1].in.!t(NumberOfReplications)] settings[2] ; tset2
variate   nReplications ; deci=0
read      [print=* ; channel=tset2 ; setn=yes] nReplications
scalar    nReps ; nvalues(nReplications) ; deci=0
subset    [settings[1].in.!t(UseWaldTest)] settings[2] ; tUseWaldTest
scalar    UseWaldTest ; tUseWaldTest.in.'True' ; deci=0
subset    [settings[1].in.!t(ExperimentalDesignType)] settings[2] ; Design
calculate locLowerTrans, locUpperTrans = log(locLower, locUpper)
if ('DETAILS'.IN.PRINT)
    print     settings[]
    print     locLower, locUpper, locLowerTrans, locUpperTrans
    print     nReps, nEffects, nDatasets, nGCI, alfa, UseWaldTest, Design
    text      primport ; 'cata'
  else
    text      primport ; ''
endif

" Import inputfile to define model "
txconstru [inputFile] DIRECTORY, ENDPOINT, '-Input.csv' ; decimals=0
enquire   channel=-1 ; name=inputFile ; exist=found
fault     [expl=!t('inputFile not found for endpoint ', #tEndpoint)] found.eq.0
import    [print=*] inputFile ; isave=input
txconstru [tinput] input
txpositio 2(tinput) ; 'Dummy','Mod' ; posDum,posMod
formula   fitH1 ; !f(GMO)
if (nDum = sum(posDum))
  restrict  posDum ; posDum ; saveset=setDum
  restrict  posDum 
  pointer   [values=input[#setDum]] Dummies
  fclassifi [out=fitH1] #fitH1 + Dummies[]
endif
if (nMod = sum(posMod))
  restrict  posMod ; posMod ; saveset=setMod
  restrict  posMod
  groups    [redefine=yes] input[#setMod] 
  pointer   [values=input[#setMod]] Modifiers
  fclassifi [out=fitH1] #fitH1 + Modifiers[]
endif
if ('RandomizedCompleteBlocks'.in.Design)
    fclassifi [out=fitH1] #fitH1 + Block
  elsif ('CompletelyRandomized'.in.Design)
    " Nothing"
  else
    fault   [expl='Design not implemented.'] 1
endif
fclassifi [out=fitH0] #fitH1 - GMO
\print     fitH1

" Additional settings "
scalar    smallGCI ; 0.0001
scalar    mis
calculate init = urand(34832 ; 1)

" Prepare main directory "
txconstru [tendpoint] ENDPOINT ; deci=0
txconstru [maindir] DIRECTORY, ENDPOINT, '-Endpoint', '\\' ; decimals=0
" Define all subdirectories "
variate   vReps ; !(#nEffects(1...#nReps))    ; deci=0
variate   vEffects ; !((1...#nEffects)#nReps) ; deci=0
txconstru [tReps] vReps
txconstru [tEffects] vEffects
txpad     [padd='0' ; method=before] tReps, tEffects ; width=2
txconstru [subdir] maindir, 'Rep', tReps, '\\Effect', tEffects, '\\'
calculate nsubdir = nvalues(subdir)
txconstru [SET] 'r', tReps, '-e', tEffects
text      [modify=yes] SET ; extra='prSet'

" Define datasets "
txconstru [tdatasets] !(1...nDatasets) ; deci=0
txpad     [padd='0' ; method=before] tdatasets
txconstru [datafiles] 'Data-', tdatasets, '.csv'

" Define models " 

" Printing of model fits "
IF 'OP'.in.PRINT
    text      prOP ; !t(model,sum,est)
  else
    text      prOP ; !t('')
endif
IF 'NB'.in.PRINT
    text      prNB ; !t(model,sum,est)
  else
    text      prNB ; !t('')
endif

" Structures to save results "
pointer   [values=diffLN,diffSQ,diffOP,diffNB] diff
pointer   [values=equiLN,equiSQ,equiOP,equiNB] equi
pointer   [values=OPesti, OPse, OPtval, OPdisp]  OPextra
pointer   [values=NBesti, NBse, OPtval, NBtheta] NBextra
variate   [nvalues=nDatasets] diff[], equi[], OPextra[], NBextra[]

pointer   [nvalues=nsubdir] maxDiff
variate   [nvalues=nval(diff) + nval(equi)] maxDiff[]
calculate maxDiff[] = mis

am2_get2  DIRECTORY ; iidir2


" Loop over subdirs "
calculate minREPS, minEFFECTS, minDATASETS = MIN(REPS,EFFECTS,DATASETS).GE.0
if minDATASETS 
    variate   loopDatasets ; DATASETS
  else
    variate   loopDatasets ; !(1...nDatasets)
endif
for [ntimes=nsubdir ; index=isub]
  scalar    iReps, iEffects ; (vReps, vEffects)$[isub]
  exit      [control=for ; repeat=yes] minREPS.AND.(iReps.NI.REPS)
  exit      [control=for ; repeat=yes] minEFFECTS.AND.(iEffects.NI.EFFECTS)
  text      isubdir ; subdir$[isub]
  txconstru [Datafiles] isubdir, datafiles
  calculate diff[], equi[], OPextra[], NBextra[] = mis
  for [ntimes=nDatasets ; index=iDataset]
    exit      [control=for ; repeat=yes] (iDataset.NI.loopDatasets)
	txconstru [tmessage] ENDPOINT, '  Rep ', iReps, \
              '  Effect ', iEffects, '  Dataset ', iDataset ; deci=0
    IF SUM(!t(OP,NB).IN.PRINT)
      txconstru [caption] iidir2, tmessage
      caption   caption ; style=meta
    endif
    " Import Data and define factors if in model "
    text      iDatafile ; Datafiles$[iDataset]
	enquire   channel=-1 ; name=iDatafile ; exist=found
    fault     [expl=!t('dataFile not found for endpoint', #tmessage)] (found.eq.0)
    import    [print=#primport] iDatafile ; isave=isave
    calculate nval = nvalues(Response)
    variate   [nvalues=nval] yy
    if ('RandomizedCompleteBlocks'.in.Design)
      groups    [redefine=yes] Block
    endif
    if (nMod)
      groups    [redefine=yes] Modifiers[]
    endif

    if (primport.nes.'')
      " Normal analysis for checking GCI "
      calculate tmpnGCI,nGCI = nGCI,1000000
      variate   [nvalues=nGCI] chi, ratio, random[1,2]
      model     Response
      fit       [print=* ; tprob=yes ; nomes=res,lev,disp] #fitH1
      rkeep     esti=vesti ; se=vse ; dev=dev ; df=df
      calculate esti,se = (vesti,vse)$[2]
      calculate pLower = cut((esti-locLower)/se ; df)
      calculate pUpper = clt((esti-locUpper)/se ; df)
      predict   [print=* ; pred=pred ; vcov=vcov] GMO ; !(0,1)
      calculate copyVcov = vcov
      calculate chi = dev/grchi(nGCI ; df)
      grmulti   [nvalues=nGCI ; vcov=copyVcov] random
      calculate random[] = #pred + random[]*sqrt(chi/(dev/df))
      calculate ratio = random[2] - random[1]
      calculate pLowerGCI = mean(ratio .lt. locLower)
      calculate pUpperGCI = mean(ratio .gt. locUpper)
      print     'Checking NormalGCI', nGCI, pLower, pLowerGCI, \
                pUpper, pUpperGCI ; deci=2(0),4(4)
      equate    '' ; primport
      calculate nGCI = tmpnGCI
      delete    [redefine=yes] chi, random, ratio
    endif

    " LN difference "
    calculate yy = log(Response+1)
    model     yy
    fit       [print=* ; tprob=yes ; nomes=res,lev,disp] #fitH1
    rkeep     esti=vesti ; se=vse ; dev=dev ; df=df
    calculate esti,se = (vesti,vse)$[2]
    calculate diff[1]$[iDataset] = 2*cut(abs(esti/se) ; df)

    " LN equivalence "
    if (nDum.eq.0) 
        predict   [print=* ; pred=pred ; vcov=vcov] GMO ; !(0,1)
      else
        predict   [print=* ; pred=pred ; vcov=vcov] GMO,Dummies[] ; \
                  !(0,1),#nDum(0)
    endif
    calculate copyVcov = vcov
    calculate chi = dev/grchi(nGCI ; df)
    grmulti   [nvalues=nGCI ; vcov=copyVcov] random
    calculate random[] = #pred + random[]*sqrt(chi/(dev/df))
    calculate random[] = exp(random[] + chi/2) - 1
    calculate random[] = bound(random[] ; smallGCI ; mis)
    calculate ratio = random[2]/random[1]
    calculate pLower = mean(ratio .lt. locLower)
    calculate pUpper = mean(ratio .gt. locUpper)
    calculate equi[1]$[iDataset] = vmax(!p(pLower, pUpper))

    " SQ difference "
    calculate yy = sqrt(Response)
    model     yy
    fit       [print=* ; tprob=yes ; nomes=res,lev,disp] #fitH1
    rkeep     esti=vesti ; se=vse ; dev=dev ; df=df ; pear=pear
    calculate esti,se = (vesti,vse)$[2]
    calculate diff[2]$[iDataset] = 2*cut(abs(esti/se) ; df)

    " SQ equivalence "
    if (nDum.eq.0) 
        predict   [print=* ; pred=pred ; vcov=vcov] GMO ; !(0,1)
      else
        predict   [print=* ; pred=pred ; vcov=vcov] GMO,Dummies[] ; \
                  !(0,1),#nDum(0)
    endif
    calculate copyVcov = vcov
    calculate chi = dev/grchi(nGCI ; df)
    grmulti   [nvalues=nGCI ; vcov=copyVcov] random
    calculate random[] = #pred + random[]*sqrt(chi/(dev/df))
    calculate random[] = random[]*random[] + chi
    calculate ratio = random[2]/random[1]
    calculate pLower = mean(ratio .lt. locLower)
    calculate pUpper = mean(ratio .gt. locUpper)
    calculate equi[2]$[iDataset] = vmax(!p(pLower, pUpper))

    " OP difference "
    model     [dist=poisson ; ; disp=* ; dmethod=pear] Response
    fit       [print=#prOP ; tprob=yes ; nomes=res,lev,disp] #fitH1
    rkeep     esti=vesti ; se=vse ; dev=dev ; df=df ; pear=pear
    calculate esti,se = (vesti,vse)$[2]
    calculate estDispersion = pear/df
    calculate OPextra[1,2,4]$[iDataset] = esti,se,estDispersion
    if UseWaldTest
        calculate pval = 2*cut(abs(esti/se) ; df)
      else
        fit       [print=#prOP ; tprob=yes ; nomes=res,lev,disp] #fitH0
        rkeep     dev=dev0 ; df=df0
        calculate testStat = (dev0-dev)/(df0-df)/estDispersion
        calculate pval = cuf(testStat ; df0-df ; df)
    endif
    calculate diff[3]$[iDataset] = pval

    " OP equivalence "
    if UseWaldTest
        calculate pLower = cut((esti-locLowerTrans)/se ; df)
        calculate pUpper = clt((esti-locUpperTrans)/se ; df)
        calculate pval = vmax( !p(pLower, pUpper))
      else
        if (esti.lt.locLowerTrans) .or. (esti.gt.locUpperTrans)
            calculate pval = 2
          else
            model     [dist=poisson ; ; disp=* ; offset=LowOffset] Response
            fit       [print=#prOP ; nomes=res,lev,disp] #fitH0
            rkeep     dev=devLower
            calculate pLower = cuf((devLower - dev)/estDispersion ; 1 ; df)
            model     [dist=poisson ; ; disp=* ; offset=UppOffset] Response
            fit       [print=#prOP ; nomes=res,lev,disp] #fitH0
            rkeep     dev=devUpper
            calculate pUpper = cuf((devUpper - dev)/estDispersion ; 1 ; df)
            calculate pval = vmax(!p(pLower, pUpper))/2
        endif
    endif
    calculate equi[3]$[iDataset] = pval

    " NB difference ; GenStat uses df=inf for calculation of Wald test "
    model     [dist=negative ; link=log] Response
    r2negbin  [print=#prNB ; _2loglik=dev ; aggregation=agg ; \
              tprob=yes ; nomes=res,lev,disp,warn] #fitH1
    rkeep     esti=vesti ; se=vse ; df=df ;
    calculate esti,se = (vesti,vse)$[2]
    calculate NBextra[1,2,4]$[iDataset] = esti,se,agg
    if UseWaldTest
        calculate pval = 2*cut(abs(esti/se) ; df)
      else
        r2negbin  [print=#prNB ; tprob=yes ; _2loglik=dev0 ; \
                  tprob=yes ; nomes=res,lev,disp,warn] #fitH0
        calculate pval = cuchi(dev0-dev ; 1)
    endif
    calculate diff[4]$[iDataset] = pval

    " NB equivalence "
    if UseWaldTest
        calculate pLower = cut((esti-locLowerTrans)/se ; df)
        calculate pUpper = clt((esti-locUpperTrans)/se ; df)
        calculate pval = vmax( !p(pLower, pUpper))
      else
        if (esti.lt.locLowerTrans) .or. (esti.gt.locUpperTrans)
            calculate pval = 2
          else
            model     [dist=negative ; link=log ; offset=LowOffset] Response
            r2negbin  [print=#prNB ; _2loglik=devLower ; \
                      tprob=yes ; nomes=res,lev,disp,warn] #fitH0
            calculate pLower = cuchi((devLower - dev) ; 1)
            model     [dist=negative ; link=log ; offset=UppOffset] Response
            r2negbin  [print=#prNB ; _2loglik=devUpper ; \
                      tprob=yes ; nomes=res,lev,disp,warn] #fitH0
            calculate pUpper = cuchi((devUpper - dev) ; 1)
            calculate pval = vmax(!p(pLower, pUpper))/2
        endif
    endif
    calculate equi[4]$[iDataset] = pval
    exit      TEST
  endfor
  calculate OPextra[3],NBextra[3] = OPextra[1],NBextra[1]/OPextra[2],NBextra[2]

  text      which ; !t(diffLN,diffSQ,diffOP,diffNB, equiLN,equiSQ,equiOP,equiNB)
  concatena [Rwhich] 'R', which
  concatena [Gwhich] 'G', which

  " Compare with results from R "
  if UseWaldTest
      txconstru [Rresults] isubdir, '00-PvaluesWald.csv'
    else
      txconstru [Rresults] isubdir, '00-PvaluesLR.csv'
  endif
  import    [print=*] Rresults ; isave=saveR
  pointer   [nvalues=which] compare
  variate   [nvalues=nDatasets] compare[]
  " Take special care of pvalues larger than 2 for equiOP and equiNB "
  calculate compare[1...6] = abs(diff[1...4], equi[1...2] - saveR[1...6])
  for yyG=equi[3,4] ; yyR=saveR[7,8] ; cc=compare[7,8]
    calculate cond1,cond2 = (yyG,yyR).gt.1.5
    restrict  yyG,yyR,cc ; cond1.and.cond2 ; null=null
    if (null.eq.0)
      calculate cc = 0
    endif
    restrict  yyG,yyR,cc
    restrict  yyG,yyR,cc ; (cond1).and.(.not.cond2) ; null=null
    if (null.eq.0)
      calculate cc = 2*(yyR.le.0.4)
    endif
    restrict  yyG,yyR,cc
    restrict  yyG,yyR,cc ; (.not.cond1).and.(cond2) ; null=null
    if (null.eq.0)
      calculate cc = 2*(yyG.le.0.4)
    endif
    restrict  yyG,yyR,cc
    restrict  yyG,yyR,cc ; (.not.cond1).and.(.not.cond2) ; null=null
    if (null.eq.0)
      calculate cc = abs(yyG-yyR)
    endif
    restrict  yyG,yyR,cc
  endfor
  calculate smaxWald[1...8] = max(compare[])
  variate   maxDiff[isub] ; !(#smaxWald)
  
  " Detailed printing "
  if 'PVALUES'.IN.PRINT
    txconstru [caption] iidir2, '  Endpoint ', ENDPOINT, '  Rep ', iReps, \
              '  Effect ', iEffects, '  All Datasets' ; deci=0
    caption   caption ; style=meta
    variate   iData ; !(1...#nDatasets) ; deci=0
    restrict  iData, saveR[], OPextra[],NBextra[], diff[],equi[], compare[] ; \
              iData.in.loopDatasets
    print     iData, OPextra[], saveR[9]  ; deci=0,*,*,3,3,3
    print     iData, NBextra[], saveR[10] ; deci=0,*,*,3,3,3
    print     diff[], equi[] ; field=10 ; deci=3 ; heading=#Gwhich
    print     saveR[1...8] ; field=10 ; deci=3 ; heading=#Rwhich
    calculate compare[] = mvinsert(compare[] ; compare[].lt.0.0005)
    print     [mis='-'] compare[] ; field=10 ; deci=3 ; heading=#which
    restrict  iData, saveR[], OPextra[],NBextra[], diff[],equi[], compare[]
  endif

  \print     which, maxDiff[isub] ; field=-12 ; deci=2
  \print     max(maxDiff[isub]) ; field=-24 ; deci=2
  exit      TEST
endfor
vequate   maxDiff ; MAXDIFF
variate   [modify=yes] MAXDIFF[] ; extra=#which
TXCONSTRU [extra] 'Endp-', ENDPOINT ; DECI=0
text      MAXDIFF[0] ; SET ; extra=extra
calculate mxDiff[1...8] = MAXIMUM(MAXDIFF[1...8])
txconstru [caption] iidir2, '  Endpoint ', ENDPOINT, '  Overall' ; deci=0
caption   caption ; style=meta
print     [mis='-' ; iprint=extra] MAXDIFF[] ; \
          field=-9 ; deci=1 ; skip=0,2,3(0),2,3(0)
print     [mis='-' ; iprint=extra ; squash=yes] #extra, mxDiff[] ; \
          field=-9 ; deci=1 ; skip=0,2,3(0),2,3(0) ; heading=*,#which
skip      [filetype=output] 1

ENDPROCEDURE

" =========================================================================== "
" =========================================================================== "
" =========================================================================== "
PROCEDURE 'AM2_GET2SUBDIRS'
PARAMETER 'OLD','NEW' ; MODE=p ; TYPE='text' ; SET=yes 
CONCATENA [NEW=new] OLD ; WIDTH=CHARACTERS(OLD) - 1
TXPOSITIO [REVERSE=yes] new ; SUBTEXT='\\' ; POSITION=pos
CONCATENA new ; WIDTH=pos-1
TXPOSITIO [REVERSE=yes] new ; SUBTEXT='\\' ; POSITION=pos
CONCATENA [NEWTEXT=NEW] OLD ; SKIP=pos
CONCATENA NEW ; ; WIDTH=CHARACTERS(NEW) - 1
SREPLACE  ['\\' ; '/'] NEW
ENDPROCEDURE
return
