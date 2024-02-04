using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public TMPro.TMP_InputField PlayerNameInput1;
    public Player Player1;

    public TMPro.TMP_InputField PlayerNameInput2;
    public Player Player2;

    public TMPro.TMP_InputField InfoTextInput;
    public TMPro.TMP_Text InfoText;

    public void OnApply()
    {
        Player1.Name = PlayerNameInput1.text;
        Player2.Name = PlayerNameInput2.text;
        InfoText.text = InfoTextInput.text;
    }
}
