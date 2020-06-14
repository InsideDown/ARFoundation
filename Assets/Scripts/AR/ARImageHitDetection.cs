using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARImageHitDetection : MonoBehaviour
{
    public MeshRenderer IndicatorMeshRenderer;
    public Material WhiteIndicatorMaterial;
    public Material GreenIndicatorMaterial;

    private void Awake()
    {
        if (IndicatorMeshRenderer == null)
            throw new System.Exception("A IndicatorMeshRenderer must be set in ARImageHitDetection");

        if (WhiteIndicatorMaterial == null)
            throw new System.Exception("A WhiteIndicatorMaterial must be set in ARImageHitDetection");

        if (GreenIndicatorMaterial == null)
            throw new System.Exception("A GreenIndicatorMaterial must be set in ARImageHitDetection");

        IndicatorMeshRenderer.material = WhiteIndicatorMaterial;
    }

    private void OnCollisionEnter(Collision collideObj)
    {
        if(collideObj.collider.tag == "PlacementIndicator")
        {
            Debug.Log("colliding with " + collideObj.collider.name);
            IndicatorMeshRenderer.material = GreenIndicatorMaterial;
        }
    }

    private void OnCollisionExit(Collision collideObj)
    {
        if (collideObj.collider.tag == "PlacementIndicator")
        {
            Debug.Log("exiting collision with " + collideObj.collider.name);
            IndicatorMeshRenderer.material = WhiteIndicatorMaterial;
        }
    }
}
