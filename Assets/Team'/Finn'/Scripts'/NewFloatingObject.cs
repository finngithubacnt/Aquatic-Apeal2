using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using Unity.Mathematics;

public class NewFloatingObject : MonoBehaviour
{
    public Rigidbody rb;

    public float depthBefSub;

    public float displacementAmt;

    public int floaters = 1;
    public float waterDrag = 1f;
    public float waterAngularDrag = 1f;
    public WaterSurface water;

    WaterSearchParameters Search;
    WaterSearchResult SearchResult;

    public void Start()
    {
        // Ensure we have a Rigidbody reference. Prefer parent (boat) if floaters are child objects.
        if (rb == null)
            rb = GetComponentInParent<Rigidbody>();

        // Initialize search struct to safe defaults
        Search = new WaterSearchParameters();
        Search.maxIterations = 8;
        Search.error = 0.01f;
        Search.includeDeformation = true;
        Search.excludeSimulation = false;

        if (water == null)
            water = GameObject.FindGameObjectWithTag("Water")?.GetComponent<WaterSurface>();
    }

    private void FixedUpdate()
    {
        if (rb == null || water == null) return;

        // Safety: avoid div-by-zero
        int effectiveFloaters = Mathf.Max(1, floaters);

        // Apply gravity per floater (Acceleration mode for gravity is appropriate)
        rb.AddForceAtPosition(Physics.gravity / effectiveFloaters, transform.position, ForceMode.Acceleration);

        // Prepare search parameters for this sample point
        Search.startPositionWS = (float3)transform.position;
        Search.targetPositionWS = (float3)transform.position;

        if (!water.ProjectPointOnWaterSurface(Search, out SearchResult))
            return;

        // If this point is underwater, apply buoyancy + drag
        if (transform.position.y < SearchResult.projectedPositionWS.y)
        {
            float displacementMulti = Mathf.Clamp01((SearchResult.projectedPositionWS.y - transform.position.y) / depthBefSub) * displacementAmt;

            // Buoyancy upward force (Acceleration)
            rb.AddForceAtPosition(Vector3.up * Mathf.Abs(Physics.gravity.y) * displacementMulti, transform.position, ForceMode.Acceleration);

            // Linear water drag: use VelocityChange and scale by Time.fixedDeltaTime so damping is frame-rate consistent
            rb.AddForce(-rb.linearVelocity * displacementMulti * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);

            // Angular water drag: damp pitch/roll only, preserve yaw so boat steering isn't suppressed.
            Vector3 worldAngVel = rb.angularVelocity;
            // Convert to local to separate yaw
            Vector3 localAngVel = transform.InverseTransformDirection(worldAngVel);

            // Zero Y (local yaw) so water damping doesn't fight steering torque
            localAngVel.y = 0f;

            // Back to world space and apply angular velocity change (VelocityChange uses radians/sec change)
            Vector3 dampAngularWorld = transform.TransformDirection(localAngVel);
            rb.AddTorque(-dampAngularWorld * displacementMulti * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
