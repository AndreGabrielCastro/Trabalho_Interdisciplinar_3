using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Must be setted")]
    [SerializeField] private float speed = 1;
    [Header("Setted during playtime")]
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip alarmClip;
    [SerializeField] private TMP_Text alarmText;
    private bool isWarning;
    public float GetSpeed() { return speed; }
    public void SetSpeedText(TMP_Text text) { speedText = text; UpdateSpeedText(); }
    public void SetWarningText(TMP_Text text) { alarmText = text; }
    public void SetAudioSource(AudioSource aSource) { audioSource = aSource; audioSource.clip = alarmClip; }
    public void AlterateSpeed(float value) { speed += value; UpdateSpeedText(); }
    public void UpdateSpeedText() { speedText.text = $"{speed}m/s"; }
    private void PlayFleeingRouteAlarm()
    {
        if (isWarning == true) { return; }
        if (alarmText != null) { alarmText.enabled = true; }
        audioSource.Play();
        isWarning = true;
    }
    private void StopFleeingRouteAlarm()
    {
        if (isWarning == false) { return; }
        if (alarmText != null) { alarmText.enabled = false; }
        audioSource.Stop();
        isWarning = false;
    }
    void Update()
    {
        if (Player.Instance.isGameOver == true) { return; }
        if (Player.Instance.isTravelling == false) { return; }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        this.transform.position += direction * speed * Time.deltaTime;

        if (transform.position.x > 25 || transform.position.x < -25 || transform.position.z > 25 || transform.position.z < -25)
        {
            Player.Instance.SetGameOver(
                "You fled off route and ran out of fuel. " +
                "Now you are lost in space forever due to mishandling of " +
                "essential resources and inability to deal with the obstacles of your role. " +
                "You will soon be replaced and forgotten for all eternity.");
        }
        else if (transform.position.x > 15 || transform.position.x < -15 || transform.position.z > 15 || transform.position.z < -15)
        {
            PlayFleeingRouteAlarm();
            // Fleeing from route! Return to the center position!
        }
        else if (transform.position.x <= 15 && transform.position.x >= -15 && transform.position.z <= 15 && transform.position.z >= -15)
        {
            StopFleeingRouteAlarm();
        }
        
        // The spaceship is so fast that a single degree could make him get out of the route. The code below makes no sense to the LORE

        //Vector3 rotation = new Vector3(0, horizontal, 0);
        //this.transform.eulerAngles += rotation * Time.deltaTime;

        //if (rotation == Vector3.zero)
        //{ this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.identity, 0.5f * Time.deltaTime).normalized; }
    }
}