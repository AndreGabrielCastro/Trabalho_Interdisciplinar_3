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
                PlayerSystem.Instance.HealEverything(20, 10);
                time = 1.5f; isFadingIn = false;
            }
        }
    }
    public void OnButtonRest()
    {
        if (isFadingIn == true)
        { UIUserInterface.Instance.PopResult("Can't do this now!", Color.red); return; }

        if (PlayerSystem.Instance.AreTaskDeliveriesPlaced() == false)
        { UIUserInterface.Instance.PopResult("Place all task's deliveries first", Color.red); return; }

        UIUserInterface.Instance.uiFader.FadeIn(); // Activates the fade in
        isFadingIn = true;
    }
}
