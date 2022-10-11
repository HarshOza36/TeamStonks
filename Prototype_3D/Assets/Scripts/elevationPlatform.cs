using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevationPlatform : MonoBehaviour
{
    private float ogLocation;
    public float speed;
    public float elevatingRange;
    public GameObject player_ball;
    // Start is called before the first frame update
    void Start()
    {
        ogLocation = transform.position.y;
        //Debug.Log("og location: " + ogLocation);
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, elevatingRange) +ogLocation;
        //Debug.Log("now og location: " + ogLocation);
        //Debug.Log("ping pong y: " + y);
        transform.position = new Vector3(0, y, 0);
    }

}
