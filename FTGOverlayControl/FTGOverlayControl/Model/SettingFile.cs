using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class PlayerSetting
{
    public string Name { get; set; }
    public int Score { get; set; }
    public string FilePath { get; set; }
    public string Copy { get; set; }
    public string Character { get; set; }
    public string ControlType { get; set; }
    public string Rank { get; set; }
}

[Serializable]
public class SettingFile
{
    public List<PlayerSetting> Players { get; } = new List<PlayerSetting>();
    public string InfoText { get; set; }
}
