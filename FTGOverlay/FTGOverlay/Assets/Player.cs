using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnChange;
    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            OnChange?.Invoke();
        }
    }

    private string _rank;
    public string Rank
    {
        get { return _rank; }
        set
        {
            _rank = value;
            OnChange?.Invoke();
        }
    }

    private string _character;
    public string Character
    {
        get { return _character; }
        set
        {
            _character = value;
            OnChange?.Invoke();
        }
    }

    private string _controlType;
    public string ControlType
    {
        get { return _controlType; }
        set
        {
            _controlType = value;
            OnChange?.Invoke();
        }
    }
}
