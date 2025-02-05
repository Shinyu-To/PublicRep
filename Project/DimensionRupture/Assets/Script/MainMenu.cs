using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    public void TestPlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void NewPlayGame()
    {
        //SceneManager.LoadScene(2);
        Transform transform = GetComponent<Transform>();
        Application.LoadLevel(2);
        //ResetPlayerPosition();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
