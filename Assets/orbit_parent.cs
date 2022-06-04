using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class orbit_parent : MonoBehaviour
{
    Rigidbody2D parent_rb;
    Rigidbody2D rb;
    float G = 1.0f;
    public Vector2 init_vel = new Vector2(0, 5);

    // Start is called before the first frame update
    void Start()
    {
        parent_rb = transform.parent.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        rb = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        rb.velocity = init_vel;
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float r_sq = x*x+y*y;
        Vector2 F = (-G*rb.mass*rb.mass*parent_rb.mass/r_sq) * new Vector2(x, y);
        rb.velocity += F*Time.deltaTime;
    }
}
