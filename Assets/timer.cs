using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    TextMeshProUGUI text_comp;
    float start_time;
    float time_limit = 60;
    // Start is called before the first frame update
    void Start()
    {
        text_comp = GetComponent<TextMeshProUGUI>();
        start_time = Time.time;
        text_comp.text = ((int)time_limit).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        text_comp.text = ((int)(time_limit - (Time.time - start_time))).ToString();
    }
}
