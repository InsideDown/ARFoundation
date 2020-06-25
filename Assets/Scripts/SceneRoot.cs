using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRoot : MonoBehaviour
{
    private void Awake()
    {
        DataLoader.Instance.LoadJSON();
    }

    private void Init()
    {
        Debug.Log("init scene");
    }



    //******************
    // EVENTS
    //******************

    private void OnEnable()
    {
        EventManager.OnMainDataLoaded += EventManager_OnMainDataLoaded;
        EventManager.OnMainDataLoadError += EventManager_OnMainDataLoadError;
    }

    private void OnDisable()
    {
        EventManager.OnMainDataLoaded -= EventManager_OnMainDataLoaded;
        EventManager.OnMainDataLoadError -= EventManager_OnMainDataLoadError;
    }

    private void EventManager_OnMainDataLoadError()
    {
        Debug.Log("Main data loaded error");
    }

    private void EventManager_OnMainDataLoaded()
    {
        Debug.Log("main data loaded");
        Init();
    }
}
