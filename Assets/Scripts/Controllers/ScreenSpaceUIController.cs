using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceUIController : MonoBehaviour
{

    public Animator MoveDeviceAnimation;
    public Animator TapToPlaceAnimation;
    public GameObject MoveDeviceHolder;

    const string k_FadeOffAnim = "FadeOff";
    const string k_FadeOnAnim = "FadeOn";

    public void Awake()
    {
        if (MoveDeviceAnimation == null)
            throw new System.Exception("A MoveDeviceAnimation must be set in ScreenSpaceUIController");

        if (TapToPlaceAnimation == null)
            throw new System.Exception("A TapToPlaceAnimation must be set in ScreenSpaceUIController");

        if (MoveDeviceHolder == null)
            throw new System.Exception("A MoveDeviceHolder must be set in ScreenSpaceUIController");
    }


    private void HideMoveShowPlace()
    {
        if (MoveDeviceAnimation)
            MoveDeviceAnimation.SetTrigger(k_FadeOffAnim);

        if (TapToPlaceAnimation)
            TapToPlaceAnimation.SetTrigger(k_FadeOnAnim);
    }

    private void HideTapToPlace()
    {
        if (TapToPlaceAnimation)
            TapToPlaceAnimation.SetTrigger(k_FadeOffAnim);

        MoveDeviceHolder.SetActive(false);
    }

    /// <summary>
    /// EVENTS
    /// </summary>

    private void OnEnable()
    {
        EventManager.OnARPlacementAllowed += EventManager_OnARPlacementAllowed;
        EventManager.OnARObjectPlaced += EventManager_OnARObjectPlaced;
    }

    private void OnDisable()
    {
        EventManager.OnARPlacementAllowed -= EventManager_OnARPlacementAllowed;
        EventManager.OnARObjectPlaced -= EventManager_OnARObjectPlaced;
    }

    private void EventManager_OnARPlacementAllowed()
    {
        HideMoveShowPlace();
    }

    private void EventManager_OnARObjectPlaced()
    {
        HideTapToPlace();
    }
}
