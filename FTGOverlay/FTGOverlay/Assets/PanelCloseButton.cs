using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCloseButton : MonoBehaviour
{
    public GameObject Target;

    public void OnExecute()
    {
        Target.SetActive(false);
    }
}
