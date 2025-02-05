using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void TestPlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void NewPlayGame()
    {
        SceneManager.LoadScene(2);

        //PlayerHealth.instance.Restart();

        //GameObject Player = GameObject.FindWithTag("Player");
        //Transform transform = GetComponent<Transform>();
        //Application.LoadLevel(2);
        //ResetPlayerPosition();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
