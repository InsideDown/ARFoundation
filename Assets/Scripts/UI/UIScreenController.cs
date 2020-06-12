using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIScreenController : MonoBehaviour
{

    public GameObject UIScreenHolder;
    public UIScreen UIScreenPrefab;

    private UIScreen _CurUIScreen;
    private float _AnimInSpeed = 0.35f;
    private RectTransform _UIScreenHolderRectTransform;

    private void Awake()
    {
        if (UIScreenHolder == null)
            throw new System.Exception("A UIScreenHolder must be defined in UIScreenController");

        if (UIScreenPrefab == null)
            throw new System.Exception("A UIScreenPrefab must be defined in UIScreenController");

        if (UIScreenHolder.GetComponent<RectTransform>() != null)
        {
            _UIScreenHolderRectTransform = UIScreenHolder.GetComponent<RectTransform>();
        }
        else
        {
            throw new System.Exception("RectTransforms are not defined in UIScreenController");
        }
    }

    public void OpenUIScreen()
    {
        EventManager.Instance.UIScreenOpen();
        UnInit();
        AnimUIScreenIn();
    }

    /// <summary>
    /// Animate our UI In
    /// </summary>
    private void AnimUIScreenIn()
    {
        _CurUIScreen = Instantiate(UIScreenPrefab, UIScreenHolder.transform, false);

        float rectWidth = _UIScreenHolderRectTransform.rect.width;
        float rectHeight = _UIScreenHolderRectTransform.rect.height;
        if (_CurUIScreen.GetComponent<RectTransform>() != null)
        {
            RectTransform curRect = _CurUIScreen.GetComponent<RectTransform>();
            curRect.transform.position = new Vector3(rectWidth, rectHeight, 0);
            curRect.transform.DOMoveX(0, _AnimInSpeed);
        }
        else
        {
            throw new System.Exception("RectTransforms are not defined in UIScreenController");
        }
    }

    private void AnimUIScreenOut()
    {
        Debug.Log("closing UI Screen");
        float rectWidth = _UIScreenHolderRectTransform.rect.width;
        if (_CurUIScreen.GetComponent<RectTransform>() != null)
        {
            RectTransform curRect = _CurUIScreen.GetComponent<RectTransform>();
            curRect.transform.DOMoveX(rectWidth, _AnimInSpeed).SetEase(Ease.OutQuad).OnComplete(() => UIScreenCloseComplete());
        }
        else
        {
            throw new System.Exception("RectTransforms are not defined in UIScreenController");
        }
    }

    private void UIScreenCloseComplete()
    {
        _CurUIScreen.UnInit();
        EventManager.Instance.UIScreenCloseComplete();
    }

    //*************
    /// EVENTS
    //*************

    private void OnEnable()
    {
        EventManager.OnUIScreenClose += EventManager_OnUIScreenClose;
    }

    private void OnDisable()
    {
        EventManager.OnUIScreenClose -= EventManager_OnUIScreenClose;
    }

    private void EventManager_OnUIScreenClose(string newScene)
    {
        AnimUIScreenOut();
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
