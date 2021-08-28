using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winScript : MonoBehaviour
{
    public playerScript playerScr;
    public menuSystem menuScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "player")
        {
            if (playerScr.playerLevelInt < 5)
            {
                playerScr.playerLevelInt += 1;
                playerScr.playerLevelString = "game" + playerScr.playerLevelInt;
            }
            
            menuScript.winGame();
        }
    }

}
