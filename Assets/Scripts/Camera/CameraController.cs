using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera camera;
    private Player player;

    private void Awake()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        player = FindObjectOfType<Player>();
        player.GetComponent<IEntityStateMachine>().OnEntityStateChanged += OnPlayerStateChanged;
    }

    private void OnPlayerStateChanged(IState state)
    {
        if (state is EntityCastingAbility)
            camera.Follow = null;
        else if (camera.Follow == null)
            camera.Follow = player.transform;       
    }
}
