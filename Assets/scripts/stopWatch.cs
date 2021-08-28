using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class stopWatch : MonoBehaviour
{
    private float timePassed;
    public TextMeshProUGUI stopWatchText;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += 1 * Time.deltaTime;
        stopWatchText.text = "Time:" + timePassed;
    }

    public void removeTime(float removeAmount)
    {
        if (timePassed >= 10)
        {
            timePassed -= removeAmount;
        } else
        {
            timePassed = 0;
        }

    }
}
