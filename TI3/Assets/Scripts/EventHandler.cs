﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventHandler : MonoBehaviour
{
    [Header("Must be setted")]
    public UIFader uiFader;
    public TMP_Text timerText;
    private float timer = 5;
    private bool isOver;
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;

        if (isOver == false)
        {
            timerText.text = $"{(int)timer}";
        }
        if (timer <= 0 && isOver == false)
        { 
            uiFader.FadeIn();
            isOver = true;
            timer = 1;
        }
        else if (timer <= 0 && isOver == true)
        {
            PlayerSystem.Instance.isTravelling = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("ColonyScene");
        }
    }
}
