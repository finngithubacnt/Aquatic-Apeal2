using UnityEngine;
using UnityEngine.VFX;
public class WaterVFX : MonoBehaviour
{
    public VisualEffect waterEffect;
    public Rigidbody rb;
    public float speed;
    public boatcontoller boatcontoller;
    public bool effects;
    public bool idleEffects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boatcontoller = GetComponent<boatcontoller>();
        rb = GetComponent<Rigidbody>();
        float speed = rb.linearVelocity.magnitude;
        waterEffect.SetFloat("SpawnRate", speed);
        waterEffect.SetFloat("ConstantSpawnRate", speed);
        boatcontoller.effects = effects;
        boatcontoller.idleEffects = idleEffects;
    }
}
