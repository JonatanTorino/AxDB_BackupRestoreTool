@ECHO OFF
REM Variables
SET SQLServer=%COMPUTERNAME%
SET Folder=%cd%

REM Parametros de entrada
SET paramSQLServer=%~1
SET paramFolder=%~2
SET paramFileName=%~3
SET FileName=%paramFileName%

IF NOT [%paramSQLServer%] == [] GOTO :InitSQLServer
GOTO :CheckParamFolder
:InitSQLServer
SET SQLServer=%paramSQLServer% 

:CheckParamFolder
IF NOT [%paramFolder%] == [] GOTO :InitFolder
GOTO :CheckParamFileName
:InitFolder
SET Folder=%paramFolder%

:CheckParamFileName
IF [%paramFileName%] == [] GOTO :AskFileName
GOTO :Run

:AskFileName
SET /p FileName="RestoreFile: "

:Run
ECHO ON
@echo REM Ejecucion del restore
SQLCMD -S %SQLServer% -E -i "%Folder%\Restore.sql" -v FileName="%Folder%\%FileName%"
pause