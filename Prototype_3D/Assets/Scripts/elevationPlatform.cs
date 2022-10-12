using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevationPlatform : MonoBehaviour
{
    private float ogLocation_y;
    private float ogLocation_x;
    //public GameObject center_pipe_A;
    //public GameObject center_pipe_B;
    public float speed;
    public float elevatingRange;
    //public GameObject player_ball;
    // Start is called before the first frame update
    void Start()
    {
        ogLocation_y = transform.position.y;
        //ogLocation_x = transform.position.x;
        //center_pipe.gameObject.transform.position.x = 
        //Debug.Log("platform location: " + transform.position);
        //Debug.Log("pip location: " + center_pipe_A.transform.position);


    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, elevatingRange) +ogLocation_y;
        //Debug.Log("now og location: " + ogLocation);
        //Debug.Log("ping pong y: " + y);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        Debug.Log("new posiiton: " + transform.position);

    }

}
