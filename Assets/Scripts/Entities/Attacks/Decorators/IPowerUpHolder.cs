using System;
using UnityEngine;

public interface IPowerUpHolder
{
    void AddPowerUp(IPowerUp powerUp, ParticleSystem particleEffect);
}