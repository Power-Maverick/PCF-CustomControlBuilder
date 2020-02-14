[![Build Status](https://dev.azure.com/danishnaglekar/GitHub-CI/_apis/build/status/Power-Maverick.PCF-CustomControlBuilder?branchName=master)](https://dev.azure.com/danishnaglekar/GitHub-CI/_build/latest?definitionId=1&branchName=master) [![Nuget](https://img.shields.io/nuget/v/Maverick.PCF.Builder)](https://www.nuget.org/packages/Maverick.PCF.Builder/) [![Gitter](https://badges.gitter.im/PCF-CustomControlBuilder/community.svg)](https://gitter.im/PCF-CustomControlBuilder/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

[![GitHub issues](https://img.shields.io/github/issues/Power-maveRICK/PCF-CustomControlBuilder)](https://github.com/Power-maveRICK/PCF-CustomControlBuilder/issues) [![GitHub forks](https://img.shields.io/github/forks/Power-maveRICK/PCF-CustomControlBuilder)](https://github.com/Power-maveRICK/PCF-CustomControlBuilder/network) [![GitHub stars](https://img.shields.io/github/stars/Power-maveRICK/PCF-CustomControlBuilder)](https://github.com/Power-maveRICK/PCF-CustomControlBuilder/stargazers) [![GitHub license](https://img.shields.io/github/license/Power-maveRICK/PCF-CustomControlBuilder)](https://github.com/Power-maveRICK/PCF-CustomControlBuilder/blob/master/LICENSE) [![Nuget](https://img.shields.io/nuget/dt/Maverick.PCF.Builder)](https://www.nuget.org/packages/Maverick.PCF.Builder/)

[![Twitter Follow](https://img.shields.io/twitter/follow/DanzMaverick?style=social)](https://twitter.com/Danzmaverick)

# PCF-CustomControlBuilder
Xrm Toolbox Plugin for building and deploying custom control using PCF

## Overview
This tool makes it easy to create and build PCF custom control by removing the need to write commands. 
It runs all the necessary commands in the order in which they need to be executed and using proper folder location to execute the commands. 
This tool also makes it easy to deploy the solution into D365 CE.

## Pre-Requisite
1. Install _npm_ from [here](https://nodejs.org/en/) - any version of _npm_ will do
2. Visual Studio
3. PowerApps CLI. Download it from [here](https://aka.ms/PowerAppsCLI)

## Usage
Before you begin with your custom control you need to specify the working folder for your custom control along with path to Visual Studio Developer Command Prompt (VsDevCmd.bat).
Once you have defined the working folder and path to VS Command Prompt you can click on either you want to create a new custom control or edit an existing one. 
Based on that different windows will open and guide you through the process.

For more details read [wiki](https://github.com/Power-maveRICK/PCF-CustomControlBuilder/wiki)

## Screenshots

##### Creating New Custom Control
![Control Home](docs/Control-Home.png)

##### Tool executing commands
![Control Running C L I](docs/Control-RunningCLI.png)
