using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class gravity : MonoBehaviour
{
    Rigidbody2D rb;
    List<(Rigidbody2D rb, Transform trans)> object_list = new List<(Rigidbody2D rb, Transform trans)>();
    Rigidbody2D other_rb;
    Transform other_trans;
    public float G = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var elem in object_list)
        {
            if (elem.trans != null)
            {
                float dx = transform.position.x - elem.trans.position.x;
                float dy = transform.position.y - elem.trans.position.y;
                float r_sq = dx * dx + dy * dy;
                var n = new Vector2(dx, dy);
                n.Normalize();
                Vector2 F = (G * elem.rb.mass * rb.mass / r_sq) * n;
                elem.rb.velocity += Time.deltaTime * F / elem.rb.mass;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other_trans = other.gameObject.GetComponent(typeof(Transform)) as Transform;
        other_rb = other.gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        object_list.Add((other_rb, other_trans));
    }
}
