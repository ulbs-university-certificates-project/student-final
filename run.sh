#!/bin/bash

echo "Cleaning docker..."
docker compose -f docker-compose.db.yml down -v
if [ $? -ne 0 ]; then
    echo "Docker clean failed."
    exit 1
fi

echo "Starting Docker Compose..."
docker compose -f docker-compose.db.yml up -d
if [ $? -ne 0 ]; then
    echo "Docker Compose failed."
    exit 1
fi

echo "Waiting for 10 seconds to allow MySQL to start..."
sleep 10

echo "Navigating to student-final directory..."
cd student-final
if [ $? -ne 0 ]; then
    echo "Failed to navigate to student-final directory."
    exit 1
fi

echo "Restoring .NET dependencies..."
dotnet restore
if [ $? -ne 0 ]; then
    echo ".NET restore failed."
    exit 1
fi

echo "Running the .NET application..."
dotnet run
if [ $? -ne 0 ]; then
    echo ".NET run failed."
    exit 1
fi

echo "All tasks completed successfully."