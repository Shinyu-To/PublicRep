using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string sceneName;
    [SerializeField] private string newScenePassword;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.scenePassword = newScenePassword;
            //SceneManager.LoadScene(sceneName);//old
            FindObjectOfType<SceneFader>().FadeTo(sceneName);
        }
    }
}
