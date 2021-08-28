using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeDet : MonoBehaviour
{


    public Transform transf;
    public Collider collid;
    public GameObject playerOb;
    public bool playerLeft = false;
    public GameObject otherGameOb;
    public string nameOfOther;
    // Start is called before the first frame update
    void Start()
    {
        nameOfOther = name.Replace("simple", "fractured");
        otherGameOb = GameObject.Find(nameOfOther);
        otherGameOb.SetActive(false);
        transf = GetComponent<Transform>();
        collid = GetComponent<Collider>();
        playerOb = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(playerOb.transform.position, transf.position));
        if (playerLeft == true)
        {
            Debug.Log("started distance check");
            if (gameObject.name.Contains("pipe"))
            {
                if (Vector3.Distance(playerOb.transform.position, transf.position) >= 20f)
                {
                    // Debug.Log("passed distance check");
                    // Debug.Log(otherGameOb);
                    otherGameOb.SetActive(true);
                    gameObject.SetActive(false);
                    foreach (Transform t in otherGameOb.transform)
                    {
                        t.gameObject.GetComponent<explodeCreate>().explodeThis();
                    }

                }
            }
            else
            {
                if (Vector3.Distance(playerOb.transform.position, transf.position) >= 10f)
                {
                    // Debug.Log("passed distance check");
                    // Debug.Log(otherGameOb);
                    otherGameOb.SetActive(true);
                    gameObject.SetActive(false);
                    foreach (Transform t in otherGameOb.transform)
                    {
                        t.gameObject.GetComponent<explodeCreate>().explodeThis();
                    }

                }
            }
       
        }
    }


    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.name == "player")
        {
            playerLeft = true;

        }
    }


}