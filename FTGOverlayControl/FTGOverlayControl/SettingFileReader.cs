using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace FTGOverlayControl
{
    internal class SettingFileReader
    {
        private static string filename = "./settings.xml";

        public static SettingFile Load()
        {
            var data = LoadFromXmlFile(filename);
            return data;
        }

        public static void Save(SettingFile setting)
        {
            SerializeToXml(setting);
        }

        static void SerializeToXml(object obj)
        {
            using (var stream = new FileStream(filename, FileMode.Truncate, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(SettingFile));
                serializer.Serialize(stream, obj);
            }
        }

        static SettingFile LoadFromXmlFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SettingFile));
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var data = (SettingFile)serializer.Deserialize(stream);
                    return data;
                }
            }
            else
            {
                var dataToWhite = new SettingFile();
                dataToWhite.Players.Add(new PlayerSetting() { Name = "推理ロボKQGE", Score = 1 });
                dataToWhite.Players.Add(new PlayerSetting() { Name = "森ユーディ", Score = 2 });
                dataToWhite.InfoText = "決勝戦";

                SerializeToXml(dataToWhite);

                return LoadFromXmlFile(filePath);
            }
        }
    }
}
