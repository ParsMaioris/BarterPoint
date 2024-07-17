#!/bin/bash

cd ..

dotnet clean
dotnet build

dotnet publish -c Release -o ./publish