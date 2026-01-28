using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class boatcontoller : MonoBehaviour
{
    public float speed = 10f;
    public float backSpeed = 4f;
    public float rotationSpeed = 100f;
    public float speedLimit = 20f;

    public Rigidbody rb;
    

    private float throttle;
    private float steering;
    public float speedcurrent;

    public string direction = "forward";
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

    private void FixedUpdate()
    { 
        rb.isKinematic = false;

        // Forward & backward movement
        float currentSpeed = throttle > 0 ? speed : backSpeed;
        


        // Turning
        rb.AddTorque(Vector3.up * steering * rotationSpeed * Time.fixedDeltaTime);
        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, rotationSpeed * 0.1f);
        // Accelerating
        rb.AddRelativeForce(Vector3.up * currentSpeed * Time.fixedDeltaTime);
    }
}


