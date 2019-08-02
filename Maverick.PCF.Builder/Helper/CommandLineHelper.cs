using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maverick.PCF.Builder.Helper
{
    public static class CommandLineHelper
    {
        public static string RunCommand(string vsPromptLocation, string[] commands)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd", $"/K \"{vsPromptLocation}\"");
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.CreateNoWindow = true;

            var process = Process.Start(psi);
            foreach (string cmd in commands)
            {
                process.StandardInput.WriteLine(cmd);
            }
            process.StandardInput.WriteLine("exit");

            var output = process.StandardOutput.ReadToEnd();
            process.Dispose();

            return output;
        }
    }
}
