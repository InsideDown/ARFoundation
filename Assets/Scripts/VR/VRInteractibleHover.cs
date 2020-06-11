using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class VRInteractibleHover : VRInteractible
{

    public GameObject HoverBackgroundObject;
    public GameObject HoverIcon;
    public TextMeshProUGUI IconTxt;
    public CanvasGroup IconTxtCanvas;

    private float _AnimSpeed = 0.4f;
    private Vector3 _EndIconScale = Vector3.one;
    private Vector3 _EndBackgroundScale = Vector3.one;
    private Vector3 _StartIconScale = Vector3.zero;
    private Vector3 _StartBackgroundScale;

    protected override void Awake()
    {
        base.Awake();

        if (HoverBackgroundObject == null)
            throw new System.Exception("A Hover Object must be defined on VRInteractibleHover");

        if (HoverIcon == null)
            throw new System.Exception("A Hover HoverIcon must be defined on VRInteractibleHover");

        if (IconTxtCanvas != null)
            IconTxtCanvas.alpha = 0;

        _StartBackgroundScale = HoverBackgroundObject.transform.localScale;
        _EndIconScale = HoverIcon.transform.localScale;
        HoverIcon.transform.localScale = _StartIconScale;
                
    }

    public void OnGazedOn()
    {
        AnimIn();
    }

    public void OnGazedOff()
    {
        AnimOut();
    }

    private void AnimIn()
    {
        KillTweens();
        HoverBackgroundObject.transform.DOScale(_EndBackgroundScale, _AnimSpeed).SetEase(Ease.OutBack);
        HoverIcon.transform.DOScale(_EndIconScale, _AnimSpeed).SetEase(Ease.OutBack).SetDelay(0.2f);
        if (IconTxtCanvas != null)
            IconTxtCanvas.DOFade(1f, 0.3f);
    }

    private void AnimOut()
    {
        KillTweens();
        HoverIcon.transform.DOScale(_StartIconScale, _AnimSpeed).SetEase(Ease.InBack);
        HoverBackgroundObject.transform.DOScale(_StartBackgroundScale, _AnimSpeed).SetEase(Ease.InBack).SetDelay(0.2f);
        if (IconTxtCanvas != null)
            IconTxtCanvas.DOFade(0, 0.3f);
    }

    private void KillTweens()
    {
        DOTween.Kill(HoverIcon.transform);
        DOTween.Kill(HoverBackgroundObject.transform);
        if (IconTxtCanvas != null)
            DOTween.Kill(IconTxtCanvas.alpha);
    }

    private void OnDestroy()
    {
        KillTweens();
    }
}
