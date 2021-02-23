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
        public static string RunCommand(string[] commands)
        {
            var start = DateTime.Now;
            var commandToLog = string.Empty;

            ProcessStartInfo psi = new ProcessStartInfo("cmd", "");
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.CreateNoWindow = true;

            var process = Process.Start(psi);
            foreach (string cmd in commands)
            {
                process.StandardInput.WriteLine(cmd);
                commandToLog += cmd + ",";
            }
            process.StandardInput.WriteLine("exit");

            var output = process.StandardOutput.ReadToEnd();
            process.Dispose();

            var end = DateTime.Now;
            var duration = end - start;
            var properties = new Dictionary<string, string>
            {
              { "Commands", commandToLog }
            };
            var metrics = new Dictionary<string, double>
            {
              { "ProcessingTime", duration.TotalMilliseconds }
            };
            Telemetry.TrackEvent("CommandLineHelper.RunCommand", properties, metrics);

            return output;
        }
    }
}
