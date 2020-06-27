using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoaderAnimController : MonoBehaviour
{
    public CanvasGroup LoaderCanvasGroup;
    public List<RawImage> RawImageList;

    private float _StartYPos;
    private float _EndYPos;
    private float _EndYIncrement = -40.0f;
    private float _BoxTweenDuration = 0.4f;
    private float _BoxTweenDelay = 0.3f;
    //pause between starting animation over
    private float _BoxPauseDuration = 0.8f;

    public void Awake()
    {
        if (LoaderCanvasGroup == null)
            throw new System.Exception("A LoaderCanvasGroup must be defined in LoaderAnimController");

        if (RawImageList.Count == 0)
            throw new System.Exception("A RawImageList must be defined in LoaderAnimController");


        _StartYPos = RawImageList[0].transform.localPosition.y;
        _EndYPos = _StartYPos - _EndYIncrement;

    }

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.0f);
        AnimIn();
    }

    private void AnimIn()
    {
        //might revist using sequences
        //Sequence loadSequence = DOTween.Sequence();
        //loadSequence.Insert(sequenceStartTiming, curImage.transform.DOLocalMoveY(_EndYPos, _BoxTweenDuration).SetEase(Ease.OutBack));

        for (int i=0;i<RawImageList.Count;i++)
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

    private void Kill()
    {
        for (int i = 0; i < RawImageList.Count; i++)
        {
            RawImage curImage = RawImageList[i];
            DOTween.Kill(curImage.gameObject.transform);
        }
    }

    /// <summary>
    /// Ensure our animations aren't trying to continue if we're removed
    /// </summary>
    private void OnDestroy()
    {
        Kill();
    }
}
