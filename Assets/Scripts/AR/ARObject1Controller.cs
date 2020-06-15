using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObject1Controller : MonoBehaviour
{
   public void OnHotspotClick()
    {
        EventManager.Instance.UIScreenOpenRequest();
    }
}
