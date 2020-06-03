using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceUIController : MonoBehaviour
{

    public Animator MoveDeviceAnimation;
    public Animator TapToPlaceAnimation;

    const string k_FadeOffAnim = "FadeOff";
    const string k_FadeOnAnim = "FadeOn";

    public void Awake()
    {
        if (MoveDeviceAnimation == null)
            throw new System.Exception("A MoveDeviceAnimation must be set in ScreenSpaceUIController");

        if (TapToPlaceAnimation == null)
            throw new System.Exception("A TapToPlaceAnimation must be set in ScreenSpaceUIController");
    }


    private void HideMoveShowPlace()
    {
        if (MoveDeviceAnimation)
            MoveDeviceAnimation.SetTrigger(k_FadeOffAnim);

        if (TapToPlaceAnimation)
            TapToPlaceAnimation.SetTrigger(k_FadeOnAnim);
    }

    /// <summary>
    /// EVENTS
    /// </summary>

    private void OnEnable()
    {
        EventManager.OnARPlacementAllowed += EventManager_OnARPlacementAllowed;
    }

    private void OnDisable()
    {
        EventManager.OnARPlacementAllowed -= EventManager_OnARPlacementAllowed;
    }

    private void EventManager_OnARPlacementAllowed()
    {
        HideMoveShowPlace();
    }
}
