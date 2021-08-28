using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeCreate : MonoBehaviour
{
    private int minExForce = 1000;
    private int maxExForce = 2000;
    private int explRadius = 20;
    // Start is called before the first frame update
    void Start()
    {

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
                rb.mass = .1f;
                int explForce = Random.Range(minExForce, maxExForce);
                rb.AddExplosionForce(explForce, transform.position, explRadius);
                Destroy(gameObject, 5);
            }
        
    }
}
