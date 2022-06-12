using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class eugene_walk : MonoBehaviour
{
    GameObject planet;
    Rigidbody2D rb;
    public float walk_speed = 0.2f;
    Camera cam;
    GameObject main_camera;
    Rigidbody2D main_camera_rb;
    public float camera_speed = 5f;
    Animator anim;
    bool reached_toilet = false;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.FindGameObjectWithTag("Finish");
        rb = GetComponent<Rigidbody2D>();
        main_camera = GameObject.FindGameObjectWithTag("MainCamera");
        var player = GameObject.FindGameObjectWithTag("Player"); // player ship
        cam = main_camera.GetComponent<Camera>();
        main_camera_rb = main_camera.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = transform.position - planet.transform.position;
        dir.Normalize();
        // make Eugene stand upright
        transform.eulerAngles = Vector3.forward * (-90f + MathF.Atan2(dir.y, dir.x) * 180f / MathF.PI);
        float shitter_angle = planet.transform.eulerAngles.z + 90f;
        Vector2 shitter_vector = new Vector2(MathF.Cos(shitter_angle * MathF.PI / 180f), MathF.Sin(shitter_angle * MathF.PI / 180f));
        Vector3 cross = Vector3.Cross((Vector3)dir, (Vector3)shitter_vector);
        Vector2 a = new Vector2(-dir.y, dir.x);
        float angle_to_toilet = 180f * MathF.Asin(cross.magnitude) / MathF.PI;
        if (MathF.Abs(angle_to_toilet) < 5f && (dir + shitter_vector).magnitude > 1f)
        {
            // Eugene reached the toilet
            rb.velocity = new Vector3(0, 0, 0);
            anim.speed = 0;
            main_camera_rb.velocity = new Vector3(0, 0, 0);
            reached_toilet = true;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (cross.z > 0.0f)
        {
            // flip direction
            transform.localScale = new Vector3(-transform.localScale.y, transform.localScale.y, 1.0f);
            // walk left
            if (rb.velocity.magnitude < walk_speed)
            {
                rb.velocity += a * Time.deltaTime;
            }
        }
        else
        {
            // flip direction
            transform.localScale = new Vector3(transform.localScale.y, transform.localScale.y, 1.0f);
            // walk right
            if (rb.velocity.magnitude < walk_speed)
            {
                rb.velocity -= a * Time.deltaTime;
            }
        }

        if (!reached_toilet)
        {
            // move camera to Eugene
            Vector3 camera_dir = (Vector3)((Vector2)(transform.position - main_camera.transform.position));
            float r = camera_dir.magnitude;
            camera_dir.Normalize();
            main_camera_rb.velocity = camera_dir * r * r * camera_speed;
        }


        //Vector2 pos = (Vector2)transform.position;
        //main_camera.transform.position = new Vector3(pos.x, pos.y, main_camera.transform.position.z);

        // float dist = ((Vector2)(transform.position - main_camera.transform.position)).magnitude;
        // if (dist > 0.05)
        // {
        //     Vector3 camera_dir = (Vector3)((Vector2)(transform.position - main_camera.transform.position));
        //     float r = camera_dir.magnitude;
        //     camera_dir.Normalize();
        //     main_camera_rb.velocity = camera_dir * r * r * camera_speed;
        // }
        // else
        // {
        //     main_camera_rb.velocity = new Vector3(0, 0, 0);
        // }

        if (MathF.Abs((transform.eulerAngles.z - main_camera.transform.eulerAngles.z)) < 1f)
        {
            //main_camera.transform.eulerAngles = transform.eulerAngles;
        }
        else
        {
            // rotate camera so its upright
            //main_camera.transform.eulerAngles = Vector3.forward * 1000f * MathF.Sign(transform.eulerAngles.z - main_camera.transform.eulerAngles.z) * Time.deltaTime;
            //main_camera.transform.eulerAngles += (transform.eulerAngles - main_camera.transform.eulerAngles) * Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Eugene is invulnerable to bullets
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit");
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>(), true);
        }
    }
}
