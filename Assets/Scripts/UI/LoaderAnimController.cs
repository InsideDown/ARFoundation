using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoaderAnimController : MonoBehaviour
{
    public CanvasGroup LoaderCanvasGroup;
    public List<RawImage> RawImageList;
    public CanvasGroup LoadTxtCanvasGroup;
    public TextMeshProUGUI LoadTxtTMP;
    public bool ShowLoadText = true;

    private float _StartYPos;
    private float _EndYPos;
    private float _EndYIncrement = -40.0f;
    private float _BoxTweenDuration = 0.4f;
    private float _BoxTweenDelay = 0.3f;
    //pause between starting animation over
    private float _BoxPauseDuration = 0.8f;
    private float _TxtEndY;
    private float _TxtStartY;
    private float _TxtEndYIncrement = 40.0f;

    public void Awake()
    {
        if (LoaderCanvasGroup == null)
            throw new System.Exception("A LoaderCanvasGroup must be defined in LoaderAnimController");

        if (RawImageList.Count == 0)
            throw new System.Exception("A RawImageList must be defined in LoaderAnimController");

        if (LoadTxtCanvasGroup == null)
            throw new System.Exception("A LoadTxtCanvasGroup must be defined in LoaderAnimController");

        if (LoadTxtTMP == null)
            throw new System.Exception("A LoadTxtTMP must be defined in LoaderAnimController");

        _StartYPos = RawImageList[0].transform.localPosition.y;
        _EndYPos = _StartYPos - _EndYIncrement;
        _TxtEndY = LoadTxtTMP.transform.localPosition.y;
        _TxtStartY = LoadTxtTMP.transform.localPosition.y + _TxtEndYIncrement;
        LoadTxtTMP.transform.localPosition = new Vector3(LoadTxtTMP.transform.localPosition.x, _TxtStartY, LoadTxtTMP.transform.localPosition.z);
        LoadTxtCanvasGroup.alpha = 0;
        LoaderCanvasGroup.alpha = 0;
    }

    private void AnimIn()
    {
        LoaderCanvasGroup.DOFade(1, 0.4f);

        if(ShowLoadText)
        {
            Sequence txtSequence = DOTween.Sequence();
            txtSequence.Append(LoadTxtCanvasGroup.DOFade(1.0f, 0.4f));
            txtSequence.Insert(0.2f, LoadTxtTMP.transform.DOLocalMoveY(_TxtEndY, 0.3f)).SetDelay(-0.3f);
            txtSequence.OnComplete(() => AnimBoxes());
        }
        else
        {
            AnimBoxes();
        }
    }

    private void AnimBoxes()
    {
        for (int i = 0; i < RawImageList.Count; i++)
        {
            RawImage curImage = RawImageList[i];
            float sequenceStartTiming = i * _BoxTweenDelay;
            AnimUp(curImage, sequenceStartTiming);
        }
    }

    private void AnimUp(RawImage curSquare, float delay = 0)
    {
        if(curSquare != null)
            curSquare.transform.DOLocalMoveY(_EndYPos, _BoxTweenDuration).SetDelay(delay)
                .SetEase(Ease.OutBack).OnComplete(()=>AnimDown(curSquare));
    }

    private void AnimDown(RawImage curSquare)
    {
        curSquare.transform.DOLocalMoveY(_StartYPos, _BoxTweenDuration).SetDelay(_BoxTweenDelay)
            .SetEase(Ease.OutBounce).OnComplete(() => AnimUp(curSquare, _BoxPauseDuration + _BoxTweenDuration));
    }

    private void AnimOut()
    {

        LoaderCanvasGroup.DOFade(0.0f, 0.3f)
            .OnComplete(()=> { Kill(); EventManager.Instance.LoadAnimOutComplete(); });
    }

    //****************
    // EVENTS
    //****************
    private void OnEnable()
    {
        EventManager.OnShowLoader += EventManager_OnShowLoader;
        EventManager.OnHideLoader += EventManager_OnHideLoader;
    }

    private void OnDisable()
    {
        EventManager.OnShowLoader -= EventManager_OnShowLoader;
        EventManager.OnHideLoader -= EventManager_OnHideLoader;
    }

    private void EventManager_OnShowLoader()
    {
        AnimIn();
    }

    private void EventManager_OnHideLoader()
    {
        AnimOut();
    }

    private void Kill()
    {
        for (int i = 0; i < RawImageList.Count; i++)
        {
            RawImage curImage = RawImageList[i];
            DOTween.Kill(curImage.gameObject.transform);
        }
        DOTween.Kill(LoaderCanvasGroup);
        DOTween.Kill(LoadTxtTMP.transform);
    }

    /// <summary>
    /// Ensure our animations aren't trying to continue if we're removed
    /// </summary>
    private void OnDestroy()
    {
        Kill();
    }
}
