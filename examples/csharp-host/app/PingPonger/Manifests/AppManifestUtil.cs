using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace PingPonger.Manifests
{
    internal class AppManifestUtil
    {
        /// <summary>
        /// In order to register a native messaging host, the application must install a manifest file that defines the native messaging host configuration.
        /// There is some differences between 
        /// <see href="https://developer.mozilla.org/en-US/docs/Mozilla/Add-ons/WebExtensions/Native_messaging">firefox</see> and
        /// <see href="https://developer.chrome.com/docs/apps/nativeMessaging/">chrome</see>.
        /// </summary>
        public static void UpdateAppManifests()
        {
            var chromeManifestName = "ChromeAppManifest.json";
            var firefoxManifestName = "FirefoxAppManifest.json";

            var currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var appPath = Path.Combine(currentPath, "ping_ponger.bat");
            var manifestsPath = Path.Combine(currentPath, "Manifests");

            if (Directory.Exists(manifestsPath))
            {
                var chromeManifestInputPath = Path.Combine(manifestsPath, chromeManifestName);
                var firefoxManifestInputPath = Path.Combine(manifestsPath, firefoxManifestName);

                var chromeManifestOutputPath = Path.Combine(currentPath, chromeManifestName);
                var firefoxManifestOutputPath = Path.Combine(currentPath, firefoxManifestName);

                ReplaceAppManifestPathValue(chromeManifestInputPath, chromeManifestOutputPath, appPath);
                ReplaceAppManifestPathValue(firefoxManifestInputPath, firefoxManifestOutputPath, appPath);

                Directory.Delete(manifestsPath, true);
            }
        }

        private static void ReplaceAppManifestPathValue(string inputPath, string outputPath, string appPath)
        {
            var json = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(inputPath));

            json.Remove("path");
            json.Add("path", JToken.FromObject(appPath));

            File.WriteAllText(outputPath, JsonConvert.SerializeObject(json));
        }
    }
}
