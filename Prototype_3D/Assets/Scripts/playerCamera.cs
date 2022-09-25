using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{   
    public Transform ball;
    private Vector3 offset;
    public float lerpSpeed;

    void Start()
    {
        offset = transform.position - ball.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, ball.position + offset, lerpSpeed);
        transform.position = newPos;
    }
}
