using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class generate_stars : MonoBehaviour
{
    // Start is called before the first frame update
    public int star_count = 1000;
    public float radius = 500;
    public float min_scale = 0.025f;
    public float max_scale = 0.1f;
    public GameObject star;
    // layers
    List<GameObject> layers = new List<GameObject>();
    public int layer_count = 3;
    GameObject main_camera;
    void Start()
    {
        main_camera = GameObject.FindGameObjectWithTag("MainCamera");
        for (int i = 0; i < layer_count; i++)
        {
            GameObject layer = new GameObject("Layer " + i.ToString());
            layer.transform.parent = transform;
            layers.Add(layer);
            for (int j = 0; j < star_count; j++)
            {
                float r = radius * MathF.Sqrt(UnityEngine.Random.Range(0f, 1f));
                float theta = UnityEngine.Random.Range(0f, 1f) * 2f * MathF.PI;
                float x = r * MathF.Cos(theta);
                float y = r * MathF.Sin(theta);
                float scale = UnityEngine.Random.Range(min_scale, max_scale);
                var instance = Instantiate(star, new Vector3(x, y, 20), Quaternion.identity);
                instance.transform.localScale = new Vector3(scale, scale, 1.0f);
                instance.transform.parent = layer.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // move each layer less than the previous
        float factor = 10;
        for(int i = 0; i < layer_count; i++)
        {
            var layer = layers[i];
            layer.transform.position = main_camera.transform.position - main_camera.transform.position/(factor*(i+1));
        }
    }
}
