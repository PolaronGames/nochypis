using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class exhaust : MonoBehaviour
{
    public GameObject particle;
    GameObject player;
    Rigidbody2D player_rb;
    public int max_particles = 100;
    public float particle_rate = 0.1f;
    float last_time;
    public float particle_speed = 1f;
    public float cone_angle = 10f;
    LinkedList<GameObject> particles = new LinkedList<GameObject>();
    public bool ship_landed = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_rb = player.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        last_time = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        int num_particles = (int)((Time.realtimeSinceStartup - last_time) / particle_rate);
        if (Input.GetKey("space") || ship_landed)
        {
            Vector2 dir = -player.transform.up;
            Vector3 particle_pos = player.transform.position + (Vector3)dir * 0.5f;
            for (int i = 0; i < num_particles; i++)
            {
                last_time = Time.realtimeSinceStartup;
                var particle_obj = Instantiate(particle, particle_pos, Quaternion.identity);
                particle_obj.transform.parent = transform;
                var particle_rb = particle_obj.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
                float rand_angle = UnityEngine.Random.Range(-cone_angle / 2, cone_angle / 2);
                Vector2 exhaust_dir = Quaternion.AngleAxis(rand_angle, Vector3.forward) * dir;
                float particle_speed_rand = UnityEngine.Random.Range(0.5f, 1f);
                particle_rb.velocity = particle_speed_rand * particle_speed * (Vector3)exhaust_dir;
                particles.AddLast(particle_obj);
                if (particles.Count > max_particles)
                {
                    Destroy(particles.First.Value);
                    particles.RemoveFirst();
                }
            }
        }
        else
        {
            // destroy particles
            // for (var node = particles.First; node != null; node = node.Next)
            // {
            //     Destroy(node.Value);
            // }
            // particles.Clear();
        }
    }
}
