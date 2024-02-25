using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public TMPro.TMP_Text Name;
    public TMPro.TMP_Text Character;
    public TMPro.TMP_Text Rank;
    public TMPro.TMP_Text ControlType;
    public Player Player;

    public void Awake()
    {
        Player.OnChange += () =>
        { 
            Name.text = Player.Name;

            if (Character) Character.text = Player.Character;
            if (Rank) Rank.text = Player.Rank == null ? "Empty" : Player.Rank;
            if (ControlType) ControlType.text = Player.ControlType == null ? "Empty" : Player.ControlType;
        };
    }
}
