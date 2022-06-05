using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class die_after_time : MonoBehaviour
{
    public float life_time = 10f;
    float birth_time;
    SpriteRenderer sprite;
    Color sprite_color;
    // Start is called before the first frame update
    void Start()
    {
        birth_time = Time.realtimeSinceStartup;
        sprite = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        sprite_color = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = MathF.Max(1.0f-(Time.realtimeSinceStartup - birth_time)/life_time, 0f);
        sprite_color.a = alpha;
        sprite.color = sprite_color;
        if(Time.realtimeSinceStartup - birth_time > life_time)
        {
            Destroy(this.gameObject);
        }
    }
}
