using UnityEngine;

public class boxMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        body.velocity = new Vector2(Input.GetAxis("Horizontal")*speed,Input.GetAxis("Vertical")*speed);

    }
}
