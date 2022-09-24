using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager manager;

    void Awake() {
        if (manager == null)
            manager = this;
        else if (manager != this)
            Destroy(gameObject);

        SceneManager.LoadScene("Tutorial");
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    
    
}
