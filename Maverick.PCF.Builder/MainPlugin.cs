using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Maverick.PCF.Builder
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "PCF Custom Control Builder"),
        ExportMetadata("Description", "Easily create, build and deployment solution for your custom control using PCF."),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAMCAgICAgMCAgIDAwMDBAYEBAQEBAgGBgUGCQgKCgkICQkKDA8MCgsOCwkJDRENDg8QEBEQCgwSExIQEw8QEBD/2wBDAQMDAwQDBAgEBAgQCwkLEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBD/wAARCAAgACADASIAAhEBAxEB/8QAGgAAAgIDAAAAAAAAAAAAAAAABwkECAMFBv/EACkQAAICAQQCAAYCAwAAAAAAAAIDAQQFBgcREgATCBQhIjEzCTIVUZL/xAAVAQEBAAAAAAAAAAAAAAAAAAAAAf/EABURAQEAAAAAAAAAAAAAAAAAAAAR/9oADAMBAAIRAxEAPwBlrnJrpOxYaClKGTMzKBERiOZmZn8REeR7GWxVT2RbydVHq/Z7XCHT+n55n6fsX/2P+48w3Mrbq2jrLwN6wMfLety2IgGy1hAcDBMguUiMNZyMcgUev2HBBAf17lNQ5RVrH19Dbh1RyNlGTm3QPRxwqTx61MqRF15wUQJzDCISOSkhBsokRkCnl9eaOweJtZ3I6ipRQoCLLj0n7xqqk4GXN9fb1pDt2Y0uAWEEZkICRRtqGSx+VQVrGX69xIOdWJiGiwRcphKauZGZiCBgGBD+RISieJiY8AQbCZHWeCTqi5mLLsrYey4VLVOEwJPlgGcysreIUPULMj6rHDHCyrYsJIIJkyFBfgs+IPdXYz4nnbUa/wAjkrdHWWqW4bU2NssG4xOda4kRbBns4hvzMiLjEig1dpmGECpGxYbVa0/WtDIvZDBNxvb3rIOWSUxHE8hxMeqCTzx2lZTHbn7vBVqzROex4Hk8fhNR517SssaqvhtNS8zGwIARHZNIzLQZLRmSn7VM79GSKiNXkDJYDBZiSnL4Whek6j6B/M1wb2rP6+5M9onlbPWvuH9S6DzE8R4qAJqvdjc/Qm3MYbAbWZLAZ10Oq4m/qg8KFa1eZDCr11VcZdZJsNnQZmYUpSodZafVJAyi/wDH/wDDTrzdzevEb67i4/LN01i2HqgMzdYTP8xlgtMBY+6WQw2DZU1zCnv+iBZHDwkmi0doNpsWF5eM2u0jUHJ1W0b0IwlZcWqzI4YlvUI7rKPoQlzEx+YnzrYiIiIiIiI+kRHirX//2Q=="),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "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"),
        ExportMetadata("BackgroundColor", "Lavender"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
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
    }
}