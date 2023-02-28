using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventHandler : MonoBehaviour
{
    public static EventHandler Instance;

    [Header("Must be setted")]
    public UIImageFader uiFader;
    public TMP_Text timerText;
    private bool isTimerOn;
    private float timer = 999;
    private bool isOver;

    [Header("Setted during playtime")]
    public Event spaceEvent; public void SetEvent(Event spaceEvent) { this.spaceEvent = spaceEvent; }
    public void PlayEvent()
    {
        Instantiate(spaceEvent, Vector3.zero, Quaternion.identity);
    }
    public void SetTimer(int time)
    {
        isTimerOn = true;
        timer = time;
    }
    private void Awake()
    {
        Instance = this;
    }
    void FixedUpdate()
    {
        if (isTimerOn == false) { return; }

        timer -= Time.fixedDeltaTime;

        if (isOver == false) { timerText.text = $"{(int)timer}"; }
        
        if (UIGameOver.Instance.isGameOver == true) { return; }

        if (timer <= 0 && isOver == false)
        { 
            uiFader.FadeIn();
            isOver = true;
            timer = 1;
        }
        else if (timer <= 0 && isOver == true)
        {
            PlayerSystem.Instance.isTravelling = false;
            Player.Instance.ResetPosition();
            UnityEngine.SceneManagement.SceneManager.LoadScene("ColonyScene");
        }
    }
}