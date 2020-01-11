using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        var position = target.position;
        transform.position = new Vector3(position.x, position.y, -10);
    }
}
