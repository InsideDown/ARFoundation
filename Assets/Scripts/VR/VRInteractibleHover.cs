using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInteractibleHover : VRInteractible
{

    public GameObject HoverBackgroundObject;
    public GameObject HoverIcon;

    protected override void Awake()
    {
        base.Awake();

        if (HoverBackgroundObject == null)
            throw new System.Exception("A Hover Object must be defined on VRInteractibleHover");

        if (HoverIcon == null)
            throw new System.Exception("A Hover HoverIcon must be defined on VRInteractibleHover");
    }

    public void OnGazedOn()
    {
        Debug.Log("gazed on vr interactible");
    }

    public void OnGazedOff()
    {
        Debug.Log("gazed off interactible");
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using DG.Tweening;
//using TMPro;
//using UnityEngine;
//using UnityEngine.EventSystems;

//[RequireComponent(typeof(Collider))]
//public class InteractibleHotspotHover : MonoBehaviour
//{
//    public GameObject HoverBackgroundObject;
//    public GameObject HoverIcon;
//    public TextMeshProUGUI IconTxt;
//    public CanvasGroup IconTxtCanvas;

//    private Collider _BaseCollider;
//    private Vector3 _StartingScale;
//    private Vector3 _StartingHoverIconScale = new Vector3(0, 0, 0);
//    private Vector3 _EndingHoverIconScale;
//    private Vector3 _EndScale = new Vector3(1.0f, 1.0f, 1.0f);
//    private float _AnimSpeed = 0.4f;
//    //check to see if we are an audio hotspot. If so, we have external controls
//    private HotspotAudioController _HotspotAudioController;

//    private void Awake()
//    {
//        if (this.gameObject.GetComponent<Collider>() == null)
//        {
//            throw new System.Exception("A collider must be attached to an InteractibleGameObject");
//        }
//        else
//        {
//            _BaseCollider = this.gameObject.GetComponent<Collider>();
//        }

//        if (HoverBackgroundObject == null)
//            throw new System.Exception("A Hover Object must be defined on InteractibleHotspotHover");

//        if (HoverIcon == null)
//            throw new System.Exception("A Hover HoverIcon must be defined on InteractibleHotspotHover");

//        if (IconTxtCanvas != null)
//            IconTxtCanvas.alpha = 0;


//        _StartingScale = HoverBackgroundObject.transform.localScale;
//        _EndingHoverIconScale = HoverIcon.transform.localScale;
//        HoverIcon.transform.localScale = Vector3.zero;
//        _HotspotAudioController = this.gameObject.GetComponent<HotspotAudioController>();
//    }

//    public void AnimIn()
//    {
//        //if we're a hotspotaudio and we're playing, we should not animate
//        //if (_HotspotAudioController != null)
//        //{
//        //    if (_HotspotAudioController.IsPlaying) return;
//        //}
//        KillTweens();
//        HoverBackgroundObject.transform.DOScale(_EndScale, _AnimSpeed).SetEase(Ease.OutBack);
//        HoverIcon.transform.DOScale(_EndingHoverIconScale, _AnimSpeed).SetEase(Ease.OutBack).SetDelay(0.2f);
//        if (IconTxtCanvas != null)
//            IconTxtCanvas.DOFade(1f, 0.3f);
//    }

//    public void AnimOut()
//    {
//        //if we're a hotspotaudio and we're playing, we should not animate
//        //if (_HotspotAudioController != null)
//        //{
//        //    if (_HotspotAudioController.IsPlaying) return;
//        //}
//        KillTweens();
//        HoverIcon.transform.DOScale(_StartingHoverIconScale, _AnimSpeed).SetEase(Ease.InBack);
//        HoverBackgroundObject.transform.DOScale(_StartingScale, _AnimSpeed).SetEase(Ease.InBack).SetDelay(0.2f);
//        if (IconTxtCanvas != null)
//            IconTxtCanvas.DOFade(0, 0.3f);
//    }

//    private void KillTweens()
//    {
//        DOTween.Kill(HoverIcon.transform);
//        DOTween.Kill(HoverBackgroundObject.transform);
//    }

//    public void Hide()
//    {
//        KillTweens();
//        HoverIcon.transform.localScale = _StartingHoverIconScale;
//        HoverBackgroundObject.transform.localScale = _StartingScale;

//        if (IconTxtCanvas != null)
//            IconTxtCanvas.alpha = 0;
//    }

//    public void OnGazedAt(bool isGazedAt)
//    {
//        if (isGazedAt)
//        {
//            AnimIn();
//        }
//        else
//        {
//            AnimOut();
//        }
//    }
//}
