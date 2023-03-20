using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maverick.PCF.Builder.Helper
{
    public class Commands
    {
        public class Cmd
        {
            private const string MSBUILD_SCRIPT_FILE_LOCATION = "msbuild.ps1";
            
            /// <summary>
            /// cd "path"
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public static string ChangeDirectory(string path)
            {
                return $"cd \"{path}\"";
            }

            /// <summary>
            /// mkdir "folderName"
            /// </summary>
            /// <param name="folderName"></param>
            /// <returns></returns>
            public static string MakeDirectory(string folderName)
            {
                return $"mkdir \"{folderName}\"";
            }

            /// <summary>
            /// rmdir /s/q "folderName"
            /// </summary>
            /// <param name="folderName"></param>
            /// <returns></returns>
            public static string RemoveDirectory(string folderName)
            {
                return $"rmdir /s/q \"{folderName}\"";
            }

            public static string SetExecutionPolicyToUnrestricted()
            {
                return $"powershell \"& \"Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Scope CurrentUser -force\"\"";
            }

            public static string ResetExecutionPolicy()
            {
                return $"powershell \"& \"Set-ExecutionPolicy -ExecutionPolicy Default -Scope CurrentUser -force\"\"";
            }

            public static string FindMsBuild()
            {
                var fullPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "\\" + MSBUILD_SCRIPT_FILE_LOCATION;

                return $"powershell \"& \"\"" + fullPath + "\"\"\"";
            }
        }

        public class Pac
        {
            /// <summary>
            /// pac pcf init --namespace controlNamespace --name controlName --template controlTemplate.ToLower()
            /// </summary>
            /// <param name="controlNamespace"></param>
            /// <param name="controlName"></param>
            /// <param name="controlTemplate"></param>
            /// <returns></returns>
            public static string PcfInit(string controlNamespace, string controlName, string controlTemplate)
            {
                return $"pac pcf init --namespace {controlNamespace} --name {controlName} --template {controlTemplate.ToLower()}";
            }

            /// <summary>
            /// pac pcf init --namespace controlNamespace --name controlName --template controlTemplate.ToLower() --framework react
            /// </summary>
            /// <param name="controlNamespace"></param>
            /// <param name="controlName"></param>
            /// <param name="controlTemplate"></param>
            /// <returns></returns>
            public static string PcfInitVirtual(string controlNamespace, string controlName, string controlTemplate)
            {
                return $"pac pcf init --namespace {controlNamespace} --name {controlName} --template {controlTemplate.ToLower()} --framework react";
            }

            /// <summary>
            /// pac solution init --publisher-name publisherName --publisher-prefix customizationPrefix
            /// </summary>
            /// <param name="publisherName"></param>
            /// <param name="customizationPrefix"></param>
            /// <returns></returns>
            public static string SolutionInit(string publisherName, string customizationPrefix)
            {
                publisherName = Regex.Replace(publisherName, @"\s+", "");
                return $"pac solution init --publisher-name {publisherName} --publisher-prefix {customizationPrefix}";
            }

            /// <summary>
            /// pac solution add-reference --path "path"
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public static string SolutionAddReference(string path)
            {
                return $"pac solution add-reference --path \"{path}\"";
            }

            /// <summary>
            /// pac install latest
            /// </summary>
            /// <returns></returns>
            public static string InstallLatest()
            {
                return $"pac install latest";
            }

            /// <summary>
            /// pac use
            /// </summary>
            /// <returns></returns>
            public static string Use()
            {
                return $"pac use";
            }

            /// <summary>
            /// pac
            /// </summary>
            /// <returns></returns>
            public static string Check()
            {
                return $"pac";
            }

            /// <summary>
            /// pac auth create --url <https://xyz.crm.dynamics.com>
            /// </summary>
            /// <param name="url">CDS Environment Url</param>
            /// <returns></returns>
            public static string CreateProfile(string url)
            {
                return $"pac auth create --url {url}";
            }

            /// <summary>
            /// pac auth list
            /// </summary>
            /// <returns></returns>
            public static string ListProfiles()
            {
                return $"pac auth list";
            }

            /// <summary>
            /// pac auth delete --index <indexoftheprofile>
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public static string DeleteProfile(int index)
            {
                return $"pac auth delete --index {index}";
            }

            /// <summary>
            /// pac auth select --index <indexoftheactiveprofile>
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public static string SwitchCurrentProfile(int index)
            {
                return $"pac auth select --index {index}";
            }

            /// <summary>
            /// pac pcf push --publisher-prefix <yourpublisherprefix>
            /// </summary>
            /// <param name="publisherPrefix"></param>
            /// <returns></returns>
            public static string DeployWithoutSolution(string publisherPrefix)
            {
                return $"pac pcf push --publisher-prefix {publisherPrefix}";
            }

            /// <summary>
            /// pac org who
            /// </summary>
            /// <returns></returns>
            public static string OrgDetails()
            {
                return $"pac org who";
            }

            /// <summary>
            /// pac solution clone
            /// </summary>
            /// <returns></returns>
            public static string SolutionClone(string solutionName)
            {
                return $"pac solution clone --name {solutionName}";
            }
        }

        public class Npm
        {
            /// <summary>
            /// npm version
            /// </summary>
            /// <returns></returns>
            public static string Version()
            {
                return $"npm version";
            }

            /// <summary>
            /// npm install
            /// </summary>
            /// <returns></returns>
            public static string Install(string packageName = "")
            {
                return $"npm install {packageName}".Trim();
            }

            /// <summary>
            /// npm run build
            /// </summary>
            /// <returns></returns>
            public static string RunBuild()
            {
                return $"npm run build";
            }

            /// <summary>
            /// npm start
            /// </summary>
            /// <returns></returns>
            public static string Start()
            {
                return $"npm start";
            }

            /// <summary>
            /// npm start watch
            /// </summary>
            /// <returns></returns>
            public static string StartWatch()
            {
                return $"npm start watch";
            }

            /// <summary>
            /// npm list -d=0 {packageName}
            /// </summary>
            /// <returns></returns>
            public static string CheckAdditionalPackage(string packageName)
            {
                return $"npm list --depth 0 {packageName}";
            }

            /// <summary>
            /// npm list -g -d=0 generator-pcf
            /// </summary>
            /// <returns></returns>
            public static string CheckPcfGenerator()
            {
                return $"npm list -g --depth 0 generator-pcf";
            }

            /// <summary>
            /// npm install -g yo
            /// </summary>
            /// <returns></returns>
            public static string InstallYo()
            {
                return $"npm install -g yo";
            }

            /// <summary>
            /// npm install -g generator-pcf
            /// </summary>
            /// <returns></returns>
            public static string InstallPcfGenerator()
            {
                return $"npm install -g generator-pcf";
            }

            /// <summary>
            /// npm update -g generator-pcf
            /// </summary>
            /// <returns></returns>
            public static string UpdatePcfGenerator()
            {
                return $"npm update -g generator-pcf";
            }
        }

        public class Msbuild
        {
            /// <summary>
            /// msbuild
            /// </summary>
            /// <returns></returns>
            public static string Build(string projectPath = "")
            {
                return $"./msbuild.exe \"{projectPath}\"";
            }

            /// <summary>
            /// msbuild /t:restore
            /// </summary>
            /// <returns></returns>
            public static string Restore(string projectPath = "")
            {
                return $"./msbuild.exe /t:restore \"{projectPath}\"";
            }

            /// <summary>
            /// msbuild /t:rebuild
            /// </summary>
            /// <returns></returns>
            public static string Rebuild(string projectPath = "")
            {
                return $"./msbuild.exe /t:rebuild \"{projectPath}\"";
            }

            /// <summary>
            /// msbuild /p:configuration=Release
            /// </summary>
            /// <returns></returns>
            public static string BuildRelease(string projectPath = "")
            {
                return $"./msbuild.exe /p:configuration=Release \"{projectPath}\"";
            }

            /// <summary>
            /// msbuild /t:rebuild /p:configuration=Release
            /// </summary>
            /// <returns></returns>
            public static string RebuildRelease(string projectPath = "")
            {
                return $"./msbuild.exe /t:rebuild /p:configuration=Release \"{projectPath}\"";
            }
        }

        public class SolutionPackager
        {
            private const string SOLUTION_PACKAGER_LOCATION = "CoreTools\\SolutionPackager.exe";

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static string UnpackSolution(string zipfile, string outputFolder)
            {
                var solutionPackagerExe = FindSolutionPackager();
                return $"{solutionPackagerExe} /action:Extract /zipfile:\"{zipfile}\" /folder:\"{outputFolder}\"";
            }


            #region Private Functions

            private static string FindSolutionPackager()
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "\\" + SOLUTION_PACKAGER_LOCATION;
            }

            #endregion


        }

        public class Yo
        {
            /// <summary>
            /// yo pcf:resx --lc {lcid} --force
            /// </summary>
            /// <param name="lcid"></param>
            /// <returns></returns>
            public static string AddResxFile(string controlName, string lcid)
            {
                return $"yo pcf:resx {controlName} --lc {lcid} --force";
            }
        }
    }
}
