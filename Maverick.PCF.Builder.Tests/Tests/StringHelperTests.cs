using Maverick.PCF.Builder.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#pragma warning disable CS0436 // Type conflicts with imported type
using StringHelper = Maverick.PCF.Builder.Common.StringHelper;
#pragma warning restore CS0436 // Type conflicts with imported type

namespace Maverick.PCF.Builder.Tests
{
    [TestClass]
    public class StringHelperTests
    {
        #region Test Data: Parse PAC Version Output

        string output_English = @"Microsoft Windows [Version 10.0.19041.746]
                                (c) 2020 Microsoft Corporation. All rights reserved.

                                C:\Users\Danish>pac
                                Microsoft PowerApps CLI
                                Version: 1.4.4+g0574e87

                                A newer version of Microsoft.PowerApps.CLI has been found. Please run 'pac install latest' to install the latest version.

                                Usage: pac [auth] [help] [org] [package] [pcf] [plugin] [solution] [telemetry]

                                  auth                        Manage how you authenticate to various services
                                  help                        Show help for the Microsoft PowerApps CLI
                                  org                         Work with your CDS Organization
                                  package                     Commands for working with CDS package projects
                                  pcf                         Commands for working with PowerApps component framework projects
                                  plugin                      Commands for working with CDS plugin class library
                                  solution                    Commands for working with CDS solution projects
                                  telemetry                   Manage telemetry settings

                                Launcher usage: pac [install] [use]

                                  install <version# | latest> Install 'latest' or a specified version of the Microsoft PowerApps CLI
                                  use <version# | latest>     Use 'latest' or a specified version of the Microsoft PowerApps CLI


                                C:\Users\Danish>exit";

        string output_French = @"Microsoft Windows [Version 10.0.19041.746]
                                (c) 2020 Microsoft Corporation. All rights reserved.

                                C:\Users\Danish> pac
                                Microsoft PowerApps CLI
                                Version : 1.4.4+g0574e87

                                Utilisation : pac [auth] [help] [org] [package] [pcf] [plugin] [solution] [telemetry]

                                 auth Gérer la manière dont vous vous authentifiez auprès de divers services
                                help Afficher l'aide de Microsoft PowerApps CLI
                                org Collaborer avec votre organisation CDS
                                package Commandes pour utiliser les projets de package CDS
                                pcf Commandes pour l'utilisation des projets PowerApps component framework
                                plugin Commandes pour utiliser la bibliothèque de classes de plug-in CDS
                                solution Commandes pour utiliser les projets de solution CDS
                                telemetry Gérer les paramètres de télémétrie

                                Launcher usage: pac [install] [use]

                                 install <version# | latest> Install 'latest' or a specified version of the Microsoft PowerApps CLI
                                use <version# | latest> Use 'latest' or a specified version of the Microsoft PowerApps CLI


                                C:\Users\Danish>exit";

        string output_Dutch = @"Microsoft Windows [Version 10.0.19041.746]
                                (c) 2020 Microsoft Corporation. All rights reserved.

                                C:\Users\Danish> pac
                                Microsoft PowerApps CLI
                                Versie: 1.4.4+g0574e87

                                Gebruik: pac [auth] [help] [org] [package] [pcf] [plugin] [solution] [telemetry]

                                    auth                        Beheren hoe u bij verschillende services verifieert
                                    help                        Help voor de Microsoft PowerApps CLI weergeven
                                    org                         Werken met uw CDS-organisatie
                                    package                     Opdrachten voor het werken met CDS-pakketprojecten
                                    pcf                         Opdrachten voor werken met PowerApps component framework-projecten
                                    plugin                      Opdrachten voor werken met klassebibliotheek voor CDS-invoegtoepassingen
                                    solution                    Opdrachten voor het werken met CDS-oplossingsprojecten
                                    telemetry                   Telemetrie-instellingen beheren

                                Launcher usage: pac [install] [use]

                                    install <version# | latest> Install 'latest' or a specified version of the Microsoft PowerApps CLI
                                    use <version# | latest>     Use 'latest' or a specified version of the Microsoft PowerApps CLI


                                C:\Users\Danish>exit";

        string output_no_cli_English = @"Microsoft Windows [Version 10.0.17763.1697]
                                        (c) 2018 Microsoft Corporation. All rights reserved.

                                        C:\Users\Danish>pac
                                        'pac' is not recognized as an internal or external command,
                                        operable program or batch file.

                                        C:\Users\Danish>exit";

        #endregion

        #region Test Data: Parse Org Details

        string output_OrgDetailsExists = @"Organization Information
                                      Org ID:                     12345678-795b-42f2-96cf-123456781234
                                      Unique Name:                org1234db
                                      Friendly Name:              Sandbox Environment
                                      Org URL:                    https://test.crm.dynamics.com/
                                      User ID:                    admin@test.onmicrosoft.com (12345678-123b-42f2-1234-123456781234)";

        string output_OrgDetailsDoNotExists = @"Error: No profiles were found on this computer. Please run 'pac auth create' to create one.";

        string output_OrgDetailsError = @"Error: Unable to login to Dynamics CRM.";

        #endregion

        [TestMethod]
        public void ParsePacVersionOutput_English_GetVersion_Test()
        {
            StringHelper test = new StringHelper();
            var details = test.ParsePacVersionOutput(output_English);

            Assert.AreEqual("1.4.4", details.CurrentVersion);
        }

        [TestMethod]
        public void ParsePacVersionOutput_English_NewVersion_Test()
        {
            StringHelper test = new StringHelper();
            var details = test.ParsePacVersionOutput(output_English);

            Assert.IsTrue(details.ContainsLatestVersionNotification);
        }

        [TestMethod]
        public void ParsePacVersionOutput_English_NoCLI_Test()
        {
            StringHelper test = new StringHelper();
            var details = test.ParsePacVersionOutput(output_no_cli_English);

            Assert.IsTrue(details.CLINotFound);
        }

        [TestMethod]
        public void ParsePacVersionOutput_French_UnableToDetectVersion_Test()
        {
            StringHelper test = new StringHelper();
            var details = test.ParsePacVersionOutput(output_French);

            Assert.IsTrue(details.UnableToDetectCLIVersion);
        }

        [TestMethod]
        public void ParsePacVersionOutput_Dutch_UnableToDetectVersion_Test()
        {
            StringHelper test = new StringHelper();
            var details = test.ParsePacVersionOutput(output_Dutch);

            Assert.IsTrue(details.UnableToDetectCLIVersion);
        }

        [TestMethod]
        public void ExtractOrgDetails_OrgDetailsExists_Test()
        {
            StringHelper test = new StringHelper();
            var details = test.ExtractOrgDetails(output_OrgDetailsExists);

            Assert.AreEqual(details.ParseStatus, OrganizationInfo.Status.Found);
        }

        [TestMethod]
        public void ExtractOrgDetails_OrgDetailsDoNotExists_Test()
        {
            StringHelper test = new StringHelper();
            var details = test.ExtractOrgDetails(output_OrgDetailsDoNotExists);

            Assert.AreEqual(details.ParseStatus, OrganizationInfo.Status.NotFound);
        }

        [TestMethod]
        public void ExtractOrgDetails_Error_Test()
        {
            StringHelper test = new StringHelper();
            var details = test.ExtractOrgDetails(output_OrgDetailsError);

            Assert.AreEqual(details.ParseStatus, OrganizationInfo.Status.Error);
        }

        [TestMethod]
        public void ParseOrgDetails_OrgDetailsExists_Test()
        {
            StringHelper test = new StringHelper();
            var details = test.ParseOrgDetails(output_OrgDetailsExists);

        }

    }
}
