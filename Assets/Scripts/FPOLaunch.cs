using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FPOLaunch : MonoBehaviour
{
    public CanvasGroup ScrollRectCanvasGroup;

    private void Awake()
    {
        ScrollRectCanvasGroup.alpha = 0.0f;
    }

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
        EventManager.Instance.ShowLoader();
        yield return new WaitForSeconds(5.0f);
        EventManager.Instance.HideLoader();
        ScrollRectCanvasGroup.DOFade(1, 0.4f).SetDelay(0.6f);
    }
}
