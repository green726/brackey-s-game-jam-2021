using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    //! instead of setactive make the button not interactble and change the image to UImask. 
    public GameObject[] levelButtons;
    // Start is called before the first frame update
    void Start()
    {
        //loop through array of level buttons
        foreach (GameObject buttonOb in levelButtons)
        {
            //disable all buttons by default except for level 1
            if (buttonOb.name.Contains("1") == false)
            {
                buttonOb.GetComponent<Button>().interactable = false;
            }
        }
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

        foreach (GameObject buttonOb in levelButtons)
        {

            string buttonLevelTest = buttonOb.name.Replace("game", "").Replace("Button", "");
            Debug.Log("buttonTest:" + buttonLevelTest);
            //get the level int from the button name, do this by removing "button" and "game"
            int buttonLevel = int.Parse(buttonOb.name.Replace("game", "").Replace("Button", ""));
            //if the button level is less than or equal to player level unlock it
            if (buttonLevel <= gameData.playerLevelInt && buttonLevel <= 1)
            {
                buttonOb.GetComponent<Button>().interactable = true;
            }
            else
            {
                break;
            }
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
