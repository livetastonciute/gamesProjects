using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class TimerController : MonoBehaviour
{
    [SerializeField]
    Text timeText;

    [SerializeField]
    Text firstText;

    [SerializeField]
    Text secondText;

    [SerializeField]
    Text thirdText;

    [SerializeField]
    Text timer;

    float laikas = 30;

    bool first = true;
    bool second = true;
    bool third = true;

    [SerializeField]
    UnityEvent noTimeLeft;

    float sekundes = 0;

    private void Update()
    {
        if (laikas > 0)
        {
            laikas -= Time.deltaTime;
            timer.text = "" + Mathf.Round(laikas);
        }
        else
        {
            noTimeLeft.Invoke();
        }

        if (timeText.IsActive())
        {
            sekundes += Time.deltaTime;
            timeText.text = "Laikas: " + Mathf.Round(sekundes);
        }

        if(!(timeText.IsActive()) && sekundes > 0)
        {

            if (!first && !second && !third)
            {
                first = true;
                second = true;
                third = true;
            }

            if (first)
            {
                firstText.text = "" + Mathf.Round(sekundes);
                first = false;
            }

            else if (second)
            {
                secondText.text = "" + Mathf.Round(sekundes);
                second = false;
            }
            else if (third)
            {
                thirdText.text = "" + Mathf.Round(sekundes);
                third = false;
            }
            sekundes = 0;
        }
    }
}
