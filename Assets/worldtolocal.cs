using UnityEngine;

public class worldtolocal : MonoBehaviour
{
    public float useLocalY;
    public float worldx;
    public float worldz;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // useLocalY = transform.localRotation.y;
        transform.rotation = Quaternion.Euler(worldx, default, worldz);
    }
}
