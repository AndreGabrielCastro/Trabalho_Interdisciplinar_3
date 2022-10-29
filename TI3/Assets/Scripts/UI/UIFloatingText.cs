using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFloatingText : MonoBehaviour
{
    private TMP_Text text;
    [HideInInspector] public float fadeTime = 2;
    private float time = 0;
    private void Awake() { text = this.GetComponent<TMP_Text>(); }
    private void FixedUpdate()
    {
        if (time >= fadeTime) { Destroy(this.gameObject); }
        time += Time.fixedDeltaTime;
        text.color -= new Color(0, 0, 0, 1 / fadeTime * Time.fixedDeltaTime);
    }
}
