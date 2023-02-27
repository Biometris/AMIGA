set gb="c:\program files\gen17ed\bin\genbatch.exe"
set genInput="D:\Diverse Opdrachten\AMIGA Power analysis\AmigaPowerAnalysis.Core\Resources\GenstatScripts\AmigaPowerValidation-Simulate.gen"
set genOutput="D:\Diverse Opdrachten\AMIGA Power analysis\AmigaPowerAnalysis.Core\Resources\GenstatScripts\AmigaPowerValidation-Simulate.txt"
set setfile="D:\Diverse Opdrachten\AMIGA Power analysis\TestData\Simple\Simple-Wald\0-Settings.csv"
set csvfile="D:\Diverse Opdrachten\AMIGA Power analysis\TestData\Simple\Simple-Wald\Validation.csv"
%gb% in=%genInput%//200 out=%genOutput%/86 in2=%setfile% out2=%csvfile%
