using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    public string destinationColony;
    private void OnLevelWasLoaded(int level)
    {
        if (PlayerData.Instance.currentColonyName == destinationColony)
        {
            Debug.LogWarning("Workou");
        }
    }
}
