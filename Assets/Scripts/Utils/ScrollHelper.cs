using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollHelper : MonoBehaviour
{
    private void OnEnable()
    {
        //we need to speed up
        GlobalVars.Instance.SetScrollFrameRate();
    }

    private void OnDisable()
    {
        //we need to slow down
        GlobalVars.Instance.ResetFrameRate();
    }
}
