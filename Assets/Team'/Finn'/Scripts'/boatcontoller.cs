using UnityEngine;
using UnityEngine.InputSystem;


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
    public float neededY;
    public string direction = "forward";
    public worldtolocal worldtolocal;
    public bool canMove = false;
    private NewFloatingObject floatingObject;
    [Header("Effects")]
    public GameObject Effects;
    public GameObject IdleEffects;
    public bool idleEffects;
    public bool effects;
    public WaterVFX waterVFX;
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
        floatingObject = GetComponentInChildren<NewFloatingObject>();

        if (floatingObject != null && floatingObject.isUnderwater)
        {
            Debug.Log("Boat is underwater, can move.");
            canMove = true;
        }
        else
        {
            canMove = false;
        }
      
        worldtolocal = GetComponent<worldtolocal>();
        neededY = transform.rotation.y * 36;
        worldtolocal.useLocalY = neededY;
       
      
       
        if (steering != 0 || throttle != 0)
        {
           idleEffects = true;
        }
        else
        {
            idleEffects = false;
        }


    }
    private void FixedUpdate()
    {
      
        if (rb == null) return;

        rb.isKinematic = false;
        if (canMove == true)
        {
            // forward backward movement
            float currentSpeed = throttle > 0 ? speed : backSpeed;

            // Turning
            rb.AddTorque(Vector3.up * steering * rotationSpeed * Time.fixedDeltaTime);

            Vector3 localAngVel = transform.InverseTransformDirection(rb.angularVelocity);

            float maxAng = rotationSpeed * 0.1f;
            // clamp X and Z (pitch/roll), preserve Y (yaw)
            localAngVel.x = Mathf.Clamp(localAngVel.x, -maxAng, maxAng);
            localAngVel.z = Mathf.Clamp(localAngVel.z, -maxAng, maxAng);

            // Convert back to world space and assign
            rb.angularVelocity = transform.TransformDirection(localAngVel);

            // Accelerating (relative to object). Keep existing axis (Vector3.up) if that is intended.
            rb.AddRelativeForce(Vector3.up * currentSpeed * Time.fixedDeltaTime);
            
            if ( steering != 0 || throttle != 0)
            {
                effects = true;
            }
            else
            {
                effects = false;
            }
        }
        else if (canMove == false)
        {
            effects = false;
            
        }



    }
}


