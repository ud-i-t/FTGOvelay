using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class PlayerSetting
{
    public string Name { get; set; }
    public int Score { get; set; }
}

[Serializable]
public class SettingFile
{
    public List<PlayerSetting> Players { get; set; } = new List<PlayerSetting>();
    public string InfoText { get; set; }
}
