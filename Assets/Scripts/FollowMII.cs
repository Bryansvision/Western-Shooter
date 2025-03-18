using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMII : MonoBehaviour
{
    public Rigidbody rb;
    public float distance;
    
    void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z* distance));
    }
}
