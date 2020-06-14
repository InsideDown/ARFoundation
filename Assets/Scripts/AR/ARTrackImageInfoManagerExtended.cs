using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ARTrackImageInfoManagerExtended : MonoBehaviour
{
    private ARTrackedImageManager m_TrackedImageManager;

    private void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += M_TrackedImageManager_trackedImagesChanged;
    }

    private void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged += M_TrackedImageManager_trackedImagesChanged;
    }

    private void M_TrackedImageManager_trackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //DebugTxt.Instance.SetTxt("---\n" + eventArgs. + Random.Range(0, 1000).ToString());

        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            //DebugTxt.Instance.SetTxt("---\n" + trackedImage.referenceImage.name + Random.Range(0, 1000).ToString());
            trackedImage.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            //DebugTxt.Instance.AddTxt(trackedImage.referenceImage.name + Random.Range(0, 1000).ToString());
        }
    }
}
