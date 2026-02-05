using UnityEngine;

public class playerbase1 : MonoBehaviour
{
    public GameObject playerbase;
    public GameObject me;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerbase = GameObject.Find("playerbase");
        me = this.gameObject;
        me.transform.parent = playerbase.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
