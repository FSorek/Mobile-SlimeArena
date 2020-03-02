using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySound : EntitySound<IEntityStateMachine>
{
    [SerializeField] private AudioData audioData;
    protected override void Subscribe()
    {
        owner.OnEntityStateChanged += EntityStateChanged;
    }

    private void EntityStateChanged(IState state)
    {
        if(state is EntityAttack && audioData.AttackSound != null)
            audioSource.PlayOneShot(audioData.AttackSound);
    }
}