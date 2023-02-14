using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : MonoBehaviour
{
    public void OnButtonPause() { Time.timeScale = 0.1f; }
    public void OnButtonUnPause() { Time.timeScale = 1; }
}
