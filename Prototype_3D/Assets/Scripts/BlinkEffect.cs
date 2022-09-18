using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{

    public Color startColor;
    public Color endColor;

    public float speed = 1;

    Renderer ren;

    void Awake()
    {
        ren = GetComponent<Renderer>();
    }

    void Update()
    {
        ren.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time*speed, 1));
    }
    
}
