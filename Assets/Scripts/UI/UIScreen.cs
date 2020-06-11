using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour
{

    public void OnCloseClick()
    {
        EventManager.Instance.UIScreenClose();
    }

    //remove ourselves from our parent, any other cleanup we need to do
    public void UnInit()
    {
        Destroy(this);
    }
}
