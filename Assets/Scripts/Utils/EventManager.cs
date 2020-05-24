using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{

    protected EventManager() { }

    public delegate void ScreenNavigateAction(string newScene);
    public static event ScreenNavigateAction OnLoadNewScene;
    public static event ScreenNavigateAction OnSceneLoad;

    public delegate void VideoAction();
    public static event VideoAction OnVideoClose;

    public delegate void ScreenClickAction();
    public static event ScreenClickAction OnScreenClick;

    public delegate void AudioAction();
    public static event AudioAction OnAudioEnd;


    public void LoadNewScene(string newScreen = "")
    {
        if (OnLoadNewScene != null)
            OnLoadNewScene(newScreen);
    }

    public void VideoClose()
    {
        if (OnVideoClose != null)
            OnVideoClose();
    }

    public void ScreenClick()
    {
        if (OnScreenClick != null)
            OnScreenClick();
    }

    public void AudioEnd()
    {
        if (OnAudioEnd != null)
            OnAudioEnd();
    }
}