using UnityEngine;

public class boxMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    public float jump;
    private float canJump = 0f;
    private Rigidbody2D body;
    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale=4;
        jump=400;
    }
    private void Update() {
        // body.velocity = new Vector2(Input.GetAxis("Horizontal")*speed,Input.GetAxis("Vertical")*speed);
        if (Input.GetButtonDown("Jump") && Time.time > canJump){
            body.AddForce(new Vector2(0, jump));
            canJump = Time.time + 0.13f;
        }

    }
}
