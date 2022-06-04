using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    GameObject player;
    public float camera_speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)((Vector2)(player.transform.position - transform.position))*camera_speed*Time.deltaTime;
    }
}
