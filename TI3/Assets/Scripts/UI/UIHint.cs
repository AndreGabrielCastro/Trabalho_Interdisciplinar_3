using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHint : MonoBehaviour
{
    [SerializeField] private bool keepActiveOnStart;
    private void Start()
    {
        UIHowToPlay.Instance.AddUIHint(this);
        if (keepActiveOnStart == true) { return; }
        gameObject.SetActive(false);
    }
}