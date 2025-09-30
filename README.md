[![Build Status](https://dev.azure.com/danishnaglekar/GitHub-CI/_apis/build/status/Power-Maverick.PCF-CustomControlBuilder?branchName=master)](https://dev.azure.com/danishnaglekar/GitHub-CI/_build/latest?definitionId=1&branchName=master) [![Nuget](https://img.shields.io/nuget/v/Maverick.PCF.Builder)](https://www.nuget.org/packages/Maverick.PCF.Builder/) [![Gitter](https://img.shields.io/gitter/room/Power-Maverick/PCF-Builder-VSCode)](https://gitter.im/PCF-Builder/community)

[![GitHub issues](https://img.shields.io/github/issues/Power-Maverick/PCF-CustomControlBuilder)](https://github.com/Power-Maverick/PCF-CustomControlBuilder/issues) [![GitHub forks](https://img.shields.io/github/forks/Power-Maverick/PCF-CustomControlBuilder)](https://github.com/Power-Maverick/PCF-CustomControlBuilder/network) [![GitHub stars](https://img.shields.io/github/stars/Power-Maverick/PCF-CustomControlBuilder)](https://github.com/Power-Maverick/PCF-CustomControlBuilder/stargazers) [![GitHub license](https://img.shields.io/github/license/Power-Maverick/PCF-CustomControlBuilder)](https://github.com/Power-Maverick/PCF-CustomControlBuilder/blob/master/LICENSE) [![Nuget](https://img.shields.io/nuget/dt/Maverick.PCF.Builder)](https://www.nuget.org/packages/Maverick.PCF.Builder/)

[![Sponsor](https://img.shields.io/static/v1?label=Sponsor&message=%E2%9D%A4&logo=GitHub)](https://github.com/sponsors/Power-Maverick)

[![Twitter Follow](https://img.shields.io/twitter/follow/DanzMaverick?style=social)](https://twitter.com/Danzmaverick)


# PCF Custom Control Builder

An XrmToolBox plugin that simplifies Power Apps Component Framework (PCF) custom control development by providing a visual interface for building and deploying PCF controls without writing CLI commands.

## Overview

PCF Custom Control Builder streamlines the entire lifecycle of PCF control development, from initialization to deployment. The tool automates command execution, manages project structure, and provides an intuitive UI for configuring control properties, resources, and deployment options.

### Key Features

- **Visual Control Creation**: Create new PCF controls with a user-friendly interface
- **Template Support**: Choose between Field (single field) and Dataset (grid) templates
- **Framework Integration**: Support for Fluent UI and other modern UI frameworks
- **PCF Features API**: Built-in support for device capabilities:
  - Capture Audio, Video, and Images
  - Barcode scanning
  - Geolocation
  - File picking
  - Utility functions
  - Web API integration
- **Resource Management**: 
  - Preview image configuration
  - CSS file management
  - RESX localization support
- **Solution Management**: Create and manage D365 CE solution packages
- **Authentication Profiles**: Manage multiple environment connections
- **Incremental Versioning**: Automatic component version management
- **Build & Test**: Integrated build and test project capabilities
- **Direct Deployment**: Deploy controls directly to D365 CE/Dataverse environments

## Prerequisites

1. **Node.js & npm**: Install from [nodejs.org](https://nodejs.org/en/) (LTS version recommended)
2. **Visual Studio**: Any recent version with C# development tools
3. **Power Apps CLI**: Download from [aka.ms/PowerAppsCLI](https://aka.ms/PowerAppsCLI)
4. **XrmToolBox**: Download from [xrmtoolbox.com](https://www.xrmtoolbox.com/)

## Installation

1. Open XrmToolBox
2. Click on "Tool Library" (or press F1)
3. Search for "PCF Custom Control Builder" or "Maverick.PCF.Builder"
4. Click Install
5. Restart XrmToolBox if required

Alternatively, install via NuGet Package Manager in XrmToolBox.

## Getting Started

### First Time Setup

1. **Configure Working Directory**: 
   - Select a folder where your PCF control projects will be stored
   - This directory will contain all your control projects

2. **Set VS Developer Command Prompt Path**:
   - Locate `VsDevCmd.bat` on your system (typically in Visual Studio installation folder)
   - Example: `C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat`

3. **Verify Power Apps CLI**:
   - The tool will automatically detect if Power Apps CLI is installed
   - If not detected, use the Help menu to download and install it

### Creating a New Control

1. Click **"New PCF Control"** button
2. Fill in the control details:
   - **Namespace**: Your organization/project namespace
   - **Control Name**: Technical name for the control
   - **Display Name**: User-friendly name
   - **Description**: Brief description of control functionality
   - **Control Type**: Choose `bound` or `input`
   - **Template**: Select `Field` or `Dataset`
   - **Additional Packages**: Optionally add Fluent UI or other libraries
   - **Version**: Set initial version (e.g., 1.0.0)

3. Configure Solution Details:
   - **Solution Name**: Unique solution name
   - **Publisher Information**: Prefix, name, and friendly name
   - **Solution Version**: Initial version number

4. Click **"Run 'pac' command"** to initialize the project
5. Use **"Build project"** to compile your control
6. Use **"Test project"** to launch the test harness
7. Use **"Create Solution Package"** when ready to deploy
8. Use **"Deploy to D365 CE"** to push to your environment

### Editing an Existing Control

1. Click **"Edit existing PCF control"** button
2. Navigate to your control's root folder
3. The tool will load existing manifest and project details
4. Make changes to properties, resources, or features
5. Build and test your changes
6. Deploy updates to your environment

## Features in Detail

### Control Properties Management

Edit control properties including:
- Property names and display names
- Data types and type groups
- Default values and descriptions
- Usage flags (bound/input)
- Required/optional settings

### Resource Management

- **Preview Images**: Add custom preview images for the solution
- **CSS Files**: Include custom stylesheets
- **RESX Files**: Add localization resources for multi-language support

### PCF Features API

Enable device and platform capabilities:
- **CaptureAudio**: Record audio input
- **CaptureVideo**: Record video input
- **CaptureImage**: Capture images from camera
- **GetBarcode**: Scan barcodes and QR codes
- **GetCurrentPosition**: Access device location
- **PickFile**: File picker integration
- **Utility**: Utility functions
- **WebApi**: Web API integration

### Authentication Profiles

Manage multiple environment connections for easy deployment across development, test, and production environments.

## Troubleshooting

### Common Issues

**Power Apps CLI Not Detected**:
- Ensure Power Apps CLI is installed from [aka.ms/PowerAppsCLI](https://aka.ms/PowerAppsCLI)
- Restart XrmToolBox after installation
- Check that `pac` command is available in your system PATH

**Build Failures**:
- Verify npm is installed and in system PATH
- Check that Visual Studio Developer Command Prompt path is correct
- Ensure you have internet connectivity for npm package downloads

**Deployment Issues**:
- Verify authentication profile is configured correctly
- Check that you have appropriate permissions in target environment
- Ensure solution package was created successfully before deployment

**PowerShell Execution Policy**:
- The tool supports custom PowerShell execution policy scripts
- If you encounter script execution errors, check your PowerShell execution policy

## Resources

- **Microsoft PCF Documentation**: [docs.microsoft.com/powerapps/developer/component-framework](https://docs.microsoft.com/en-us/powerapps/developer/component-framework/overview)
- **PCF Limitations**: [docs.microsoft.com/powerapps/developer/component-framework/limitations](https://docs.microsoft.com/en-us/powerapps/developer/component-framework/limitations)
- **PCF Gallery**: [pcf.gallery](https://pcf.gallery/)
- **Sample Controls**: [aka.ms/PCFSampleControls](https://aka.ms/PCFSampleControls)
- **Wiki**: [github.com/Power-Maverick/PCF-CustomControlBuilder/wiki](https://github.com/Power-Maverick/PCF-CustomControlBuilder/wiki)

## Contributing

Contributions are welcome! Here's how you can help:

### For Users
- Report bugs via [GitHub Issues](https://github.com/Power-Maverick/PCF-CustomControlBuilder/issues)
- Request features through issues with clear use cases
- Share feedback and suggestions
- Star the repository if you find it useful

### For Developers
- Fork the repository
- Create a feature branch (`git checkout -b feature/AmazingFeature`)
- Make your changes following the existing code style
- Build and test your changes
- Commit your changes (`git commit -m 'Add some AmazingFeature'`)
- Push to the branch (`git push origin feature/AmazingFeature`)
- Open a Pull Request with a clear description

### Development Setup
1. Clone the repository
2. Open `Maverick.PCF.Builder.sln` in Visual Studio
3. Restore NuGet packages
4. Build the solution
5. The output will be in `Maverick.PCF.Builder/bin/Debug` or `Release`

### Code Guidelines
- Follow existing code structure and naming conventions
- Add XML documentation comments for public methods
- Test your changes thoroughly
- Update documentation as needed

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Author

**Danish Naglekar** - [@DanzMaverick](https://twitter.com/Danzmaverick)

## Support

- **Sponsor**: [github.com/sponsors/Power-Maverick](https://github.com/sponsors/Power-Maverick)
- **Community Chat**: [gitter.im/PCF-Builder/community](https://gitter.im/PCF-Builder/community)
- **Issues**: [github.com/Power-Maverick/PCF-CustomControlBuilder/issues](https://github.com/Power-Maverick/PCF-CustomControlBuilder/issues)

---

Made with ❤️ for the Power Platform Community
