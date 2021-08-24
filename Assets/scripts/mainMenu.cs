using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    //! instead of setactive make the button not interactble and change the image to UImask. 
    public GameObject l1button;
    public GameObject l2button;
    public GameObject l3button;
    public GameObject l4button;
    public GameObject l5button;
    // Start is called before the first frame update
    void Start()
    {
        l2button.SetActive(false);
        l3button.SetActive(false);
        l4button.SetActive(false);
        l5button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadGameClick()
    {
        //get saved player data
        playerData gameData = saveGame.loadGameData();
        //loop through buttons for levels that player has made it to and unlock them
        Debug.Log(gameData.playerLevelInt);
        for (int i = 1; i <= int.Parse(gameData.playerLevelInt); i++)
        {
            string level = i.ToString();
            string buttonName = "game" + level + "Button";
            Debug.Log(buttonName);
            GameObject.Find(buttonName).SetActive(true);
        }

    }

    public void loadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void quitGame()
    {
        Application.Quit();
    }



}
