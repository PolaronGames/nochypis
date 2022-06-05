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
    Rigidbody2D main_camera_rb;
    GameObject main_camera;
    public float camera_speed = 5f;
    HingeJoint2D joint;

    // Start is called before the first frame update
    void Start()
    {
        laser = transform.GetChild(0).gameObject;
        laser.SetActive(false);
        rb = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        main_camera = GameObject.FindGameObjectWithTag("MainCamera");
        main_camera_rb = main_camera.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        joint = GetComponent(typeof(HingeJoint2D)) as HingeJoint2D;
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Linear forces
        Vector2 acc = new Vector2(0, 0);
        if (Input.GetKey("space"))
        {
            // accelerate in direction ship is facing
            Vector2 dir = transform.up;
            acc += linear_acc * dir;
        }
        rb.velocity += acc * Time.deltaTime;

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

        // move camera to ship
        Vector3 camera_dir = (Vector3)((Vector2)(transform.position - main_camera.transform.position));
        float r = camera_dir.magnitude;
        camera_dir.Normalize();
        main_camera_rb.velocity = camera_dir * r * r * camera_speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D other_rb = other.gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        joint.enabled = true;
        joint.connectedBody = other_rb;
        joint.autoConfigureConnectedAnchor = false;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        joint.enabled = false;
        joint.connectedBody = null;
        joint.autoConfigureConnectedAnchor = true;
    }
}
