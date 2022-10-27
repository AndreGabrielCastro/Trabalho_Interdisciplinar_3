using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFloatingText : MonoBehaviour
{
    private TMP_Text text;
    private float timeToFade = 2;
    private void Awake() { text = this.GetComponent<TMP_Text>(); }
    private void FixedUpdate()
    {
        if (timeToFade <= 0) { Destroy(this.gameObject); }
        timeToFade -= Time.fixedDeltaTime;
        text.color -= new Color(0, 0, 0, 1 * 0.5f * Time.fixedDeltaTime);
    }
}
