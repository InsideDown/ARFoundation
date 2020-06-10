using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenController : MonoBehaviour
{

    public GameObject UIScreenHolder;
    public UIScreen UIScreenPrefab;

    public void Awake()
    {
        if (UIScreenHolder == null)
            throw new System.Exception("A UIScreenHolder must be defined in UIScreenController");

        if (UIScreenPrefab == null)
            throw new System.Exception("A UIScreenPrefab must be defined in UIScreenController");
    }


    public void OpenUIScreen()
    {

    }
}
