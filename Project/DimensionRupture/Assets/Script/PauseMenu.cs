using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;//PauseMenu
    //public static bool QuitCheckPanel = false;//QuitGame
    public GameObject pauseMenuUI;//PauseMenu
    //public GameObject QuitCheckUI;//QuitGame


    public static bool StatusPanel = false;//StatusPanel
    public GameObject StatusMenuUI;//StatusPanel

    public static bool SpPanel = false;//SpPanel
    public GameObject SpMenuUI;//SpPanel

    public static bool TutorialPanel = false;//チュートリアル操作パネル
    public GameObject TutorialMenuUI;

    //-----------------------シーンチェンジの保存-----------------------//
    public static PauseMenu instance;
    //-----------------------シーンチェンジの保存-----------------------//

    [SerializeField] private GameObject _BackGameFirst;
    [SerializeField] private GameObject _OffStatusPanelFirst;
    [SerializeField] private GameObject _OffSpPanelFirst;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    //-----------------------シーンチェンジの保存-----------------------//

    void Update()
    {
        //-----------------------PauseMenu-----------------------//
        if (Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.Escape))//7 = START
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        //-----------------------PauseMenu-----------------------//


        //-----------------------StatusMenu-----------------------//
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton4))//4 = LB
        {
            if (StatusPanel)
            {
                ExitStatus();
                OffStatusUp();
            }
            else
            {
                Status();
            }
        }
        //-----------------------StatusMenu-----------------------//


        //-----------------------TutorialMenu-----------------------//
        if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.JoystickButton6))//6 = BACK
        {
            if (TutorialPanel)
            {
                ExitTutorial();
            }
            else
            {
                Tutorial();
            }
        }
        //-----------------------TutorialMenu-----------------------//

    }


    //-----------------------PauseMenu-----------------------//
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        EventSystem.current.SetSelectedGameObject(_BackGameFirst);
    }

    //public void QuitCheck()
    //{
        //QuitCheckUI.SetActive(false);
        //Time.timeScale = 0f;
        //QuitCheckPanel = false;
    //}

    public void LoadMainMenu()
    {
        Destroy(gameObject);
        Time.timeScale = 1f;
        //SceneManager.LoadScene("Menu");
        Application.LoadLevel("Menu");
        DontDestroyOnLoad(gameObject);
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
    //-----------------------PauseMenu-----------------------//


    //-----------------------StatusMenu-----------------------//
    public void ExitStatus()
    {
        StatusMenuUI.SetActive(false);
        StatusPanel = false;

    }

    void Status()
    {
        StatusMenuUI.SetActive(true);
        StatusPanel = true;

        EventSystem.current.SetSelectedGameObject(_OffStatusPanelFirst);
    }

    //-----------------------SpPanel-----------------------//
    public void StatusUp()
    {
        SpMenuUI.SetActive(true);
        SpPanel = true;

        EventSystem.current.SetSelectedGameObject(_OffSpPanelFirst);
    }

    public void OffStatusUp()
    {
        SpMenuUI.SetActive(false);
        SpPanel = false;
    }
    //-----------------------SpPanel-----------------------//

    //-----------------------StatusMenu-----------------------//


    //-----------------------TutorialMenu-----------------------//
    public void ExitTutorial()
    {
        TutorialMenuUI.SetActive(false);
        TutorialPanel = false;

    }

    void Tutorial()
    {
        TutorialMenuUI.SetActive(true);
        TutorialPanel = true;
    }
    //-----------------------TutorialMenu-----------------------//

}