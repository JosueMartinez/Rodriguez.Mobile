using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Rodriguez.Mobile.Classes
{
    public class AppSettingsManager
    {
        //instance of the singleton
        private static AppSettingsManager _instance;

        //appsettings in memory
        private JObject _secrets;

        //files
        private const string Namespace = "Rodriguez.Mobile";
        private const string Filename = "appsettings.json";

        //singleton instance
        private AppSettingsManager()
        {
            try
            {
                var assembly = Assembly.GetAssembly(typeof(AppSettingsManager));
                var stream = assembly.GetManifestResourceStream($"{Namespace}.{Filename}");

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    _secrets = JObject.Parse(json);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Unable to load secrets file");
            }
            
        }

        public static AppSettingsManager Settings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppSettingsManager();
                }

                return _instance;
            }
        }

        public string this[string name]
        {
            get
            {
                try
                {
                    var path = name.Split(':');

                    JToken node = _secrets[path[0]];
                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Unable to retrieve secret '{name}'");
                    return string.Empty;
                }
            }
        }
    }
}
