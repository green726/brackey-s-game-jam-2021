using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpScript : MonoBehaviour
{
    public playerScript playerScr;
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
        Debug.Log("collision with thing");
        if (collision.gameObject.name == "player")
        {
            Debug.Log("collision with player");
            StartCoroutine(playerScr.applyPowerUp(gameObject.name.Replace("_icon", "")));
        }
    }

    public void rotate()
    {

    }
}
