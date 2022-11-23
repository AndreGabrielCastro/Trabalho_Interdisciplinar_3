using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    public static UIGameOver Instance;
    [HideInInspector] public Image imageGameOver;
    [HideInInspector] public TMP_Text textGameOver;
    [HideInInspector] public Button buttonQuitGame;
    [HideInInspector] public TMP_Text textButtonQuitGame;
    private float transparency;
    [HideInInspector] public bool isGameOver;
    private IsFading isFading = IsFading.IsFadingImage;
    enum IsFading { IsFadingImage, IsFadingText, IsFadingButton, }
    private void Awake()
    {
        Instance = this;
        this.imageGameOver = this.GetComponentInChildren<Image>();
        this.textGameOver = this.GetComponentInChildren<TMP_Text>();
        this.buttonQuitGame = this.GetComponentInChildren<Button>();
        this.textButtonQuitGame = buttonQuitGame.GetComponent<TMP_Text>();
        this.imageGameOver.gameObject.SetActive(false);
        this.textGameOver.gameObject.SetActive(false);
        this.buttonQuitGame.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (isGameOver == false) { return; }
        switch (isFading)
        {
            case IsFading.IsFadingImage:
                transparency += Time.fixedDeltaTime * 0.5f;
                imageGameOver.color = new Color(imageGameOver.color.r, imageGameOver.color.g, imageGameOver.color.b, transparency);
                if (transparency >= 1) { transparency = 0; textGameOver.gameObject.SetActive(true); isFading = IsFading.IsFadingText; }
                break;

            case IsFading.IsFadingText:
                transparency += Time.fixedDeltaTime * 0.5f;
                textGameOver.color = new Color(textGameOver.color.r, textGameOver.color.g, textGameOver.color.b, transparency);
                if (transparency >= 1) { transparency = 0; buttonQuitGame.gameObject.SetActive(true); isFading = IsFading.IsFadingButton; }
                break;

            case IsFading.IsFadingButton:
                transparency += Time.fixedDeltaTime * 0.5f;
                textButtonQuitGame.color = new Color(textButtonQuitGame.color.r, textButtonQuitGame.color.g, textButtonQuitGame.color.b, transparency);
                //if (transparency >= 1) { Time.timeScale = 0; }
                break;
        }
    }

    /// <summary>
    /// Ends the game.
    /// </summary>
    public void SetGameOver() { isGameOver = true;  imageGameOver.gameObject.SetActive(true); }
}