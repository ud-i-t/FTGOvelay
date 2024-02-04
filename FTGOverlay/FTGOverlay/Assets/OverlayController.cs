using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    public KeyInput Input;
    public FTGOverlay.Score Score1P;
    public FTGOverlay.Score Score2P;
    public GameObject Settings;

    void Start()
    {
        Input.OnPush1 += Score1P.OnAddScore;
        Input.OnPush2 += Score2P.OnAddScore;
        Input.OnPushS += () => Settings.SetActive(true);
        Input.OnPushR += Score1P.ResetScore;
        Input.OnPushR += Score2P.ResetScore;
    }
}
