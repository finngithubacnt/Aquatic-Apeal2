using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
public class NewFloatingObject : MonoBehaviour
{
    public float Rigidbody rb;

    public float depthBefSub;

    public float displacementAmt;

    public int Floaters;
    public float waterDrag;
    public float waterAngularDrag;
    public WaterSurface water;

    WaterSearchParameters Search;

    WaterSearchResult SearchResult;

    private Void FixedUpdate()
    {


        rb.AddForceAtPosition(Physi)
    }
}
