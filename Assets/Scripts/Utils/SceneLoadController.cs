﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadController : MonoBehaviour
{
	public GameObject AlphaGameObject;

	private Material AlphaMaterial;

    private void Awake()
	{
		if (AlphaGameObject == null)
			throw new System.Exception("An AlphaGameObject must be defined in IntroScreenController");

        AlphaGameObject.SetActive(true);
	}

    private IEnumerator Start()
	{
		AlphaMaterial = AlphaGameObject.GetComponent<Renderer>().material;
        yield return new WaitForSeconds(0.2f);
        FadeSceneIn();
	}

    private void FadeSceneIn()
	{
		AlphaMaterial.DOFade(0, 1).OnComplete(()=> AlphaGameObject.SetActive(false));
	}

    private void FadeSceneOut(string newScene)
    {
        AlphaGameObject.SetActive(true);
        AlphaMaterial.DOFade(1, 1).OnComplete(()=> LoadNewScene(newScene));
    }

    private void LoadNewScene(string newScene)
    {
        StartCoroutine(LoadSceneAsync(newScene));
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
        FadeSceneOut(newScene);
    }
}
