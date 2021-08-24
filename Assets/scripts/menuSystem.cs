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
    void start()
    {
        escMenu = GameObject.Find("escapeMenu");
        player = GameObject.Find("playerMain");
        //get player script component
        
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
        saveGame.performSave(player.GetComponent<playerScript>());
    }


  

}
