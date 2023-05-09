using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] float height;

    private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y + height, target.position.z);
    }
}