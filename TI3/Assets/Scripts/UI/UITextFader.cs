using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextFader : MonoBehaviour
{
    [SerializeField] private bool onStartFade = true;
    [HideInInspector] public TMP_Text text;
    private float transparency;
    private Transparency desiredTransparency = Transparency.Idle;
    enum Transparency { Transparent, Opaque, Idle }

    private void Awake()
    {
        this.text = this.GetComponent<TMP_Text>();

        transparency = 1;
        text.color = new Color(text.color.r, text.color.g, text.color.b, transparency);
    }
    private void Start() { if (onStartFade == true) { Invoke(nameof(LateStart), 0.5f); } }

    /// <summary>
    /// Is called by Start to be invoked in half second.
    /// </summary>
    private void LateStart() { FadeOut(); }
    private void FixedUpdate()
    {
        switch (desiredTransparency)
        {
            case Transparency.Transparent:

                transparency -= Time.fixedDeltaTime;
                text.color = new Color(text.color.r, text.color.g, text.color.b, transparency);
                if (transparency <= 0) { desiredTransparency = Transparency.Idle; }

                break;

            case Transparency.Opaque:

                transparency += Time.fixedDeltaTime;
                text.color = new Color(text.color.r, text.color.g, text.color.b, transparency);
                if (transparency >= 1) { desiredTransparency = Transparency.Idle; }

                break;

            case Transparency.Idle:
                break;
        }
    }

    /// <summary>
    /// Sets the desired transparency to transparent.
    /// </summary>
    public void FadeOut() { desiredTransparency = Transparency.Transparent; }

    /// <summary>
    /// Sets the desired transparency to opaque.
    /// </summary>
    public void FadeIn() { desiredTransparency = Transparency.Opaque; }
}
