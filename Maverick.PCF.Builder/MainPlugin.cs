using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using Maverick.PCF.Builder.Helper;

namespace Maverick.PCF.Builder
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "PCF Builder"),
        ExportMetadata("Description", "Easily create, build and deployment solution for your custom control using PCF."),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "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"),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "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"),
        ExportMetadata("BackgroundColor", "#D9D9D9"),
        ExportMetadata("PrimaryFontColor", "Blue"),
        ExportMetadata("SecondaryFontColor", "Blue")]
    public class MainPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new MainPluginControl();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public MainPlugin()
        {
            // If you have external assemblies that you need to load, uncomment the following to 
            // hook into the event that will fire when an Assembly fails to resolve
            // AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// Event fired by CLR when an assembly reference fails to load
        /// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
        /// For example, a folder named Sample.XrmToolBox.MyPlugin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }

        /// <summary>
        /// Event fired when unhandled exception is thrown by the tool
        /// Exception details are sent ovet to AppInsights
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Telemetry.TrackException(ex);
        }
    }
}