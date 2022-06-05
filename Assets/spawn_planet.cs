using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class spawn_planet : MonoBehaviour
{
    public GameObject planet;
    public float planet_dist = 500f;
    // Start is called before the first frame update
    void Start()
    {
        
        float shitter_angle = UnityEngine.Random.Range(0f, 360f);
        float planet_angle = UnityEngine.Random.Range(0f, 360f);
        Vector2 heading = planet_dist*(new Vector2(MathF.Cos(planet_angle), MathF.Sin(planet_angle)));
        Instantiate(planet, new Vector3(heading.x, heading.y, 10), Quaternion.Euler(new Vector3(0, 0, shitter_angle)));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
