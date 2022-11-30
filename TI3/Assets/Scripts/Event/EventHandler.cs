using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventHandler : MonoBehaviour
{
    [Header("Must be setted")]
    public UIImageFader uiFader;
    public TMP_Text timerText;
    private float timer = 40;
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
            if (UIGameOver.Instance.isGameOver == true) { return; }
            PlayerSystem.Instance.isTravelling = false;
            Player.Instance.ResetPosition();
            UnityEngine.SceneManagement.SceneManager.LoadScene("ColonyScene");
        }
    }
}
