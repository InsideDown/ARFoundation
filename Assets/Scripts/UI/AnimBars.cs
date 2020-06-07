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


    private void AnimIn(GameObject barObject = null, float scaleSize = 1.0f)
    {
        if (barObject != null)
        {
            float ranSeconds = Random.Range(1f, 2f);
            float newScaleSize = Random.Range(0.4f, 0.8f);

            if (scaleSize != 1.0f)
            {
                scaleSize = newScaleSize;
                newScaleSize = 1.0f;
            }
            else
            {
                scaleSize = 1.0f;
            }

            barObject.transform.DOScaleX(scaleSize, AnimSpeed).SetEase(Ease.OutBack).SetDelay(ranSeconds).OnComplete(() => AnimIn(barObject,newScaleSize));
        }
    }
}
