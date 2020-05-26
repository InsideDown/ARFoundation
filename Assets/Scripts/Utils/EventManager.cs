using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{

    protected EventManager() { }

    public delegate void ScreenNavigateAction(string newScene);
    public static event ScreenNavigateAction OnLoadNewScene;
    public static event ScreenNavigateAction OnSceneLoad;

    public delegate void GlobalClickAction();
    public static event GlobalClickAction OnGlobalClick;



    public void LoadNewScene(string newScreen = "")
    {
        OnLoadNewScene?.Invoke(newScreen);
    }

    public void GlobalClick()
    {
        OnGlobalClick?.Invoke();
    }

    
}