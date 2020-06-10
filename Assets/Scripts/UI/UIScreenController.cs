using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenController : MonoBehaviour
{

    public GameObject UIScreenHolder;
    public UIScreen UIScreenPrefab;

    private UIScreen _CurUIScreen;

    public void Awake()
    {
        if (UIScreenHolder == null)
            throw new System.Exception("A UIScreenHolder must be defined in UIScreenController");

        if (UIScreenPrefab == null)
            throw new System.Exception("A UIScreenPrefab must be defined in UIScreenController");
    }


    public void OpenUIScreen()
    {
        Debug.Log("open the UI screen");
        UnInit();
        _CurUIScreen = Instantiate(UIScreenPrefab, UIScreenHolder.transform,false);
        RectTransform curHolderRect = UIScreenHolder.GetComponent<RectTransform>();
        float rectWidth = curHolderRect.rect.width;
        float rectHeight = curHolderRect.rect.height;
        RectTransform curRect = _CurUIScreen.GetComponent<RectTransform>();
        //curRect.position = new Vector2(250, 200);
        curRect.transform.position = new Vector3(rectWidth, rectHeight, 0);
        
   
    }

    private void UnInit()
    {
        foreach (Transform child in UIScreenHolder.transform)
        {
            Destroy(child.gameObject);
        }
        _CurUIScreen = null;
    }

    private void OnDestroy()
    {
        UnInit();   
    }
}
