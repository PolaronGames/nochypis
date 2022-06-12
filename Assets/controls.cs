using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

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
    bool landed = false;
    float zoom_scale = 1f;
    Camera cam;
    public GameObject eugene;
    bool eugene_walking = false;
    GameObject planet;

    // Start is called before the first frame update
    void Start()
    {
        laser = transform.GetChild(0).gameObject;
        laser.SetActive(false);
        rb = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        main_camera = GameObject.FindGameObjectWithTag("MainCamera");
        planet = GameObject.FindGameObjectWithTag("Finish");
        main_camera_rb = main_camera.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        joint = GetComponent(typeof(HingeJoint2D)) as HingeJoint2D;
        joint.enabled = false;
        cam = main_camera.GetComponent<Camera>();

        // point ship to planet
        var dir = planet.transform.position - transform.position;
        transform.eulerAngles = Vector3.forward * (-90f + MathF.Atan2(dir.y, dir.x) * 180f / MathF.PI);
    }

    // Update is called once per frame
    void Update()
    {
        // Linear forces
        Vector2 acc = new Vector2(0, 0);
        if (Input.GetKey("space") || landed) // comically accelerate the ship after landing
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


        // zoom the camera if landed
        if (landed)
        {
            if (MathF.Abs(cam.orthographicSize - zoom_scale) < 1e-3)
            {
                cam.orthographicSize = zoom_scale;
            }
            else
            {
                cam.orthographicSize += (zoom_scale - cam.orthographicSize) * Time.deltaTime;
            }
        }
        else
        {
            
            // move camera to ship
            float dist = ((Vector2)(transform.position - main_camera.transform.position)).magnitude;
            if (dist > 0.05)
            {
                Vector3 camera_dir = (Vector3)((Vector2)(transform.position - main_camera.transform.position));
                float r = camera_dir.magnitude;
                camera_dir.Normalize();
                main_camera_rb.velocity = camera_dir * r * r * camera_speed;

                // adjust camera scale based on speed
                cam.orthographicSize += (10f + rb.velocity.magnitude - cam.orthographicSize) * Time.deltaTime / 10f;
            }
            else
            {
                main_camera_rb.velocity = new Vector3(0, 0, 0);
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            //stop planet spinning
            other.gameObject.GetComponent<spin>().spin_speed = 0f;

            //var bullets = GameObject.FindGameObjectsWithTag("Bullet");
            //foreach (GameObject bullet in bullets)
            //{
            //    Destroy(bullet);
            //}

            landed = true;
            GameObject.FindGameObjectWithTag("Exhaust").GetComponent<exhaust>().ship_landed = true;
            linear_acc *= 1.5f;
            // spawn Eugene
            if (!eugene_walking)
            {
                float distance_from_planet_center = 0.9025f;
                eugene_walking = true;
                Vector2 dir = transform.position - other.gameObject.transform.position;
                Vector3 eugene_pos = (Vector2)other.gameObject.transform.position + distance_from_planet_center * dir;
                eugene_pos.z = transform.position.z;
                var eugene_obj = Instantiate(eugene, eugene_pos, Quaternion.identity);
                // make all enemies target Eugene
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    //Destroy(enemy);
                    enemy.GetComponent<enemy>().player = eugene_obj;
                    enemy.GetComponent<enemy>().player_rb = eugene_obj.GetComponent<Rigidbody2D>();
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" && !landed)
        {
            //Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    //private void OnCollisionExit2D(Collision2D other)
    //{
    //    joint.enabled = false;
    //    joint.connectedBody = null;
    //    joint.autoConfigureConnectedAnchor = true;
    //    landed = false;
    //}
}
