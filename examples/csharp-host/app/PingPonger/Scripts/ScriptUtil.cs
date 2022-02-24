using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace PingPonger.Scripts
{
    internal class ScriptUtil
    {
        public static void UpdateScript()
        {
            var batName = "ping_ponger.bat";

            var currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var appPath = Path.Combine(currentPath, "PingPonger.exe");
            var scriptsPath = Path.Combine(currentPath, "Scripts");

            if (Directory.Exists(scriptsPath))
            {
                var batInputPath = Path.Combine(scriptsPath, batName);
                var batOutputPath = Path.Combine(currentPath, batName);

                ReplaceBatPathValue(batInputPath, batOutputPath, appPath);

                Directory.Delete(scriptsPath, true);
            }
        }

        private static void ReplaceBatPathValue(string inputPath, string outputPath, string appPath)
        {
            var text = File.ReadAllText(inputPath);

            text = text.Replace("path\\to\\exe", appPath);

            File.WriteAllText(outputPath, text);
        }
    }
}
