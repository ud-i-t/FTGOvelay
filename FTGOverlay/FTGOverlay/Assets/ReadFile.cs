using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

public class ReadFile : MonoBehaviour
{
    public Player Player1;
    public Player Player2;
    public FTGOverlay.Score Score1;
    public FTGOverlay.Score Score2;
    public TMPro.TMP_Text InfoText;

    void Start()
    {
        //var dataToWhite = new SettingFile();
        //dataToWhite.Players.Add(new PlayerSetting() { Name = "推理ロボKQGE", Score = 1 });
        //dataToWhite.Players.Add(new PlayerSetting() { Name = "森ユーディ", Score = 2 });
        //dataToWhite.InfoText = "決勝戦";

        //var xml = SerializeToXml(dataToWhite);
        //SaveXmlToFile(xml, "./settings.xml");

        StartCoroutine(nameof(LoadAndUpdateDisplay));
    }

    private IEnumerator LoadAndUpdateDisplay()
    {
        for (;;)
        {
            var data = LoadFromXmlFile("./settings.xml");
            Player1.Name = data.Players[0].Name;
            Player2.Name = data.Players[1].Name;
            Player1.Rank = data.Players[0].Rank;
            Player2.Rank = data.Players[1].Rank;
            Player1.Character = data.Players[0].Character;
            Player2.Character = data.Players[1].Character;
            Player1.ControlType = data.Players[0].ControlType;
            Player2.ControlType = data.Players[1].ControlType;

            if (Score1) Score1.Value = data.Players[0].Score;
            if (Score2) Score2.Value = data.Players[1].Score;
            if (InfoText) InfoText.text = data.InfoText;

            yield return new WaitForSeconds(1f);
        }
    }

    static string SerializeToXml(object obj)
    {
        XmlSerializer serializer = new XmlSerializer(obj.GetType());

        using (StringWriter writer = new StringWriter())
        {
            serializer.Serialize(writer, obj);
            return writer.ToString();
        }
    }

    static void SaveXmlToFile(string xmlString, string filePath)
    {
        File.WriteAllText(filePath, xmlString);
    }

    static SettingFile LoadFromXmlFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SettingFile));

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    // ファイルからXMLデータを逆シリアライズしてオブジェクトに復元
                    return (SettingFile)serializer.Deserialize(fileStream);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
        else
        {
            Debug.LogError("指定されたファイルが存在しません。");
        }

        return null;
    }
}
