﻿using System;
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
            /// pac solution init --publisher-name publisherName --publisher-prefix customizationPrefix
            /// </summary>
            /// <param name="publisherName"></param>
            /// <param name="customizationPrefix"></param>
            /// <returns></returns>
            public static string SolutionInit(string publisherName, string customizationPrefix)
            {
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
            public static string Install()
            {
                return $"npm install";
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
        }

        public class Msbuild
        {
            /// <summary>
            /// msbuild
            /// </summary>
            /// <returns></returns>
            public static string Build()
            {
                return $"msbuild";
            }

            /// <summary>
            /// msbuild /t:restore
            /// </summary>
            /// <returns></returns>
            public static string Restore()
            {
                return $"msbuild /t:restore";
            }

            /// <summary>
            /// msbuild /t:rebuild
            /// </summary>
            /// <returns></returns>
            public static string Rebuild()
            {
                return $"msbuild /t:rebuild";
            }

            /// <summary>
            /// msbuild /p:configuration=Release
            /// </summary>
            /// <returns></returns>
            public static string BuildRelease()
            {
                return "msbuild /p:configuration=Release";
            }

            /// <summary>
            /// msbuild /t:rebuild /p:configuration=Release
            /// </summary>
            /// <returns></returns>
            public static string RebuildRelease()
            {
                return "msbuild /t:rebuild /p:configuration=Release";
            }
        }

    }
}
