using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemy : MonoBehaviour
{
    GameObject player;
    Rigidbody2D player_rb;
    public GameObject bullet;
    bool in_range = false;
    public float fire_rate = 1f;
    public float fire_speed = 1f;
    float prev_shot_time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // we want to lead the shot, so calculate where Eugene will be if his velocity is constant and adjust the angle
        if (in_range)
        {
            // shoot Eugene
            if (Time.realtimeSinceStartup - prev_shot_time > fire_rate)
            {
                // direction to Eugene
                var dir = player.transform.position - transform.position;
                dir.z = 0f;
                dir.Normalize();
                float v1 = player_rb.velocity.magnitude;
                if (fire_speed > v1)
                {
                    float theta1 = Vector2.Dot((Vector2)player_rb.velocity, -(Vector2)dir) / (v1 * dir.magnitude);
                    float cross_z = Vector3.Cross(player_rb.velocity, -dir).z;
                    float theta2;
                    Vector2 fire_dir;
                    if (cross_z > 0f)
                    {
                        theta2 = MathF.Asin((v1 / fire_speed) * MathF.Sin(theta1));
                        fire_dir.x = (MathF.Cos(theta2) * dir.x - MathF.Sin(theta2) * dir.y);
                        fire_dir.y = -(MathF.Sin(theta2) * dir.x - MathF.Cos(theta2) * dir.y);
                    }
                    else if (cross_z < 0f)
                    {
                        theta2 = -MathF.Asin((v1 / fire_speed) * MathF.Sin(theta1));
                        fire_dir.x = (MathF.Cos(theta2) * dir.x - MathF.Sin(theta2) * dir.y);
                        fire_dir.y = -(MathF.Sin(theta2) * dir.x - MathF.Cos(theta2) * dir.y);
                    }
                    else
                    {
                        fire_dir = dir;
                    }
                    var bullet_obj = Instantiate(bullet, transform.position, Quaternion.identity);
                    var bullet_rb = bullet_obj.GetComponent<Rigidbody2D>();
                    bullet_rb.velocity = fire_dir * fire_speed;
                    transform.eulerAngles = Vector3.forward * (MathF.Atan2(fire_dir.y, fire_dir.x) * 180f / MathF.PI);
                    prev_shot_time = Time.realtimeSinceStartup;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            in_range = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            in_range = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            in_range = true;
        }
    }
}
