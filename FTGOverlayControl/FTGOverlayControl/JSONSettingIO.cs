using FTGOverlayControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Xml.Serialization;

namespace JsonSample
{
    public class JsonUtilSample
    {
        public static void ToJson<T>(string filename, T data)
        {
            using (var stream = new FileStream(filename, FileMode.Truncate, FileAccess.Write))
            {
                JsonSerializer.Serialize(stream, data);
            }
        }

        private static JsonSerializerOptions GetOption()
        {
            // ユニコードのレンジ指定で日本語も正しく表示、インデントされるように指定
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };
            return options;
        }

        public static T Read<T>(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return JsonSerializer.Deserialize<T>(stream);
            }
        }
    }
}

