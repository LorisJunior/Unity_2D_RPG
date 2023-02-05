using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject questMark;
    public GameObject questDialog;
    public float displayTime = 5f;

    float timerDisplay;
    bool displayActive;
  
    // Update is called once per frame
    void Update()
    {
        if (displayActive)
        {
            timerDisplay -= Time.deltaTime;

            if (timerDisplay < 0)
            {
                displayActive = false;
                questDialog.SetActive(false);
            }
        }
    }

    public void DisplayQuest()
    {
        questMark.SetActive(false);
        questDialog.SetActive(true);
        timerDisplay = displayTime;
        displayActive = true;
    }
}
