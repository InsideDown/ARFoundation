using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AR3DTouchManager : MonoBehaviour
{
    public string ItemClickedStr;
    public UnityEvent MethodToInvoke;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && this.gameObject == hit.transform.gameObject)
                {
                    Debug.Log("you clicked: " + ItemClickedStr);
                    if (MethodToInvoke != null)
                    {
                        Debug.Log("invoking method");
                        MethodToInvoke.Invoke();
                    }
                    else
                    {
                        Debug.LogWarning("You clicked " + this.gameObject.name + "and its AR3DTouchManager component has no MethodToInvoke set");
                    }
                }
            }
        }
    }
}
