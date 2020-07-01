using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollHelper : MonoBehaviour
{
    //TODO: This will all need to be rewritten due to the calculations for these items being dynamic
    //with dynamic information coming in, heights will be adjusted and scroll reset
    public RectTransform ScrollHolder;

    private bool _IsApplicationQuitting = false;

    private void Awake()
    {
        if (ScrollHolder == null)
            throw new System.Exception("A ScrollHolder is not defined in ScrollHelper");
    }

    private void Start()
    {
        //we need to speed up
        GlobalVars.Instance.SetScrollFrameRate();
    }

    private void OnDestroy()
    {
        if (_IsApplicationQuitting) return; 
        //we need to slow down
        GlobalVars.Instance.ResetFrameRate();
    }

    /// <summary>
    /// due to how Unity sets up its scrolling, we need to find the heights of all of the direct decendants
    /// of our ScrollHolder and set ScrollHolder's height to those values combined
    /// </summary>
    //private void SetScrollSize()
    //{
    //    float rectHolderHeight = ScrollHolder.rect.height;
    //    Debug.Log(rectHolderHeight);
    //    foreach (Transform child in ScrollHolder.transform)
    //    {
    //        RectTransform curRect = child.GetComponent<RectTransform>();
    //        if (curRect != null)
    //            Debug.Log(curRect.rect.height);
    //    }
    //}

    private void OnApplicationQuit()
    {
        _IsApplicationQuitting = true;
    }

}
