using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PingPonger.Messages
{
    /// <summary>
    /// <see href="https://stackoverflow.com/questions/24219144/native-messaging-chrome/24219903#24219903">Native Messaging</see> handler
    /// </summary>
    internal class MessageHandler
    {
        public static string Process(JObject data)
        {
            try
            {
                string message = data["text"].Value<string>();

                return message switch
                {
                    "ping" => "pong",
                    "exit" => "exit",
                    _ => "unknown message: " + message,
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Process error: {ex.Message}", ex);
            }
        }

        public static JObject? Read()
        {
            try
            {
                var stdin = Console.OpenStandardInput();
                var length = 0;

                // We need to read first 4 bytes for length information
                var lengthBytes = new byte[4];
                stdin.Read(lengthBytes, 0, 4);
                length = BitConverter.ToInt32(lengthBytes, 0);

                var buffer = new char[length];

                using (var reader = new StreamReader(stdin))
                    while (reader.Peek() >= 0)
                        reader.Read(buffer, 0, buffer.Length);

                var message = new string(buffer);
                
                JObject? value;

                try
                {
                    value = JsonConvert.DeserializeObject<JObject>(message);
                }
                catch
                {
                    value = new JObject { ["text"] = message.Trim('"') };
                }

                return value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Read error: {ex.Message}", ex);
            }
        }

        public static void Write(JToken data)
        {
            try
            {
                var json = new JObject { ["text"] = data };
                var bytes = System.Text.Encoding.UTF8.GetBytes(json.ToString(Formatting.None));

                var stdout = Console.OpenStandardOutput();
                
                // We need to send the 4 btyes of length information
                stdout.WriteByte((byte)((bytes.Length >> 0) & 0xFF));
                stdout.WriteByte((byte)((bytes.Length >> 8) & 0xFF));
                stdout.WriteByte((byte)((bytes.Length >> 16) & 0xFF));
                stdout.WriteByte((byte)((bytes.Length >> 24) & 0xFF));
                stdout.Write(bytes, 0, bytes.Length);
                stdout.Flush();
            }
            catch (Exception ex)
            {
                throw new Exception($"Write error: {ex.Message}", ex);
            }
        }
    }
}
