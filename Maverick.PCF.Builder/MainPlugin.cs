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
        ExportMetadata("SmallImageBase64", "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"),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "iVBORw0KGgoAAAANSUhEUgAAAFAAAABQCAYAAACOEfKtAAAACXBIWXMAAA9hAAAPYQGoP6dpAAAF+mlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDIgNzkuMTYwOTI0LCAyMDE3LzA3LzEzLTAxOjA2OjM5ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdEV2dD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlRXZlbnQjIiB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ0MgMjAxOCAoV2luZG93cykiIHhtcDpDcmVhdGVEYXRlPSIyMDE5LTA4LTAxVDE2OjI2OjA1LTA0OjAwIiB4bXA6TWV0YWRhdGFEYXRlPSIyMDE5LTA4LTAxVDE2OjI2OjA1LTA0OjAwIiB4bXA6TW9kaWZ5RGF0ZT0iMjAxOS0wOC0wMVQxNjoyNjowNS0wNDowMCIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpkYWE3NDZiNy0yYjMwLTVhNDItOGM5Zi0wNjRiNjU1MDY3ZmQiIHhtcE1NOkRvY3VtZW50SUQ9ImFkb2JlOmRvY2lkOnBob3Rvc2hvcDozMjQ4NTI5Zi0wMzZhLThmNGYtOWUxMy01NzZhMjY1Y2ZkZTUiIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDowMGZmODRhNi05MjBmLWRmNDEtOGMzMy1iOTZiNTBlNGRiOWQiIGRjOmZvcm1hdD0iaW1hZ2UvcG5nIiBwaG90b3Nob3A6Q29sb3JNb2RlPSIzIiBwaG90b3Nob3A6SUNDUHJvZmlsZT0ic1JHQiBJRUM2MTk2Ni0yLjEiPiA8eG1wTU06SGlzdG9yeT4gPHJkZjpTZXE+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjcmVhdGVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOjAwZmY4NGE2LTkyMGYtZGY0MS04YzMzLWI5NmI1MGU0ZGI5ZCIgc3RFdnQ6d2hlbj0iMjAxOS0wOC0wMVQxNjoyNjowNS0wNDowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTggKFdpbmRvd3MpIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJzYXZlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDpkYWE3NDZiNy0yYjMwLTVhNDItOGM5Zi0wNjRiNjU1MDY3ZmQiIHN0RXZ0OndoZW49IjIwMTktMDgtMDFUMTY6MjY6MDUtMDQ6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE4IChXaW5kb3dzKSIgc3RFdnQ6Y2hhbmdlZD0iLyIvPiA8L3JkZjpTZXE+IDwveG1wTU06SGlzdG9yeT4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz6ehXfaAAARNElEQVR42u2cCVRUR9aA72sQFLWDgBLoRoMLRpNz5oyo4+4/Ou6CIPvWok5QREkUMSauiIKgQkRUdKIoCKiogCAocSYBN0REdM5EI+DGJqsssi/vr1t0d2joZkub6Elfzz3C6+qi6nu3qu69Ve8xLMuCQnovjAKgAqACoAKgAqBCFAAVABUAFQDbS1pMWjCHw5nJAKPcWlp6OY4yZwDDCMt8INLc2Fwu80NW9B9bxTayQYZLDQ91G2CCd8LghpaGooy4DCjILIDGukbgKHH+dJaFXNgWFgZoDIBhfx0G0wTTYKLZRKZTgA8uP/hfxOaIsTmPc4CrwQVVNVVoqG2ApoamP9/QZBjo07cPKKsoQ1VpFeUw2WoyjJ0+9pNZLrNedgCYHpte8Z3pd1zV/qowQHMAlOWUgbquOnw86mPoP6g/vRt/JngtLS1Q8boC8n7Og+amZhikOwjyMvPAYKIBzFw5c+Qsp1nZYoD3Lt1zO+Z4bD8Sxy+T+QHmrpsLk6wnAW8Mr5GUyyR6VqjPcAr5jW1UIjqcqDVRc+G1C3Ksn/l1Juu9uI5yVXU84Jh+I+TG2PvR92HI8CGQm5kLU8ymgOsFV0YM0G+JH/sw/iEMHDyQkl93dh2M+b8x+HkL0VdEw4meIpotvCYPwYl1BNElwt9j5Fy/XCVyayQbuzdWDNE13BWm2ExhmPsx968HWATM1uBrQFleGayPXg9/mf8X0fdwpbpMFFehB3KwDGmWqCe0mFfvoH65SpAgiE05lwIq/VSA/zkftiZtZZgwt7CKBL8EroqaCowzHgcu4S6i8rhy3CfqTzSWaM07apeSyLN43+fGu5F3zUPWhUQqqypDzZsacD7jHMfs/vvuivzH+dzqN9XgEuYCE8wmiMqXEg0RWt9zhcvcKkftj7JpUWnQ1NgE9t/ZVzLuY9wrmuqbuHVVdeCe4A76hvqiue8RUR+il4g2KNC1ysWdF9kYzxhgOAws2bKkknEb5VbR3NzMbaprArc4N/jkr59guTrh3IcA0xXYfpXo3dFs1K4o6mgbbTaqZDaO3lhBHGUuRhxusW4iCyxDaxUO30IFtl/lksclNmZPDAVo/I2xTIC4Iu4lGiy0RoX0ACA6oE+IehI99776Ze8zQNEC4ilcQBSiAPh7Abzs1qw/Xr+elHsonANj5RFT/mkAukS4ZIyZOeYpKXePaALRx4o5sAcAlx9dHma4xNCPlMsh+kYYzimkhwDtFZgUANsLR5jxaVYA7J0oC+GxHzTA1AupgpbmluJJVpMS5JnOSr2YuralruXBJLtJt2RYHysvb+IPAxjuHs4mByeDkrISLFi/ABZ/vZiBXxOqIFy4egSR3Aw4seoEm3YpjSY5SZ0wz3Ve250zRqgtH/QceHz5cTbxVCLo8HSgpakFiguLwexrM7Daa2UAvUzpYwf2L9rPpiakgu5QXWioaYDSklKw3WULJttMROBw6DZ+0IsIwvvx1I/AM+ABh8Oh3Wqqb4L8Z/lgsskErH2sHwmLdntTCXcI9y/ez2YkZOCmF+0MboY11jbC6xevwWKnBSzdsZQLrVn05g8W4DHHY2zS6STQNdCl8ERbqNhZzOgWZBeAsbsx2PjaYMI2C9NtwvDxMcjYQsCdMv4Ift3Daw/F8ESzGyY5sR8FzwvAYpsFmO0yYz5YNwY3YJJCk8SW1/4EBHYWLbHgWQEYuRmB7X5bHGp5RJOEUVCq8HdxNpyAV/Ke49306IdHwB/Dl4DXtl7sy+vnr8FsqxmYe5ozHxzAI/ZH2OSwZOAb8KXCk4DYQIZzdj4YbzAG2wO2OP9VEv2F6G1hSIk/5xHLK+cN59U9THwoE157iGiJljssYenOpcwHA/Cow1E2+UyyTMvr0FnhRn5edh4sXr8Y7P3on28UgsSV+SlpXyax6C23zt0CvTF6ncKTgFjfCHnP8mDlwZXtV+f3E+AR2yPszYibwBvNo2C6e3Su7Zy46KtFYO8vbgIuAA0ZVzJq9i3ep6kzSoeC6a5g2ariKsAtWzNPs3EzBDMevLcAA60DWbQQ/mh+j+C1t0QczgtdF4LDQQfxZwQg+C72pTemp3Xidi1HmYOr/eSpdlNT3kuAgVaB7M3zN0FvtJ5UeKLDOuir4c9oEdJOr1CITWQ4Z+XBgrULYNmhZfQ6ts3fxB8yrmWA3lg96kB364Y0N8PLzJdg52EHpttN388hjPBun78tc9ji2UIEV1FYARp6GhRaWW4ZPe2kpKLU4dRXW4gLXRaCIFBAr6Ml+c73hWdpz0D3U91OIUrUsZbUcUggAe9+zP0o0o5sQxPDjX8owEOWh9g7kXc6hVddXg015TVg5mEGk60nU2D/Dvo3xPrEgiZfE1T6q8iEmJ+VD/Nd5sOywFZLxHp85vnAs3sE4hjpECW+u4Z89/AyMbzwb8IHlWSVlKXHptOQcpbTLLDzs2P+EIAHzQ6yKZdS6JwnbThSeMRqKosq4YuTX8B0wXSJz+P3x0OYexhoDdUCPJMoFSIZgvmZ+TDPeR44HnFshVhRA77zfCH7XnYHS+zM8hDe8zvPyx4lPQLeCB4tV/CyAGYvmw2rTq1ifleAbeHJsry3ZW+h/HU5rAlZA1MdpkqtJ/5APIRuDIUhekOg74C+dJ6UNRTnrZoHy4OW0+t1b+tg7z/2QlZqFo1GEKIUeLak6EVcxY8bHVcrqiqqfpz0mPqQLSwpD63zcu7TXJjpMBOcQ5yZ3wUgwrt76W7nw7asGsoLy7FRMNV+aqcN6gnEuavmwoqgFWKIPnN94Oc7P1OLwvYX5hWCkasRruDig1GR3pFvnyQ8Kfrl5i+tsLF+VnJxy3uaRyGuDlnNvFOA/qb+7L3oe8D7lEfvoFR4OGyLK8H5tDNMtp3crWFx1f8qhGwIgcFDB3c5nOc4zYEVx1oh1lbWQtCyIHh09RHgkTPiLIP5LnrglR7NK8wu9Cfx+NmnN58CbyxP5pyJfw8hznCY0SXEXgHEAF7/M/261JhU4H/KF6ZEOjakvrYeqoqqwOmkEz0e3BNJDEhsnRP1tIDThyO1fgSA55TbQkTBlVntIzV6nlskZPEq9zPyU6eWJwOeBEQCJPeXXJhhRyCGrmbwmlwAYsHv//k9m3gyEfQ/1Rdfk5afK31VCrb7bGHO2jm98rEubLtAh7TmUE2ZHUUQLzJfgOW3lmC5x1JqOXTIAywD4G70XRg2dli3/Ua0xOdPn4PpBlOwOyB9de4xwPvR94+QKMNZnadOj/zLikHrKuugv0Z/8Mrwop5/bwRX7K2GW+lUgAfeZXUUhy4mIbweeDUO4g/qUPBJ0hPY8/c9oD1KG2RZksy639ZCY00jrDy20n6C+YSwLgG6GbhVkDvGxdQScRVCCMBlbb9wL+qeR5BD0HZ89AGfG5EVotW/rafz154He1ojjV4IPlqxbeI2Cq+Pah+Z8e3b0rcUssddj7uaepqjyWWucO+DSlZKFnhO9wTNYZqg1Eep27shNPQjfiuWdwxwNJ5gOSG2XRG1KM+o6mjPaMnzgQiwvroe55V/EYBO7Ss+98059uLeizB01FDacFkQS16VwCK3RZiD6xXAk6tPwq0ztwAPvHeWpsp7ngcr/FfA/K/m42MSC4jOJIqBch/h/NxwYvUJlcTjiTB09NBuxeaihEZOdg4I9grA6Guj9nsr6kQnRe2KiscDllieAtw+cXtFeUE5t6qsCg+Y5xoaG+pJ+wPBzsHs1aCrwB/Jp158hwYxrXNP8fNiTI7SjaOeyNnNZyHONw4w4yLtmby2iVLLnZZguoPGtmpE8XmMpURNiI4UdpY+1xJgHrDrzsU7XSY42uYlhdsM7VuAv2sRNTq15tSJn078RK3Uwsuikjm+/HjFrfBbXKx8wVcL8MscWVuACPFa0DXgjeRJhYj5v7rqOmqJ9gfsYcGG7kE8u+ksRO+LBt3hutQd6eDGkA7io1aFLwrBYoeFRII0LSqNTT6VTIfqbKfZdZ/P/TyXXD4PwudaiN/a3JnT3zYzvsR9CVj7WsuaNDm5j3OHB1oEZmKQgO7alxe+rGRSzqVUHLI6xMWVDxOQxNsXTDCZECqrs6fXnmavHr4KuiN1pULEBuF0UPyqGOx87WCR+6JO4UW4R8Dl/ZdBZ7gOXaSk1ddus0jcwf8c+w/7/ervQYlRotEFtmdtxFqYaD4RHzUQP9ciK2aXgLdpiTTLk5Ar+66wOFK4Q7jQX70/eKR4VDJk1QnaPX33qqIXRdDS2AL8z/gwfeX0kbO/mJ0tq6KQdSFsfGC8TEsUQSx6VQR2e+3ofq00CdsYBnEHyLAdIYTXieVZeVqByVYTcQevH7nOBrsEgyZPE/p91I9ew8WlqoRORfA3y79JwDhsc5i9dfaWGGLb3UHTzaZg5W3VKTx8lvCI3RHuQM2B9GZaelhiiqwMH/Va+iD2QbiPsY8qDqE3+W9A73M9MNluUjDOaJyuPCDaeNngki/x+Zn1Z+DKd1e6Bc96tzU+UiDu4A+Hf2CD1waDFl8L+nH7if08GoMTiJg6c4lwwewP0z719tP5n+j+NM55JcUlYPGNBVh6WcqEdyP4xliyPvwPFw7VAapQW1FLF7ltSdv8udpcUwQ4jJR7GeURxYbtDAM9fT2a8cBGjZ42GvTH6dOGoY8XFxDXNyAzoF5UeeiXoeyVgCutEPsoSYdQ00BvCoZZU+ymANvMwvWj1yEpOAm0h2u3fo+V/r3Clx3hJQYmssHrgmEwf7AEvPaJDIyInMOcO0AM/SqUvR1+m940XOgWui2U+JyEqvtfpL1wQ1essrASMJLJ+W8OaPA06GOv2Mc1YWvQX44jxdciQBXR1iFxENmLOy7STqnrqNP8GzqtNc01MGTIEDDfbi7xrKyoQfEH40F3hK5MGHga4U3eG+jL7UuXJ7TMQbxBMid1ETybPcRyvzXuOGz5ZNgO7Nch8dA+oYHD2fmMM0yymiQBKSUyxYr0OFfaWZobp2+w3o7eoKWsRRfFAVoDaH0lOSV0tNgcsIkdv2Q8DqdxGOV2eGI9/XJ6FpmcRyB57CidW5rfgraWNk7iHQDSuWxDGBvrH0uzItIgtg2xUKhzK8OdQHhFL8mw97ZBP0vc8ZuhN9lAQSBo62lD34F9uwzPRJaIw3lDzIZmMh1165UEyaeTWS9HLxjMGUzbg/8+0vkIxpuMh1HjR42bLJgs2pjSJuok850JmPYuflFsgtEBpo4Gag2ENw/f9HeKdZJ6YiDMjUD0i6WWKG1O60rwbosXntbVWwxvp9FOtdrs2mp8mhSHkizLkwYx/3E+fPaPz2Bz4uZuxXSk35Fp0WnmaupqoNpPlSYiSGS0jKzsIe2KqhK1l+tbO8LdwtnLfpd7DFFi1fYhq/amxRKdDXMN46YnpFeg74UuRE/qLcwqBIOpBrDlxy3yPuaB/vJSub/2JGJTBHt5n9CvU+0aYlt49r72EpbXVhL8Ethgt2DgD+f3qN7Xr16Dy0kXmLF8hrwBYn1G7+S9MSSyYMWRhRTnWJrT7XDAASOXTjt5fst5NsorqsubI5oOaL3+Dhgzy/2QkVCM39mLd85tPsdG+UTJDM9o2Efm1uKcYhD4C7rdSUxsRO3tol4STnb3prx3Q7g7nRXBK8opAoGfAP0xRp71FuYUgmCfABZuXMjAuxPik4HdO3/10/lvybDzjoKPh39Mc3zoFvTG8mQNZ2n1vmPLg265MfKUyC2RbKRXJD2Kizm30oJSWB6wHF+t8ps6eWHbBfb87vOgM1SH7tyV5peC40FHuZ3E6kLQkf76d3v52LWD1ypxvwOPc+DO2RTbKVx51Ru3L44uVnj6YZrDNC14968owOgNo5EvFG9v+62+jAKgAqACoAKgAqBCFAAVABUAFQAV0nP5f+5rjsTLQTBzAAAAAElFTkSuQmCC"),
        ExportMetadata("BackgroundColor", "White"),
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