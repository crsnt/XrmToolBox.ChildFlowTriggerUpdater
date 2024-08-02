using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace ChildFlowTriggerUpdater
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Child Flow Trigger Updater"),
        ExportMetadata("Description", "This tool will update the child flow triggers to include the parent URL and will update all parent flows to include their flow run URLs."),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAAA7EAAAOxAfWD7UkAAAc/SURBVFhHlZdrbFxHFcf/c+/d9/qV+pW1kziJnSYmoU7SkCjEaSJU9UGpUh4RiFRV1QqEEKJVECBRUSFEeXypkIAkpYpEKaKlUhGgSnyo0oYWKkixW5xCQ2nsxkld2/V6vV7f3fvYe/nP7N2XH0n8k9Yz98zrnDNnzowFroMrp74U7/ryE2bwWcfEU8cTtilOAf5nIEQ0EFfwXRvFhVl4xeLruiaObfrWby4ETYplFfB++rXIeCx8v++JL3KKXSjacc/Ou17BfB0aflUwndPbv/ecLfuOnTr+uIB4SA1cAb/owJ2b5GpiuPfbv90ViBVaUFa4dPKbO8aj4TfhixNC4IAQIi6MCPR4s2E0te0TQj8RjRnDF3507zbZn4vfogZeBaGH+IdL+f7O849+LhyIFXUKjJ043u+J4ln2vjEQ1SEnMhrbOJfer/vOX9557FgvfBhB81WhIap0kxFqU6WigP/SowZ7PUOLWgLR8tASPbFG1lqFVnzap0TJa6CCaNiwFanBI4g0twXS5akoMH5h4SiV3BF8Voh3rEfnvjuQ6NoszVAyYYShhWPSpXt911LeErqBSEs7mrfsROrgEbTcuBtGLIH2mz+Bps07oEcSauxiKtqPnTz+B7rpblnXwhHEWruQ7O6jBa2qXeLmc8hdeReFqcuw0hNwczNUrA9tH7sdOhWatxw0ROu2GDnLRiIcgsjOM5bz0D6cbtLuvDcbNFc9QNffJMu1+z+J7kOfBTYOwAwnVVuZGUdA79qKzm0fR2rXbUrmmllaF0faLGD3D55QsloeeOqPOHNhTHoLOoMZqfZi0KSoxgD8JlkasdKiv3j5HH756pCql/nxn/+KZ86dV3U9VDryRSuvykjIwIHe9apey46uDnQ0LO9+SdUDwf76vqfKg30bcGDzOlUvc2v/JuzpSal6uV8Z6ebT96kdrOOROwexvas9+OI6N92g8keZigJl3PyCKm/ZsgH7Fylwx/ZeDASTuYWcKq8Lul/9gDkhbnaULGCJAvnJ8aAWYFlAgT8Gk8IulebsB6r0iaqUkZ+yj8N1vMBLrqsKXxN/UpUaligwf+ntihcUus6RnMhgKSelMq5lIjf1nmouWuYU1/yP+ijjcEGpcFm3fIExBgue9sOSoMoSBTzXwfTQGUZ34GKDiS7C6JVW5RbU4tPvnKNOpWBmLHhMz5/mAhNKIGMpEQeSDDyN0y+YvAuKRR6zB7VdB/+t+tRQVaDGkc5CFhOvvcBzzuNqMspzVIaW2zxyE2+9Aof5oGyd4PERGz/6NoS7m6IXlbulp/Icx7Pv2/Yok+chbefhp9WARVQU8AUuB1WFEU1Ai9KSODNeonSM9FCYO1JK5WUPEB5yTtSzZwLC+5vaMnnxSE+oGPDPi4HDr8o+y1HJhOMnv3HM1ZK/tiP9sMPbkeoNY03rfKlRWjtPq+nJy6NNuPRfDwn9TcQ8bkVP8uHMXfuza5zkrb2Z1nuY0LhfRHpCjiH/Ssyd/WdD+sWYbpzR26N/PyqOVrRXCgw/m70Hmv91VgdL6ksDfKS6Z3BDW5Zb6fNBoWH6/SQujiRgZRx4Dq3jaKPbRGjvJPRNGXSajejONXOqwC4qMWlkcDb+Aeb5JuBuySELmhC/D/veI8dSD74nhp7NPsAlnyyNWIpc3Ai5cGyDjqg4jCfFReFDxkW2dCxjR8agb5mF4elotCNcSCAXsmDppSPo0YtZBvhk3oTF7eNMUwmh7dUYBd9RPVaAMQ7bCtUtLplw0rj/H49hpHBRfduvdarS1YpIR03MRBcqi0toNZoZQ70NTYjwuuamtvON9xONM/cEfVbFusZ2PLz38/jZ/55X336mtPXXQqciKV7TEh7hT1Gx4BJYJYlkAfv6ovjK4Da0tBVqwvnaJEO8nlnSC1Ex/Fy2JgNcm2jUxvpNU4jHmZ4lPBky2Zg5HRf70zAjdbftirw1l0aRcVFNRMRzPWatlfUJR1z0bn2/urhEnvuGJOLJIrZdSiJm83sV1ClgpRm1mZrJAzThoSmRwbr1U8zMy1god5EJS2dTz2QcLVa8ehRrkBa7i67xOgVWYkPHKDauvYiGptLjY+TKFF6Wr5wAWR+ZTMszi4YFDX2zrdg8V33KlZm1LcxYjJcaqIA/HdSlqUxDSzWP8DxDr+raHI+ireaVI+tSVunDFBwt1r2+KyzeYC1zfiiV9zJx+WPujXtNeVWv/emGM8g88EIwButaGvGRVPW5LetSVp59LlLAaONM6aMGaZpWc1xYM5eauwK+/zvdH+u9Qh91KIHLDS/ILeEU0cD6uSz7IfNGp7nFWOtVs1BAzjeEPm+KWGPYezc9n3SEnr1uBSTe6PBDHPB48Fl6eEj4IEWBeysfHhq+qw0c/n6p4dqsSgG+vjSMvXGa1ftKkgB5//Oxwh14Xuw8xH9wxPUlA7IqBcp4o0Nf4Gpf5RW5BwXeUo49QvHPxcChJ7n44ji7CsD/AXsDwCqmZAeYAAAAAElFTkSuQmCC"),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "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"),
        ExportMetadata("BackgroundColor", "Lavender"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class MyPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new MyPluginControl();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public MyPlugin()
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