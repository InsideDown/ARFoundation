using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaunchScreenController : MonoBehaviour
{
    public CanvasGroup BarsCanvasGroup;
    public CanvasGroup ArtsLogoCanvas;
    public CanvasGroup BtnARTourCanvas;
    public CanvasGroup BtnVRTourCanvas;
    public float AnimDelay = 0.2f;
    public string ARSceneName;
    public string VRSceneName;

    private Button _BtnARTour;
    private Button _BtnVRTour;
    private string _SceneStr;

    private void Awake()
    {
        if (BarsCanvasGroup == null)
            throw new System.Exception("An BarsCanvasGroup is required in LaunchSceneController");

        if (ArtsLogoCanvas == null)
            throw new System.Exception("An ArtsLogoCanvas is required in LaunchSceneController");

        if (BtnARTourCanvas == null)
            throw new System.Exception("A BtnARTourCanvas is required in LaunchSceneController");

        if (BtnVRTourCanvas == null)
            throw new System.Exception("A BtnVRTourCanvas is required in LaunchSceneController");

        _BtnARTour = BtnARTourCanvas.GetComponent<Button>();
        _BtnVRTour = BtnVRTourCanvas.GetComponent<Button>();

        if (_BtnARTour == null || _BtnVRTour == null)
            throw new System.Exception("BtnARTour or BtnVRTour is not a component in LaunchSceneController");

        BarsCanvasGroup.alpha = 0;
        ArtsLogoCanvas.alpha = 0;
        BtnARTourCanvas.alpha = 0;
        BtnVRTourCanvas.alpha = 0;
    }

    private void Start()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        yield return new WaitForSeconds(AnimDelay);
        AnimIn();
    }

    private void AnimIn()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(BarsCanvasGroup.DOFade(1, 0.4f));
        mySequence.Insert(0.3f,ArtsLogoCanvas.DOFade(1, 0.4f));
        mySequence.Insert(0.6f, BtnARTourCanvas.DOFade(1, 0.4f));
        mySequence.Insert(0.9f, BtnVRTourCanvas.DOFade(1, 0.4f));
    }

    private void AnimOut()
    {
        //disable our buttons
        _BtnARTour.enabled = _BtnVRTour.enabled = false;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(BarsCanvasGroup.DOFade(0, 0.4f));
        mySequence.Insert(0.2f, ArtsLogoCanvas.DOFade(0, 0.4f));
        mySequence.Insert(0.4f, BtnARTourCanvas.DOFade(0, 0.4f));
        mySequence.Insert(0.6f, BtnVRTourCanvas.DOFade(0, 0.4f));
        mySequence.OnComplete(() => { StartCoroutine(LoadSceneAsync()); });
    }

    public void OnARTourClick()
    {
        Debug.Log("AR Tour Click");
        _SceneStr = ARSceneName;
        AnimOut();
    }

    public void OnVRTourClick()
    {
        Debug.Log("VR Tour Click");
        _SceneStr = VRSceneName;
        AnimOut();
    }

    IEnumerator LoadSceneAsync()
    {
        if (string.IsNullOrEmpty(_SceneStr))
            throw new System.Exception("A _SceneStr is not defined in LaunchSceneController");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_SceneStr);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class PreLaunchSceneController : MonoBehaviour
{
    public CanvasGroup IUHealthLogoCanvas;
    public CanvasGroup BtnHaveCardboardCanvas;
    public CanvasGroup BtnNoCardboardCanvas;
    public CanvasGroup TxtGameDescription;

    public string CardboardScene;
    public string NoCardboardScene;

    private bool _IsSet = false;

    private void Awake()
    {
        XRSettings.enabled = false;

        if (IUHealthLogoCanvas == null)
            throw new System.Exception("An IUHealthLogoCanvas is required in PreLaunchSceneController");

        if (BtnHaveCardboardCanvas == null)
            throw new System.Exception("A BtnHaveCardboardCanvas is required in PreLaunchSceneController");

        if (BtnNoCardboardCanvas == null)
            throw new System.Exception("A BtnNoCardboardCanvas is required in PreLaunchSceneController");

        if (CardboardScene == null || CardboardScene == "")
            throw new System.Exception("A CardboardScene is required in PreLaunchSceneController");

        if (NoCardboardScene == null || NoCardboardScene == "")
            throw new System.Exception("A NoCardboardScene is required in PreLaunchSceneController");

        if (TxtGameDescription == null)
            throw new System.Exception("A TxtGameDescription is required in PreLaunchSceneController");

        IUHealthLogoCanvas.alpha = 0;
        BtnHaveCardboardCanvas.alpha = 0;
        BtnNoCardboardCanvas.alpha = 0;
        TxtGameDescription.alpha = 0;

        StartCoroutine(Init());
    }

    // Call via `StartCoroutine(SwitchTo2D())` from your code. Or, use
    // `yield SwitchTo2D()` if calling from inside another coroutine.
    IEnumerator SwitchTo2D()
    {
        // Empty string loads the "None" device.
        XRSettings.LoadDeviceByName("");

        // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
        yield return null;

        // Not needed, since loading the None (`""`) device takes care of this.
        // XRSettings.enabled = false;

        // Restore 2D camera settings.
        ResetCameras();
        StartCoroutine(AnimOutNoCardboard());
    }

    // Resets camera transform and settings on all enabled eye cameras.
    void ResetCameras()
    {
        // Camera looping logic copied from GvrEditorEmulator.cs
        for (int i = 0; i < Camera.allCameras.Length; i++)
        {
            Camera cam = Camera.allCameras[i];
            if (cam.enabled && cam.stereoTargetEye != StereoTargetEyeMask.None)
            {

                // Reset local position.
                // Only required if you change the camera's local position while in 2D mode.
                cam.transform.localPosition = Vector3.zero;

                // Reset local rotation.
                // Only required if you change the camera's local rotation while in 2D mode.
                cam.transform.localRotation = Quaternion.identity;

                // No longer needed, see issue github.com/googlevr/gvr-unity-sdk/issues/628.
                // cam.ResetAspect();

                // No need to reset `fieldOfView`, since it's reset automatically.
            }
        }
    }



    private IEnumerator Init()
    {
        yield return new WaitForSeconds(0.5f);
        AnimIn();
    }

    private void AnimIn()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(IUHealthLogoCanvas.DOFade(1, 0.4f));
        mySequence.Insert(0.3f, BtnHaveCardboardCanvas.DOFade(1, 0.4f));
        mySequence.Insert(0.6f, BtnNoCardboardCanvas.DOFade(1, 0.4f));
    }

    public void OnGoogleCardboardClick()
    {
        GlobalVars.Instance.IsCardboard = true;

        if (!_IsSet)
        {
            _IsSet = true;
            AnimTextIn(true);
        }
        //StartCoroutine(AnimOutCardboard());
    }

    public void OnNoCardboardClick()
    {
        GlobalVars.Instance.IsCardboard = false;
        if (!_IsSet)
        {
            _IsSet = true;
            AnimTextIn(false);
        }
        //StartCoroutine(SwitchTo2D());
    }

    private void AnimTextIn(bool isCardboard)
    {
        Sequence textSequence = DOTween.Sequence();
        textSequence.Append(BtnHaveCardboardCanvas.DOFade(0, 0.6f));
        textSequence.Insert(0.5f, BtnNoCardboardCanvas.DOFade(0, 0.6f));
        textSequence.Insert(1.5f, TxtGameDescription.DOFade(1, 0.6f));
        textSequence.AppendInterval(5.0f);
        textSequence.OnComplete(() => { StartCoroutine(AnimOut(isCardboard)); });
    }

    private IEnumerator AnimOut(bool isCardboard)
    {
        yield return new WaitForSeconds(0.2f);
        if (isCardboard)
        {
            StartCoroutine(LoadSceneAsync(CardboardScene));
        }
        else
        {
            StartCoroutine(SwitchTo2D());
            //StartCoroutine(LoadSceneAsync(NoCardboardScene));
        }
    }

    private IEnumerator AnimOutNoCardboard()
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(LoadSceneAsync(NoCardboardScene));
    }

    IEnumerator LoadSceneAsync(string newScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newScene);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}*/

