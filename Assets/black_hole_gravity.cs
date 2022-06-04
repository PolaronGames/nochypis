using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class black_hole_gravity : MonoBehaviour
{
    Rigidbody2D rb;
    Rigidbody2D other_rb;
    Transform other_trans;
    SpriteRenderer sprite;
    public float G = 1f;
    Color sprite_color;
    bool in_range = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        sprite = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        sprite_color = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (in_range)
        {
            float dx = transform.position.x - other_trans.position.x;
            float dy = transform.position.y - other_trans.position.y;
            float r_sq = dx * dx + dy * dy;
            var n = new Vector2(dx, dy);
            n.Normalize();
            Vector2 F = (G * other_rb.mass * rb.mass / r_sq) * n;
            other_rb.velocity += Time.deltaTime * F / other_rb.mass;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        sprite.color = Color.red;
        in_range = true;
        other_trans = other.gameObject.GetComponent(typeof(Transform)) as Transform;
        other_rb = other.gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Destroy(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        sprite.color = sprite_color;
        in_range = false;
        other_rb.velocity = new Vector2(0,0);
    }
}
