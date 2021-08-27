using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeCreate : MonoBehaviour
{
    public int minExForce, maxExForce, explRadius;
    // Start is called before the first frame update
    void Start()
    {
        minExForce = 1000;
        maxExForce = 2000;
        explRadius = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void explodeThis()
    {

        //Debug.Log("explosion called");
        //collid.enabled = false;
       

            var rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("exploded: " + gameObject.name);
                rb.mass = .1f;
                rb.AddExplosionForce(Random.Range(minExForce, maxExForce), transform.position, explRadius);
                Destroy(gameObject, 5);
            }
        
    }
}
