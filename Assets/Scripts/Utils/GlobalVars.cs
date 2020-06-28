using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVars : Singleton<GlobalVars>
{

    protected GlobalVars() { }

    private bool _IsApplicationQuitting = false;

    private void Awake()
    {
        Debug.Log("target framerate: " + Application.targetFrameRate);
        StartingFrameRate = Application.targetFrameRate;
    }

    public enum CurrentScene
    {
        LobbyScene = 0,
        EmergencyDeptScene = 1,
        ImagingScene = 2,
        PatientRoomScene = 3,
        RehabilitationScene = 4
    }

    //note, scene IDs need to align
    public int[] IsSceneAudioPlayed = new int[] { 0, 0, 0, 0, 0 };

    public bool IsCardboard = false;

    private int StartingFrameRate = 30;

    private int ScrollFrameRate = 60;

    //TODO: need to check to see if Android has similar scrolling issues
    public void ResetFrameRate()
    {
        if (_IsApplicationQuitting) return;

#if UNITY_IOS
        Application.targetFrameRate = StartingFrameRate;
#endif
    }

    public void SetScrollFrameRate()
    {
        if (_IsApplicationQuitting) return;

#if UNITY_IOS
        Application.targetFrameRate = ScrollFrameRate;
#endif
    }

    private void OnApplicationQuit()
    {
        _IsApplicationQuitting = true;
    }

    //public void CheckRequired(object obj, string nameStr, string scriptNameStr)
    //{
    //    if (obj == null)
    //        throw new Exception(String.Format("A {0} is required in {1}", nameStr, scriptNameStr));
    //}

    //[Serializable]
    //public struct SceneAudio
    //{
    //    public string SceneName;
    //    public bool IsInitialPlaythrough;
    //}

    //public List<SceneAudio> SceneAudioList = new List<SceneAudio>();

    //private void Awake()
    //{
    //    Debug.Log("fired awake of global vars");
    //}

}