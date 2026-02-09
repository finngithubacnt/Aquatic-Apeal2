using UnityEngine;
using System.Collections;

public class rotationDampener : MonoBehaviour
{
    [Header("Settings")]
    public float rotationDampening = 0.1f;
    public float maxRotationAnglex = 45f;
    public float maxRotationAngley = 45f;
    public float maxRotationAnglez = 45f;

    [Header("Current Rotation Values")]
    public float xValue;
    public float yValue;
    public float zValue;

    [Header("References")]
    public Rigidbody Rb;
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }


    void Update()
    {

        yValue = Rb.transform.rotation.y;
        xValue = Rb.transform.rotation.x;
        zValue = Rb.transform.rotation.z;

       

        if (Mathf.Abs(xValue) > maxRotationAnglex)
        {
           Rb.transform.rotation = Quaternion.Euler(Mathf.Clamp(xValue, -maxRotationAnglex, maxRotationAnglex), yValue, zValue);        
        }

        if (Mathf.Abs(zValue) > maxRotationAnglex)
        {
            Rb.transform.rotation = Quaternion.Euler(xValue, yValue, Mathf.Clamp(zValue, -maxRotationAnglez, maxRotationAnglez));   
        }

    }
}
