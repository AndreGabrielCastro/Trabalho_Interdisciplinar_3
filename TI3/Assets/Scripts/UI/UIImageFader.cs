using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageFader : MonoBehaviour
{
    [SerializeField] private bool onStartFade = true;
    [HideInInspector] public Image image;
    private float transparency;
    private Transparency desiredTransparency = Transparency.Idle;
    enum Transparency { Transparent, Opaque, Idle }

    private void Awake()
    {
        this.image = this.GetComponent<Image>();

        transparency = 1;
        image.color = new Color(image.color.r, image.color.g, image.color.b, transparency);
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
                image.color = new Color(image.color.r, image.color.g, image.color.b, transparency);
                if (transparency <= 0) { desiredTransparency = Transparency.Idle; }

                break;

            case Transparency.Opaque:

                transparency += Time.fixedDeltaTime;
                image.color = new Color(image.color.r, image.color.g, image.color.b, transparency);
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