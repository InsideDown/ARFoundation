using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimBars : MonoBehaviour
{
    public float AnimSpeed = 0.3f;

    private void Awake()
    {
        foreach (Transform bar in this.gameObject.transform)
        {
            GameObject barObject = bar.gameObject;
            barObject.transform.localScale = new Vector3(0, 1f, 1f);
        }
    }

    private void Start()
    {
        foreach (Transform bar in this.gameObject.transform)
        {
            GameObject barObject = bar.gameObject;
            AnimIn(barObject);
        }
    }


    private void AnimIn(GameObject barObject = null)
    {
        if (barObject != null)
        {
            float ranSeconds = Random.Range(0, 1f);
            barObject.transform.DOScale(1, AnimSpeed).SetEase(Ease.OutBack).SetDelay(ranSeconds);
         

            //HoverIcon.transform.DOScale(_EndingHoverIconScale, _AnimSpeed).SetEase(Ease.OutBack).SetDelay(0.2f);

            //AlphaMaterial.DOFade(0, 1).OnComplete(() => AlphaGameObject.SetActive(false));
        }
    }
}
