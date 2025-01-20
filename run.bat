@echo off
echo Cleaning docker...
docker compose -f docker-compose.db.yml down -v
if %errorlevel% neq 0 (
    echo Docker clean failed.
    pause
    exit /b %errorlevel%
)

echo Starting Docker Compose...
docker compose -f docker-compose.db.yml up -d
if %errorlevel% neq 0 (
    echo Docker Compose failed.
    pause
    exit /b %errorlevel%
)

echo Waiting for 10 seconds to allow MySQL to start...
timeout /t 10 /nobreak

echo Navigating to student-final directory...
cd student-final
if %errorlevel% neq 0 (
    echo Failed to navigate to student-final directory.
    pause
    exit /b %errorlevel%
)

echo Restoring .NET dependencies...
dotnet restore
if %errorlevel% neq 0 (
    echo .NET restore failed.
    pause
    exit /b %errorlevel%
)

echo Running the .NET application...
dotnet run
if %errorlevel% neq 0 (
    echo .NET run failed.
    pause
    exit /b %errorlevel%
)

echo All tasks completed successfully.
pause