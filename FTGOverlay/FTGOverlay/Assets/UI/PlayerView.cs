using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public TMPro.TMP_Text Text;
    public Player Player;

    public void Awake()
    {
        Player.OnChangeName += (x) => Text.text = x;       
    }
}
