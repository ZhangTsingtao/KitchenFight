using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public float startingTime;
    float currentTime;
    [SerializeField] TMP_Text timerText;

    private void Start()
    {
        currentTime = startingTime;
    }
    void Update()
    {
        currentTime -= Time.deltaTime;
        timerText.text = ((int)currentTime / 60).ToString() + " : " + (currentTime % 60).ToString("0");
        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }
}
