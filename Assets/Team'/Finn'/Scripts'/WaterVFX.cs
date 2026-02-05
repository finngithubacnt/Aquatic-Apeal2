using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.VFX;
public class WaterVFX : MonoBehaviour
{
    [Header("References")]
    public boatcontoller boatcontoller;

    public Rigidbody rb;
    public GameObject controller;

    public ParticleSystem Particleeffects;
    public ParticleSystem.EmissionModule emissionModule;
    public VisualEffect waterEffect;

    [Header("Settings")]
    public float speed;
    public float rate = 0f;

    public bool effects1;
    public bool idleEffects1;
    public bool Delay = false;
    void Update()
    {
        boatcontoller = controller.GetComponent<boatcontoller>();
        rb = controller.GetComponent<Rigidbody>();
        float speed = rb.linearVelocity.magnitude;
        effects1 = boatcontoller.effects;
        idleEffects1 = boatcontoller.idleEffects;
        waterEffect.SetFloat("SpawnRate", speed * 10);
        waterEffect.SetFloat("ConstantSpawnRate", speed * 10);

        if (effects1 == true)
        {
            emissionModule = Particleeffects.emission;
            emissionModule.rateOverTime = speed * 10f; 
        }
        else if (effects1 == false && Delay == false)
        {
              
            emissionModule.rateOverTime =- rate; 
            StartCoroutine(DelayedAction(0.02f));
            Delay = true;
        }
 
    }
    IEnumerator DelayedAction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Delay = false;
    }
}
