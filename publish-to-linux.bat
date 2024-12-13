REM Run the dotnet publish command and create publish.tar.bz2
cd dotnet-webapp-ec2
dotnet publish -o publish --os linux
cd publish
tar cjvf ..\publish.tar.bz2 .

REM scp publish.tar.bz2 to the server
cd ..
scp -i %NORTHWIND_KEY% .\publish.tar.bz2 ec2-user@ec2-34-227-151-78.compute-1.amazonaws.com:~/dotnet-webapp-ec2

REM run the deploy script on the server
ssh -i %NORTHWIND_KEY% ec2-user@ec2-34-227-151-78.compute-1.amazonaws.com "source /home/ec2-user/.bash_profile && /home/ec2-user/scripts/deploy-publish-file.sh"
