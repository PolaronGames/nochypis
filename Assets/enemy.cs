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
                    // point at Eugene
                    Vector2 fire_dir = dir;
                    if (v1 > 0f)
                    {
                        // lead the shot
                        Vector3 cross = Vector3.Cross(player_rb.velocity, -dir);
                        float theta1 = cross.magnitude/ (v1 * dir.magnitude);
                        float cross_z = cross.z;
                        float theta2 = MathF.Sign(cross_z) * MathF.Asin((v1 / fire_speed) * MathF.Sin(theta1));
                        fire_dir.x = (MathF.Cos(theta2) * dir.x - MathF.Sin(theta2) * dir.y);
                        fire_dir.y = (MathF.Sin(theta2) * dir.x + MathF.Cos(theta2) * dir.y);
                    }
                    var bullet_obj = Instantiate(bullet, transform.position, Quaternion.identity);
                    var bullet_rb = bullet_obj.GetComponent<Rigidbody2D>();
                    bullet_rb.velocity = fire_dir * fire_speed;
                    prev_shot_time = Time.realtimeSinceStartup;
                    transform.eulerAngles = Vector3.forward * (MathF.Atan2(fire_dir.y, fire_dir.x) * 180f / MathF.PI);
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
