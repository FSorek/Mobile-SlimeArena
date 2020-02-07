using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : EntitySound<Player>
{
    [SerializeField] private PlayerAudioData audioData;
    protected override void Subscribe()
    {
        owner.PlayerAttack.OnAttack += PlayerOnAttack;
        owner.PlayerAttack.OnTargetHit += PlayerOnTargetHit;
    }
    private void PlayerOnAttack(Vector2 obj)
    {
        if(audioData.AttackSound != null)
            audioSource.PlayOneShot(audioData.AttackSound);
    }
    private void PlayerOnTargetHit(ITakeDamage obj)
    {
        if(audioData.HitSound != null)
            audioSource.PlayOneShot(audioData.HitSound);
    }
}