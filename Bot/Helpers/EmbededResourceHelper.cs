using System.IO;
using System.Reflection;

namespace Bot.Helpers
{
    public class EmbededResourceHelper
    {
        public static string Get(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}