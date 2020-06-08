using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRReticle : MonoBehaviour
{

    public Camera ReticleCamera;
    public LayerMask Mask;
    public RawImage ReticleNormal;
    public RawImage ReticleHover;

    private Transform _CurHit = null;

    private void Awake()
    {
        if (ReticleCamera == null)
            throw new System.Exception("A ReticleCamera must be defined in VRReticle");
        if (ReticleNormal == null)
            throw new System.Exception("A ReticleNormal must be defined in VRReticle");
        if (ReticleHover == null)
            throw new System.Exception("A ReticleHover must be defined in VRReticle");

        AnimOff();

    }

    private void AnimOff(Transform hitObj = null)
    {
        if(hitObj != null)
        {
            ReticleNormal.gameObject.SetActive(true);
            ReticleHover.gameObject.SetActive(false);
        }
    }

    private void AnimOn(Transform hitObj)
    {
        ReticleNormal.gameObject.SetActive(false);
        ReticleHover.gameObject.SetActive(true);

        if(hitObj.GetComponent<VRInteractible>() != null)
        {
            VRInteractible curVRInteractible = hitObj.GetComponent<VRInteractible>();
            curVRInteractible.OnPointerEnter.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = ReticleCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, Mask))
        {
            if(_CurHit != hit.transform)
            {
                _CurHit = hit.transform;
                //here is where we need to trigger an action in the item we rolled over
                Debug.Log("trigger rollover action");
                AnimOn(_CurHit);
            }
        }
        else
        {
            if(_CurHit != null)
            {
                //we're going to have to change how off is called, because you might roll onto something and never hit null
                AnimOff(_CurHit);
                _CurHit = null;
                Debug.Log("trigger rollout action");
            }
        }
    }
}
