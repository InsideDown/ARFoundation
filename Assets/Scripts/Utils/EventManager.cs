﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{

    protected EventManager() { }

    public delegate void ScreenNavigateAction(string newScene);
    public static event ScreenNavigateAction OnLoadNewScene;
    //whenever one of our 2d screens are open
    public static event ScreenNavigateAction OnUIScreenOpen;
    public static event ScreenNavigateAction OnUIScreenClose;
    public static event ScreenNavigateAction OnUIScreenCloseComplete;

    public delegate void GlobalClickAction();
    public static event GlobalClickAction OnGlobalClick;

    public delegate void ARAction();
    public static event ARAction OnARPlacementAllowed;
    public static event ARAction OnARObjectPlaced;



    public void LoadNewScene(string newScreen = "")
    {
        OnLoadNewScene?.Invoke(newScreen);
    }

    public void UIScreenOpen(string newScreen = "")
    {
        OnUIScreenOpen?.Invoke(newScreen);
    }

    public void UIScreenClose(string newScreen = "")
    {
        OnUIScreenClose?.Invoke(newScreen);
    }

    public void UIScreenCloseComplete(string newScreen = "")
    {
        OnUIScreenCloseComplete?.Invoke(newScreen);
    }

    public void GlobalClick()
    {
        OnGlobalClick?.Invoke();
    }

    public void ARPlacementAllowed()
    {
        OnARPlacementAllowed?.Invoke();
    }

    public void ARObjectPlaced()
    {
        OnARObjectPlaced?.Invoke();
    }
}