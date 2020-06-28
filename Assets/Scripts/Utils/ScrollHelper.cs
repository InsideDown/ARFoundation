using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollHelper : MonoBehaviour
{
    private void Awake()
    {
        //we need to speed up
        GlobalVars.Instance.SetScrollFrameRate();
    }

    private void OnDestroy()
    {
        //we need to slow down
        GlobalVars.Instance.ResetFrameRate();
    }
}
