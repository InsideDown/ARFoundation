using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSceneSwitch : MonoBehaviour
{
    public void OnClick(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            EventManager.Instance.LoadNewScene(sceneName);
        }else
        {
            throw new System.Exception("sceneName is not defined in BtnSceneSwitch");
        }
    }
}
