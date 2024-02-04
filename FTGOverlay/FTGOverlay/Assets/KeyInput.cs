using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour
{
    public event Action OnPush1;
    public event Action OnPush2;
    public event Action OnPushS;
    public event Action OnPushR;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnPush1();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnPush2();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            OnPushS();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            OnPushR();
        }
    }
}
