# Image Processing App

## Overview

This project is an image processing application that uses the Google Cloud Vision API to detect faces in uploaded images. The application is built using ASP.NET Core and includes a frontend for uploading images and viewing results.

**Clone the Repository**

git clone https://github.com/your-username/Image-Processing.git

Create a Secrets folder in the root directory of the project. This folder is where you will place your Google Cloud Vision API credentials JSON file.

Create a .env file in the root directory of the project and add the following environment variable:
GOOGLE_APPLICATION_CREDENTIALS=path/to/your/credentials.json

cd ImageProcessingApp
dotnet restore

In the ImageProcessingApp
dotnet run

In the ImageProcessingApp.Tests
dotnet test
