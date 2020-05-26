using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugTxt : Singleton<DebugTxt>
{

    protected DebugTxt() { }

    public TextMeshProUGUI TxtDebug;

    private void Start()
    {
        if (TxtDebug == null)
            throw new System.Exception("A TxtDebug must be defined in DebugTxt");
    }

    public void AddTxt(string txtToAdd = "")
    {
        TxtDebug.text = TxtDebug.text + "\n" + txtToAdd;
    }

    public void SetTxt(string txtToSet = "")
    {
        TxtDebug.text = txtToSet;
    }

}
