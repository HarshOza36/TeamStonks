using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class doodleJump : MonoBehaviour
{
    public GameObject player_ball;
    public float speed;
    public bool hasDoodleJump;
    public float movingRange;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        hasDoodleJump = false;
        targetPosition = new Vector3(transform.position.x,
            transform.position.y+ movingRange,
            transform.position.z);
        Debug.Log("target Pos=" + targetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("ball position: " + player_ball.transform.position);
        if (hasDoodleJump == true)
        {
            // Debug.Log("inside update()");
            player_ball.transform.position = Vector3.MoveTowards(
                player_ball.transform.position,
                targetPosition,
                speed * Time.deltaTime
            );

            // if(player_ball.transform.position.y - targetPosition.y < 0.5 &&
            //     player_ball.transform.position.y - targetPosition.y > 0)
            // {
            //     player_ball.transform.position = new Vector3(targetPosition.x,
            //         player_ball.transform.position.y,
            //         targetPosition.z
            //         );


            //     hasDoodleJump = false;
            //     Destroy(gameObject);

            // }
            if(Mathf.Abs(player_ball.transform.position.y-targetPosition.y)<1){
                hasDoodleJump=false;
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collision" + collision.gameObject.name);

        if (collision.gameObject.name == "Player_Ball")
        {
            Debug.Log("collide with doodle jump");
            //move the ball up via update()
            hasDoodleJump = true;
        }
        
    }



}