using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class VRInteractible : MonoBehaviour
{

    public UnityEvent OnPointerEnter;
    public UnityEvent OnPointerExit;
    public UnityEvent OnPointerClick;

    protected virtual void Awake()
    {
        if (this.gameObject.GetComponent<Collider>() == null)
        {
            throw new System.Exception("A collider must be attached to an VRInteractible");
        }
    }

    public void TestEnter()
    {
        Debug.Log("enter from test enter");
    }

    public void TestExit()
    {
        Debug.Log("exit from test exit");
    }

    public void TestClick()
    {
        Debug.Log("triggered click");
    }

}
