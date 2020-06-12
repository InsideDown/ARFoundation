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
        VRInteractibleObj.gameObject.SetActive(true);
    }

    private void AnimIn()
    {
        _TextHolderCanvasGroup.alpha = 0;
        TextHolder.SetActive(true);

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