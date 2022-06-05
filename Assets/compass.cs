using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class compass : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject target;
    GameObject player;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Finish");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Finish");
        }
        Vector2 dir = target.transform.position - player.transform.position;
        dir.Normalize();
        float angle = -90f+MathF.Atan2(dir.y, dir.x)*180f/MathF.PI;
        transform.eulerAngles = Vector3.forward * angle;
    }
}
