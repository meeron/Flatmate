#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Development

export FLATMATE_Development_MongoDb=mongodb://localhost:27017/Flatmate
export FLATMATE_Development_AuthKey=NGJmMTlmMGUtOWUxMS00ZTlkLWJhMWItNmQ1ZWU0NzYzMjMz

cd src/Flatmate.Web
dotnet run