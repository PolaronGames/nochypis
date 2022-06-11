using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class spawn_planet : MonoBehaviour
{
    public GameObject planet;
    public GameObject enemy;
    public int enemy_count = 100;
    public float planet_dist = 500f;
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
            float r = planet_dist * MathF.Sqrt(UnityEngine.Random.Range(0f, 1f));
            float theta = UnityEngine.Random.Range(0f, 1f) * 2f * MathF.PI;
            float x = r * MathF.Cos(theta);
            float y = r * MathF.Sin(theta);
            float dx = (x - heading.x);
            float dy = (y - heading.y);
            float dist_to_planet = MathF.Sqrt(dx * dx + dy * dy);
            if (dist_to_planet > 10 && r > 10)
            {
                Instantiate(enemy, new Vector3(x, y, 20), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
