using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpScript : MonoBehaviour
{
    //public playerScript playerScr;
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
        if (collision.gameObject.TryGetComponent(out playerScript player))
        {
            Debug.Log("collision with player");
            player.StartCoroutine(player.applyPowerUp(gameObject.name.Replace("_icon", "")));
            gameObject.SetActive(false);
        }
    }

    public void rotate()
    {

    }
}
