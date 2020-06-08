using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class VRInteractible : MonoBehaviour
{

    public UnityEvent OnPointerEnter;
    public UnityEvent OnPointerExit;

    public void TestEnter()
    {
        Debug.Log("enter from test enter");
    }

    public void TestExit()
    {
        Debug.Log("exit from test exit");
    }
    
}
