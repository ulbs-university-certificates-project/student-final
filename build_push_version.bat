@echo off

echo Building Docker image...
docker build -f Dockerfile -t student-final-api:latest .

echo Getting the new image ID...
setlocal enabledelayedexpansion
for /f "tokens=*" %%i in ('docker images -q student-final-api:latest') do set IMAGE_ID=%%i
echo New image ID: !IMAGE_ID!
endlocal

echo Tagging the image...
docker tag student-final-api:latest florescucristian/student-final-api:latest

echo Pushing the tagged image...
docker push florescucristian/student-final-api:latest

echo Script completed.

pause