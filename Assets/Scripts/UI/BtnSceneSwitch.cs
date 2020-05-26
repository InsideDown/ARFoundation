using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSceneSwitch : MonoBehaviour
{
    public void OnClick(string sceneName)
    {
        if(sceneName != null)
            EventManager.Instance.LoadNewScene(sceneName);
    }
}
