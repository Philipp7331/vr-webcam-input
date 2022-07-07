using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime;

    public int startMinutes;

    public Text currentTimeText;

    void Start()
    {
        currentTime = startMinutes * 60;
    }

    void Update()
    {
        currentTimeText.text = currentTime.ToString();
    }
}
