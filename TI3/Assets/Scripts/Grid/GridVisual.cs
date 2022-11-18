using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esse script tem a função de representar a célula de grid por meio de um sprite simples e controlar seu comportamento.
public class GridVisual : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer spriteRenderer;
    private void Awake() { spriteRenderer = this.GetComponent<SpriteRenderer>(); }

    private void Start() { SetColorToWhite(); }

    /// <summary>
    /// Enables the sprite renderer.
    /// </summary>
    public void Show() { spriteRenderer.enabled = true; }

    /// <summary>
    /// Disables the sprite renderer.
    /// </summary>
    public void Hide() { spriteRenderer.enabled = false; }

    public void SetColorToGreen() { spriteRenderer.color = new Color(0.5f, 1f, 0.5f); }

    public void SetColorToWhite() { spriteRenderer.color = new Color(0.75f, 0.75f, 0.75f); }

    public void SetColorTo(Color color) { spriteRenderer.color = color; }
}
