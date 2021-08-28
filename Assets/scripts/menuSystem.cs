using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class menuSystem : MonoBehaviour
{
    public GameObject escMenu;
    public GameObject player;
    public playerScript plScript;
    public bool escMenuOpen = false;
    public GameObject optionsMenu;
    public GameObject sensSlider;
    public GameObject sensText;
    public TextMeshProUGUI winGameText;
    public GameObject winGameMenu;
    public stopWatch timeScript;
    public GameObject uiMenu;
    public GameObject deathMenu;

    void Start()
    {


        sensText.GetComponent<TMP_InputField>().text = plScript.mouseSens.ToString();
        sensSlider.GetComponent<Slider>().value = plScript.mouseSens;
        escMenu.SetActive(false);
        deathMenu.SetActive(false);
        winGameMenu.SetActive(false);
        optionsMenu.SetActive(false);
        //get player script component
        ResumeGame();
        //escMenu.SetActive(false);
    }

    public void quitFromEsc()
    {
        Application.Quit();

    }

    public void mainFromEsc()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void closeEsc()
    {
        closeOptions();
        escMenuOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        plScript.isPaused = false;
        escMenu.SetActive(false);
        ResumeGame();
    }

    public void openEsc()
    {
        escMenuOpen = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        plScript.isPaused = true;
        Debug.Log(escMenu);
        escMenu.SetActive(true);
        PauseGame();
    }

    public void openOptions()
    {
        optionsMenu.SetActive(true);
        escMenu.SetActive(false);
    }

    public void closeOptions()
    {
        optionsMenu.SetActive(false);
        escMenu.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void getMouseSensSlider()
    {
        float changedSens = sensSlider.GetComponent<Slider>().value;
        changedSens = Mathf.Round(changedSens * 10);
        changedSens = changedSens / 10;
        sensText.GetComponent<TMP_InputField>().text = changedSens.ToString();
        plScript.mouseSens = changedSens;
       
    }

    public void getMouseSensInput()
    {
        float changedSens = float.Parse(sensText.GetComponent<TMP_InputField>().text);
        changedSens = Mathf.Round(changedSens * 10);
        changedSens = changedSens / 10;
        sensSlider.GetComponent<Slider>().value = changedSens;
        plScript.mouseSens = changedSens;
    }

    public void saveGameStart()
    {
        saveGame.performSave(plScript);
    }

    public void winGame()
    {
        Debug.Log("won gane");
        PauseGame();
        winGameMenu.SetActive(true);
        uiMenu.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        timeScript.wonGame = true;
        winGameText.text = "You beat the level! your time was:" + timeScript.timePassed;
        saveGameStart();
    }
    public void die()
    {
        PauseGame();
        deathMenu.SetActive(true);
        uiMenu.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void nextLevel()
    {
        SceneManager.LoadScene(plScript.playerLevelString);
    }
}
