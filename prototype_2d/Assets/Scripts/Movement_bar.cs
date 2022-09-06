using UnityEngine;
public class Movement_bar : MonoBehaviour
{
    [SerializeField] public int mapWidth;
    [SerializeField] public bool isWrapping;
    [SerializeField] public int movespeed = 1;
    public Vector2 userDirection = Vector2.right;
    
    private void Update() {
        //int i = 0;
        Vector2 pos = transform.position;
        transform.Translate(userDirection * movespeed * Time.deltaTime); 
        if (isWrapping) {
            if (pos.x >= mapWidth) {
                userDirection = Vector2.left;
            } else if (pos.x <= -1*mapWidth) {
                userDirection = Vector2.right;
            }
        }
    }
}