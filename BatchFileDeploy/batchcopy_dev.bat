@echo off 
set source="C:\Users\sinasst-adm\source\repos\PowerPlatform.Service\PowerBiEmbedder\PowerBiEmbedder\bin\Release\net6.0"
set destination="\\ISI-APP-SAPAZ01\Integration-PP-PBI\dev"

echo Copying files from %source% to %destination%
robocopy %source% %destination% /XF appsettings.json appsettings.QA.json appsettings.Preproduction.json appsettings.Production.json

echo Files copied successfully!
pause

