using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIHotspotTextController : MonoBehaviour
{
    public GameObject TextHolder;
    public VRInteractible VRInteractibleObj;

    private CanvasGroup _TextHolderCanvasGroup;
    private float _TextHolderStartYPos;
    private float _TextHolderEndYPos;
    private float _AnimSpeed = 0.4f;


    private void Awake()
    {
        if (TextHolder == null)
            throw new System.Exception("TextHolder must be defined in UIHotspotTextController");

        if (VRInteractibleObj == null)
            throw new System.Exception("VRInteractibleObj must be defined in UIHotspotTextController");

        _TextHolderCanvasGroup = TextHolder.GetComponent<CanvasGroup>();
        if (_TextHolderCanvasGroup == null)
            throw new System.Exception("_TextHolderCanvasGroup must be defined in HotspotText");

        _TextHolderEndYPos = TextHolder.gameObject.transform.localPosition.y;
        _TextHolderStartYPos = _TextHolderEndYPos - 15.0f;

        AnimOut();
    }

    private void AnimOut()
    {
        TextHolder.SetActive(false);
    }

    private void AnimIn()
    {
        _TextHolderCanvasGroup.alpha = 0;
        TextHolder.SetActive(true);

        //if (ReferenceHotspot.GetComponent<InteractibleHotspotHover>() != null)
        //    ReferenceHotspot.GetComponent<InteractibleHotspotHover>().Hide();

        //ReferenceHotspot.SetActive(false);

        VRInteractibleObj.gameObject.SetActive(false);

        TextHolder.gameObject.transform.localPosition = new Vector3(TextHolder.gameObject.transform.localPosition.x, _TextHolderStartYPos, TextHolder.gameObject.transform.localPosition.z);
        TextHolder.transform.DOLocalMoveY(_TextHolderEndYPos, _AnimSpeed).SetEase(Ease.OutQuad);
        _TextHolderCanvasGroup.DOFade(1f, _AnimSpeed);
    }

    public void OnHotspotClick()
    {
        AnimIn();
    }

    public void OnHotspotClose()
    {
        AnimOut();
    }
}


//public GameObject TextHolder;
//public TextMeshProUGUI LiveTxt;
//public TextMeshProUGUI PaginationTxt;
//public GameObject ReferenceHotspot;
//public CanvasGroup TxtCanvasGroup;
//public GameObject BtnLeft;
//public GameObject BtnRight;
//public List<string> TextCarouselList;

//private int _CurText = 0;
//private int _TextCarouselLength;
//private bool _IsOpen = false;
//private float _TextStartYPos;
//private float _TextEndYPos;
//private float _AnimSpeed = 0.4f;
//private CanvasGroup _TextHolderCanvasGroup;
//private float _TextHolderStartYPos;
//private float _TextHolderEndYPos;

//// Start is called before the first frame update
//private void Awake()
//{
//    if (LiveTxt == null)
//        throw new System.Exception("LiveTxt must be defined in HotspotText");

//    if (TextCarouselList == null || TextCarouselList.Count == 0)
//        throw new System.Exception("TextCarouselList must be defined in HotspotText");

//    if (TextHolder == null)
//        throw new System.Exception("TextHolder must be defined in HotspotText");

//    if (PaginationTxt == null)
//        throw new System.Exception("PaginationTxt must be defined in HotspotText");

//    if (ReferenceHotspot == null)
//        throw new System.Exception("ReferenceHotspot must be defined in HotspotText");

//    if (TxtCanvasGroup == null)
//        throw new System.Exception("TxtCanvasGroup must be defined in HotspotText");

//    if (BtnLeft == null)
//        throw new System.Exception("BtnLeft must be defined in ScreenshotCarousel");

//    if (BtnRight == null)
//        throw new System.Exception("BtnRight must be defined in ScreenshotCarousel");


//    _TextCarouselLength = TextCarouselList.Count;
//    _IsOpen = false;
//    _TextEndYPos = LiveTxt.gameObject.transform.localPosition.y;
//    _TextStartYPos = _TextEndYPos - 15.0f;
//    _TextHolderEndYPos = TextHolder.gameObject.transform.localPosition.y;
//    _TextHolderStartYPos = _TextHolderEndYPos - 15.0f;
//    TxtCanvasGroup.alpha = 0;
//    _TextHolderCanvasGroup = TextHolder.GetComponent<CanvasGroup>();
//    //hide left and right if we don't have more than a single shot
//    if (_TextCarouselLength < 2)
//    {
//        BtnLeft.SetActive(false);
//        BtnRight.SetActive(false);
//    }

//    if (_TextHolderCanvasGroup == null)
//        throw new System.Exception("_TextHolderCanvasGroup must be defined in HotspotText");

//    SetSlide();
//    AnimOut();
//}

//private void AnimOut()
//{
//    TextHolder.SetActive(false);
//    if (_IsOpen)
//    {
//        _IsOpen = false;
//        ReferenceHotspot.SetActive(true);
//    }
//}

//private void AnimIn()
//{
//    _TextHolderCanvasGroup.alpha = 0;
//    TextHolder.SetActive(true);
//    _IsOpen = true;

//    if (ReferenceHotspot.GetComponent<InteractibleHotspotHover>() != null)
//        ReferenceHotspot.GetComponent<InteractibleHotspotHover>().Hide();

//    ReferenceHotspot.SetActive(false);

//    TextHolder.gameObject.transform.localPosition = new Vector3(TxtCanvasGroup.gameObject.transform.localPosition.x, _TextHolderStartYPos, TxtCanvasGroup.gameObject.transform.localPosition.z);
//    TextHolder.transform.DOLocalMoveY(_TextHolderEndYPos, _AnimSpeed).SetEase(Ease.OutQuad);
//    _TextHolderCanvasGroup.DOFade(1f, _AnimSpeed);
//}

//private void SetSlide()
//{
//    EventManager.Instance.ScreenClick();
//    LiveTxt.text = TextCarouselList[_CurText];
//    PaginationTxt.text = _CurText + 1 + " of " + _TextCarouselLength;
//    DOTween.Kill(LiveTxt.gameObject.transform.localPosition);
//    TxtCanvasGroup.alpha = 0;
//    LiveTxt.gameObject.transform.localPosition = new Vector3(LiveTxt.gameObject.transform.localPosition.x, _TextStartYPos, LiveTxt.gameObject.transform.localPosition.z);
//    LiveTxt.transform.DOLocalMoveY(_TextEndYPos, _AnimSpeed).SetEase(Ease.OutQuad);
//    TxtCanvasGroup.DOFade(1f, _AnimSpeed);
//}

//public void OnLeftClick()
//{
//    _CurText -= 1;
//    if (_CurText < 0)
//        _CurText = _TextCarouselLength - 1;
//    SetSlide();
//}

//public void OnRightClick()
//{
//    _CurText += 1;
//    if (_CurText > _TextCarouselLength - 1)
//        _CurText = 0;
//    SetSlide();
//}

//public void OnHotspotClick()
//{
//    AnimIn();
//}

//public void OnHotspotClose()
//{
//    AnimOut();
//}