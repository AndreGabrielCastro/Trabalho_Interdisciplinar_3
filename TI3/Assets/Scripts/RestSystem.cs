using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSystem : MonoBehaviour
{
    private float time = 1.5f;
    private bool isFadingIn;
    private void FixedUpdate()
    {
        if (isFadingIn == true)
        {
            time -= Time.fixedDeltaTime;

            if (time <= 0)
            {
                UIUserInterface.Instance.uiFader.FadeOut();
                UITaskMenuSystem.Instance.ReGenerateTasks();
                UITaskMenuSystem.Instance.ResetTaskDescription();
                PlayerSystem.Instance.DecrementTasksTime();
                time = 1.5f; isFadingIn = false;
            }
        }
    }
    public void OnButtonRest()
    {
        if (isFadingIn == true) { UIUserInterface.Instance.PopResult("Can't do this now!", Color.red); return; }
        UIUserInterface.Instance.uiFader.FadeIn(); // Activates the fade in
        isFadingIn = true;
    }
}
