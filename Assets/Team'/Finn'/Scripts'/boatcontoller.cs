using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;


public class boatcontoller : MonoBehaviour
{
    public float speed = 10f;
    public float backSpeed = 4f;
    public float rotationSpeed = 100f;
    public float speedLimit = 20f;

    // if you want to explicitly assign the Rigidbody in the inspector you can,
    // otherwise Awake will search on this GameObject, its children and parents.
    public Rigidbody rb;
    

    private float throttle;
    private float steering;
    public float speedcurrent;
    public float neededY;
    public string direction = "forward";
    public worldtolocal worldtolocal;
    private void Awake()
    {
        // Respect an inspector-assigned Rigidbody first
        if (rb != null)
            return;

        // Try to find a Rigidbody on same GameObject
        rb = GetComponent<Rigidbody>();

        // If none, try children (useful if floating-child objects carry the Rigidbody)
        if (rb == null)
            rb = GetComponentInChildren<Rigidbody>();

        // If still none, fall back to parent (useful if controller is on a child object)
        if (rb == null)
            rb = GetComponentInParent<Rigidbody>();

        if (rb == null)
            Debug.LogError($"[{nameof(boatcontoller)}] No Rigidbody found on '{name}', children or parents.");
    }

    public void Throttle(InputAction.CallbackContext context)
    {
        throttle = context.ReadValue<float>();
        Debug.Log("Throttle input: " + throttle);
    }

    public void Steering(InputAction.CallbackContext context)
    {
        steering = context.ReadValue<float>();
        Debug.Log("Steering input: " + steering);
    }
    public void Update()
    {
        worldtolocal = GetComponent<worldtolocal>();
        neededY = transform.rotation.y * 36;
        worldtolocal.useLocalY = neededY;
    }
    private void FixedUpdate()
    {
      
        if (rb == null) return;

        rb.isKinematic = false;

        // Forward & backward movement
        float currentSpeed = throttle > 0 ? speed : backSpeed;

        // Turning: apply yaw torque
        rb.AddTorque(Vector3.up * steering * rotationSpeed * Time.fixedDeltaTime);

        // Only clamp pitch/roll angular velocity so yaw (steering) is not squashed.
        // Convert world angular velocity to local space
        Vector3 localAngVel = transform.InverseTransformDirection(rb.angularVelocity);

        float maxAng = rotationSpeed * 0.1f;
        // clamp X and Z (pitch/roll), preserve Y (yaw)
        localAngVel.x = Mathf.Clamp(localAngVel.x, -maxAng, maxAng);
        localAngVel.z = Mathf.Clamp(localAngVel.z, -maxAng, maxAng);

        // Convert back to world space and assign
        rb.angularVelocity = transform.TransformDirection(localAngVel);

        // Accelerating (relative to object). Keep existing axis (Vector3.up) if that is intended.
        rb.AddRelativeForce(Vector3.up * currentSpeed * Time.fixedDeltaTime);
    }
}


