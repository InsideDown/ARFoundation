using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VRReticle : MonoBehaviour
{

    public Camera ReticleCamera;
    public LayerMask Mask;
    public RawImage ReticleNormal;
    public RawImage ReticleHover;
    public float AnimScaleSpeed = 0.4f;

    private Transform _CurHit = null;
    private Vector3 _MinReticleScale = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 _MaxNormalReticleScale = Vector3.one;
    private Vector3 _MaxHoverReticleScale = new Vector3(1.4f, 1.4f, 1.4f);
    private bool _IsReticleEnabled = true;

    private void Awake()
    {
        if (ReticleCamera == null)
            throw new System.Exception("A ReticleCamera must be defined in VRReticle");
        if (ReticleNormal == null)
            throw new System.Exception("A ReticleNormal must be defined in VRReticle");
        if (ReticleHover == null)
            throw new System.Exception("A ReticleHover must be defined in VRReticle");

        ReticleNormal.gameObject.SetActive(true);
        ReticleHover.gameObject.SetActive(false);
    }

    private void AnimOff(Transform hitObj = null)
    {
        if(hitObj != null)
        {
            KillTweens();
            ReticleNormal.gameObject.SetActive(true);

            ReticleNormal.transform.localScale = Vector3.zero;
            ReticleNormal.transform.DOScale(_MaxNormalReticleScale, AnimScaleSpeed).SetEase(Ease.OutBack).SetDelay(AnimScaleSpeed/2);

            ReticleHover.transform.DOScale(_MinReticleScale, AnimScaleSpeed).SetEase(Ease.InBack).OnComplete(() => ReticleHover.gameObject.SetActive(false)); ;
        }
    }

    private void AnimOn(Transform hitObj)
    {
        KillTweens();
        ReticleNormal.gameObject.SetActive(false);
        ReticleHover.gameObject.SetActive(true);

        ReticleHover.transform.localScale = _MinReticleScale;
        ReticleHover.transform.DOScale(_MaxHoverReticleScale, AnimScaleSpeed).SetEase(Ease.OutBack);


        //check to make sure we're an interactible and trigger pointer stuff
        if (hitObj.GetComponent<VRInteractible>() != null)
        {
            VRInteractible curVRInteractible = hitObj.GetComponent<VRInteractible>();
            curVRInteractible.OnPointerEnter.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsReticleEnabled)
        {
            Ray ray = ReticleCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500, Mask))
            {
                if (_CurHit != hit.transform)
                {
                    _CurHit = hit.transform;
                    AnimOn(_CurHit);
                }
            }
            else
            {
                if (_CurHit != null)
                {
                    //TODO: we're going to have to change how off is called, because you might roll onto something and never hit null
                    AnimOff(_CurHit);
                    if (_CurHit.GetComponent<VRInteractible>() != null)
                    {
                        VRInteractible curVRInteractible = _CurHit.GetComponent<VRInteractible>();
                        curVRInteractible.OnPointerExit.Invoke();
                    }
                    _CurHit = null;
                }
            }

            //listen for clicks
            if (Input.GetMouseButtonDown(0))
            {
                if (_CurHit != null)
                {
                    if (_CurHit.GetComponent<VRInteractible>() != null)
                    {
                        VRInteractible curVRInteractible = _CurHit.GetComponent<VRInteractible>();
                        curVRInteractible.OnPointerClick.Invoke();
                    }
                }
            }
        }
    }

    private void EnableReticle()
    {
        _IsReticleEnabled = true;
    }

    private void DisableReticle()
    {
        _IsReticleEnabled = false;
    }

    //****************
    // EVENTS
    //****************

    private void OnEnable()
    {
        EventManager.OnUIScreenOpen += EventManager_OnUIScreenOpen;
        EventManager.OnUIScreenCloseComplete += EventManager_OnUIScreenCloseComplete;
        EventManager.OnLoadNewScene += EventManager_OnLoadNewScene;
    }

    private void OnDisable()
    {
        EventManager.OnUIScreenOpen -= EventManager_OnUIScreenOpen;
        EventManager.OnUIScreenCloseComplete -= EventManager_OnUIScreenCloseComplete;
        EventManager.OnLoadNewScene -= EventManager_OnLoadNewScene;
    }

    private void EventManager_OnLoadNewScene(string newScene)
    {
        DisableReticle();
    }

    private void EventManager_OnUIScreenOpen(string newScene)
    {
        DisableReticle();
    }

    private void EventManager_OnUIScreenCloseComplete(string newScene)
    {
        EnableReticle();
    }

    private void KillTweens()
    {
        DOTween.Kill(ReticleNormal);
        DOTween.Kill(ReticleHover);
    }

    private void OnDestroy()
    {
        KillTweens();
    }
}
