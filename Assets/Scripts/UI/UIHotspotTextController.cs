using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHotspotTextController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


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
