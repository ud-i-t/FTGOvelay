using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string _name;
    public string Name 
    { 
        get { return _name; }
        set
        {
            _name = value;
            OnChangeName(_name);
        }
    }
    public event Action<string> OnChangeName;
}
