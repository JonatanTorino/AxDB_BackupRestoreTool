--FechaHora formatedo a texto
declare @dateTime nvarchar(100) = convert(varchar, getdate(), 20)
set @dateTime = replace(@dateTime, '-', '')
set @dateTime = replace(@dateTime, ':', '')
set @dateTime = replace(@dateTime, ' ', '_')
print @dateTime

--Declaración de variables
declare @parmInFolder nvarchar(255) = N'$(folder)'
declare @parmInPostFixName nvarchar(255) = N'$(postFixName)'
--set @parmInFolder = 'C:\JTORINO\Bimo\SQL\'
declare @backupTo nvarchar(255) = @parmInFolder + HOST_NAME() + '_AxDB_' + @dateTime + '_' + @parmInPostFixName + '.bak'

USE [master]
BACKUP DATABASE [AxDB] TO  DISK = @backupTo
	WITH NOFORMAT, NOINIT,  NAME = N'AxDB-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, COMPRESSION,  STATS = 10
