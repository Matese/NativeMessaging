using Newtonsoft.Json.Linq;
using PingPonger.Manifests;
using PingPonger.Messages;
using PingPonger.Scripts;

namespace Ponger
{
    internal class Program
    {
        public static void Main()
        {
            AppManifestUtil.UpdateAppManifests();
            ScriptUtil.UpdateScript();

        Loop:
            try
            {
                JObject? data;
                while ((data = MessageHandler.Read()) != null)
                {
                    var processed = MessageHandler.Process(data);

                    MessageHandler.Write(processed);

                    if (processed == "exit")
                        return;
                }
            }
            catch (Exception ex)
            {
                MessageHandler.Write(ex.Message);
                goto Loop;
            }
        }
    }
}