using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : Singleton<GlobalVars>
{

    protected GlobalVars() { }

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