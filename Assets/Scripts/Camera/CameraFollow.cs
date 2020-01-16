using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private bool canFollow;

    private void Update()
    {
        if(!canFollow)
            return;
        var position = target.position;
        transform.position = new Vector3(position.x, position.y, -10);
    }

    public void AllowFollow(bool value)
    {
        canFollow = value;
    }
}
