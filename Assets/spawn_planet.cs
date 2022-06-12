using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class spawn_planet : MonoBehaviour
{
    public GameObject planet;
    public GameObject enemy;
    public int enemy_count = 2;
    public float planet_dist = 500f;
    public float enemy_radius = 2f;
    // Start is called before the first frame update
    void Start()
    {
        // spawn planet
        float shitter_angle = UnityEngine.Random.Range(0f, 360f);
        float planet_angle = UnityEngine.Random.Range(0f, 360f);
        Vector2 heading = planet_dist * (new Vector2(MathF.Cos(planet_angle), MathF.Sin(planet_angle)));
        Instantiate(planet, new Vector3(heading.x, heading.y, 10), Quaternion.Euler(new Vector3(0, 0, shitter_angle)));

        // spawn enemies
        for (int j = 0; j < enemy_count; j++)
        {
            // float theta = UnityEngine.Random.Range(0f, 2f * MathF.PI);
            float theta = ((float)j / (float)enemy_count) * 2f * MathF.PI;
            float x = enemy_radius * MathF.Cos(theta);
            float y = enemy_radius * MathF.Sin(theta);
            Instantiate(enemy, new Vector3(x + heading.x, y + heading.y, 20), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
