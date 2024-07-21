using System.Runtime.InteropServices;
using System.Text.Json;

namespace HHD.Service
{
    public class Localization
    {
        public static Dictionary<string, Localization> Languages = new Dictionary<string, Localization>()
        {
            { "ru", new Localization("ru") },
            { "en", new Localization("en") }
        };

        private Dictionary<string, string> labels = new Dictionary<string, string>();

        public Localization(string language)
        {
            using (StreamReader sr = new StreamReader("./Localizations/Label-" + language + ".json"))
            {
                string json = sr.ReadToEnd();
                labels = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }
        }

        public string GetLabel(string label, string def = "")
        {
            if (labels.ContainsKey(label))
            {
                return labels[label];   
            }
            return def;
        }
    }
}
