using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateBasedSound : EntitySound<IEntityStateMachine>
{
    [SerializeField] private StateAudioData stateAudioData;
    protected override void Subscribe()
    {
        owner.OnEntityStateChanged += EntityStateChanged;
    }

    private void EntityStateChanged(IState state)
    {
        if(state is EntityAttack && stateAudioData.AttackSound != null)
            audioSource.PlayOneShot(stateAudioData.AttackSound);
    }
}