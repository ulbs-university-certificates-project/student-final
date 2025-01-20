#!/bin/bash

echo "Building Docker image..."
docker build -f Dockerfile -t student-final-api:latest .

echo "Getting the new image ID..."
IMAGE_ID=$(docker images -q student-final-api:latest)
echo "New image ID: $IMAGE_ID"

echo "Tagging the image..."
docker tag student-final-api:latest florescucristian/student-final-api:latest

echo "Pushing the tagged image..."
docker push florescucristian/student-final-api:latest

echo "Script completed."