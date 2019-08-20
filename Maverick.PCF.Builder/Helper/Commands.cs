using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maverick.PCF.Builder.Helper
{
    public class Commands
    {
        public class Cmd
        {
            public static string ChangeDirectory(string path)
            {
                return $"cd {path}";
            }

            public static string MakeDirectory(string folderName)
            {
                return $"mkdir {folderName}";
            }

            public static string RemoveDirectory(string folderName)
            {
                return $"rmdir /s {folderName}";
            }
        }

        public class Pac
        {
            public static string PcfInit(string controlNamespace, string controlName, string controlTemplate)
            {
                return $"pac pcf init --namespace {controlNamespace} --name {controlName} --template {controlTemplate.ToLower()}";
            }

            public static string SolutionInit(string publisherName, string customizationPrefix)
            {
                return $"pac solution init --publisher-name {publisherName} --publisher-prefix {customizationPrefix}";
            }

            public static string SolutionAddReference(string path)
            {
                return $"pac solution add-reference --path {path}";
            }

            public static string InstallLatest()
            {
                return $"pac install latest";
            }
        }

        public class Npm
        {
            public static string Install()
            {
                return $"npm install";
            }

            public static string RunBuild()
            {
                return $"npm run build";
            }

            public static string Start()
            {
                return $"npm start";
            }
        }

        public class Msbuild
        {
            public static string Build()
            {
                return $"msbuild";
            }

            public static string Restore()
            {
                return $"msbuild /t:restore";
            }

            public static string Rebuild()
            {
                return $"msbuild /t:rebuild";
            }

            
        }

    }
}
