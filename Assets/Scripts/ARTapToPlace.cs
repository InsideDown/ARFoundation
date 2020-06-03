using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;

public class ARTapToPlace : MonoBehaviour
{
    public GameObject PlacementIndicator;
    public Camera ARCamera;
    public GameObject ObjectToPlacePrefab;
    public GameObject PlacedObjectHolder;

    private ARSessionOrigin _AROrigin;
    private Pose _PlacementPose;
    private bool _PlacementPoseIsValid;
    //have we let AR know we can place yet?
    private bool _ARAllowPlace = false;
    private bool _ARObjectPlaced = false;

    private void Awake()
    {
        if (PlacementIndicator == null)
            throw new System.Exception("A PlacementIndicator must be set in ARTapToPlace");

        if (ARCamera == null)
            throw new System.Exception("An ARCamera must be set in ARTapToPlace");

        if (ObjectToPlacePrefab == null)
            throw new System.Exception("An ObjectToPlacePrefab must be set in ARTapToPlace");

        if (PlacedObjectHolder == null)
            throw new System.Exception("An PlacedObjectHolder must be set in ARTapToPlace");

        _AROrigin = FindObjectOfType<ARSessionOrigin>();
    }

    private void Update()
    {
        //as soon as we've placed an object, stop tracking any of this
        if (!_ARObjectPlaced)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();

            if (_PlacementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                PlaceObject();
        }
    }

    private void PlaceObject()
    {
        EventManager.Instance.ARObjectPlaced();
        Instantiate(ObjectToPlacePrefab, _PlacementPose.position, _PlacementPose.rotation, PlacedObjectHolder.transform);
        DisablePlacementIndicator();
        _ARObjectPlaced = true;
    }

    private void TrackPlacementIndicator()
    {
        PlacementIndicator.SetActive(true);
        PlacementIndicator.transform.SetPositionAndRotation(_PlacementPose.position, _PlacementPose.rotation);
    }

    private void DisablePlacementIndicator()
    {
        PlacementIndicator.SetActive(false);
    }

    private void UpdatePlacementIndicator()
    {
        if(_PlacementPoseIsValid)
            TrackPlacementIndicator();
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = ARCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        _AROrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        _PlacementPoseIsValid = hits.Count > 0;
        if(_PlacementPoseIsValid && !_ARAllowPlace)
        {
            _ARAllowPlace = true;
            EventManager.Instance.ARPlacementAllowed();
        }
        
        if (_PlacementPoseIsValid)
        {
            _PlacementPose = hits[0].pose;
            var cameraForward = ARCamera.transform.forward;
            Vector3 cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            _PlacementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UnInit()
    {
        foreach (Transform child in PlacedObjectHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// EVENTS
    /// </summary>

    private void OnEnable()
    {
        EventManager.OnLoadNewScene += EventManager_OnLoadNewScene;
    }

    private void OnDisable()
    {
        EventManager.OnLoadNewScene -= EventManager_OnLoadNewScene;
    }

    private void EventManager_OnLoadNewScene(string newScene)
    {
        UnInit();
    }
}
