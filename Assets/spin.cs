using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class spin : MonoBehaviour
{
    public float spin_speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += Vector3.forward * spin_speed*Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D other)
    {

        // Vector2 n = (other.transform.position - transform.position);
        // Vector2 v = new Vector2(-n.y, n.x);
        // v.Normalize();
        // Rigidbody2D rb = other.gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        // Vector2 target_v = MathF.PI*spin_speed*v*n.magnitude/180f;
        // rb.velocity = target_v;
    }
}
