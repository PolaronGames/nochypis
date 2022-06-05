using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distance_counter : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject planet;
    GameObject player;
    TextMesh counter;
    void Start()
    {
        planet = GameObject.FindGameObjectWithTag("Finish");
        player = GameObject.FindGameObjectWithTag("Player");
        counter = GetComponent(typeof(TextMesh)) as TextMesh;
    }

    // Update is called once per frame
    void Update()
    {
        if (planet == null)
        {
            planet = GameObject.FindGameObjectWithTag("Finish");
        }
        Vector2 dir = (Vector2)(planet.transform.position - player.transform.position);
        int dist = (int)dir.magnitude;
        counter.text = dist.ToString();
    }
}
