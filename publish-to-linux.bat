REM Run the dotnet publish command and create publish.tar.bz2
cd dotnet-webapp-ec2
dotnet publish -o publish --os linux
cd publish
tar cjvf ..\publish.tar.bz2 .

REM scp publish.tar.bz2 to the server
cd ..
scp -i %NORTHWIND_KEY% .\publish.tar.bz2 %NORTHWIND_USER%@%NORTHWIND_SERVER%:~/dotnet-webapp-ec2

REM run the deploy script on the server
ssh -i %NORTHWIND_KEY% %NORTHWIND_USER%@%NORTHWIND_SERVER% "source /home/%NORTHWIND_USER%/.bash_profile && /home/%NORTHWIND_USER%/scripts/deploy-publish-file.sh"
