using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class doodleJump : MonoBehaviour
{
    public GameObject player_ball;
    public float speed;
    public bool hasDoodleJump;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        hasDoodleJump = false;
        targetPosition = new Vector3(player_ball.transform.position.x, 15, player_ball.transform.position.z);
        //transform.Translate(0, 13, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ball poisiotn: " + player_ball.transform.position);
        if (hasDoodleJump == true)
        {
            Debug.Log("inside update()");
            player_ball.transform.position = Vector3.MoveTowards(
                player_ball.transform.position,
                targetPosition,
                speed * Time.deltaTime
            );
            //hasDoodleJump = false;
            //Destroy(gameObject);

            if (Mathf.Abs(targetPosition.y - player_ball.transform.position.y) < 5)
            {
                hasDoodleJump = false;
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

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.name == "Player_Ball")
    //    {
    //        Destroy(gameObject);
    //    }
    //}



}