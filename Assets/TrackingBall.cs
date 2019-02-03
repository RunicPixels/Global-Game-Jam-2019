using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBall : MonoBehaviour
{
    public Rigidbody2D rb;

    public float maxDistance;
    public float intensity;
    public float horizontalMultiplier;
    public float verticalMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = rb.transform.position + Vector3.Scale(Vector3.ClampMagnitude(rb.velocity * intensity , maxDistance) - (Vector3.forward * 10),new Vector3(horizontalMultiplier,verticalMultiplier)) + Vector3.up * 0.75f;
    }
}
