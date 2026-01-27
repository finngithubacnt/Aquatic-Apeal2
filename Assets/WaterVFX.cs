using UnityEngine;
using UnityEngine.VFX;
public class WaterVFX : MonoBehaviour
{
    public VisualEffect waterEffect;
    public Rigidbody rb;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody>();
        float speed = rb.linearVelocity.magnitude;
        waterEffect.SetFloat("SpawnRate", speed);
        waterEffect.SetFloat("ConstantSpawnRate", speed);
    }
}
