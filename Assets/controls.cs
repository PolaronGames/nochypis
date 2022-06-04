using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class controls : MonoBehaviour
{
    public float rotation_acc = 1f;
    public float linear_acc = 1f;
    GameObject laser;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        laser = transform.GetChild(0).gameObject;
        laser.SetActive(false);
        rb = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    }

    // Update is called once per frame
    void Update()
    {
        // Linear forces
        Vector2 acc = new Vector2(0,0);
        if (Input.GetKey("space"))
        {
            // accelerate in direction ship is facing
            Vector2 dir = transform.up;
            acc += linear_acc * dir;
        }
        rb.velocity += acc * Time.deltaTime;

        // apply drag force opposite of angular velocity
        float angular_acc = 0f;
        if (Input.GetKey("left"))
        {
            angular_acc += rotation_acc;
        }
        if (Input.GetKey("right"))
        {
            angular_acc -= rotation_acc;
        }
        rb.angularVelocity += angular_acc * Time.deltaTime;

        // transform.eulerAngles += Vector3.forward * rotation_speed * Input.GetAxis("Horizontal");
        // if (Input.GetKey("space"))
        // {
        //     Vector2 dir = transform.up;
        //     rb.velocity += linear_acc * Time.deltaTime * (Vector3)dir;
        //     current_speed += (linear_speed - current_speed) * Time.deltaTime * linear_acc;
        // }
        // else
        // {
        //     current_speed -= current_speed * Time.deltaTime * linear_acc;
        // }
        // rb.velocity = current_speed * (Vector3)dir;

        if (Input.GetKey("e"))
        {
            // blast the laser
            laser.SetActive(true);
        }
        else
        {
            // stop blasting the laser
            laser.SetActive(false);
        }
    }
}
