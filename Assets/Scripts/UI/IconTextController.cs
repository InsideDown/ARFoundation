using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconTextController : MonoBehaviour
{
    //TODO: need to rewrite this - just for testing
    public GameObject IconObject;
    public GameObject TextObject;

    private void Awake()
    {
        if (IconObject == null)
            throw new System.Exception("An IconObject does not exist in IconTextController");

        if (TextObject == null)
            throw new System.Exception("A TextObject does not exist in IconTextController");

        OnShowIconObject();
    }

    public void OnShowIconObject()
    {
        IconObject.SetActive(true);
        TextObject.SetActive(false);
    }

    public void OnShowTextObject()
    {
        IconObject.SetActive(false);
        TextObject.SetActive(true);
    }
}
