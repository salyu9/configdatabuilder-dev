using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Untitled.ConfigDataBuilder.Editor;

namespace ConfigDataBuilderDev.Editor
{
    public class JsonL10nExporter : IL10nExporter
    {
        private const string Folder = "Assets/Localization/";

        public void Export(IList<L10nData> l10nList)
        {
            foreach (var data in l10nList)
            {
                var jDoc = new JObject();

                foreach (var row in data.Rows)
                {
                    var key = row.Key;
                    var properties = row.Properties;

                    var jProps = new JObject();
                    foreach (var property in properties)
                    {
                        var name = property.Name;
                        var value = property.Value;
                        if (value is string str)
                        {
                            jProps.Add(name, new JValue(str));
                        }
                        else if (value is string[] strs)
                        {
                            jProps.Add(name, new JArray(strs.Cast<object>()));
                        }
                    }
                    jDoc.Add(key, jProps);
                }

                if (!Directory.Exists(Folder)) {
                    Directory.CreateDirectory(Folder);
                }
                var jsonPath = Folder + data.SheetName + ".json";
                using var writer = new StreamWriter(jsonPath);
                var jsonWriter = new JsonTextWriter(writer)
                {
                    Formatting = Formatting.Indented
                };
                jDoc.WriteTo(jsonWriter);
            }
        }
    }
}
