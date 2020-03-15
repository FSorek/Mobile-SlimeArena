using UnityEngine;

public interface IPowerUp
{
    void PickUpPower(GameObject entity);
    void Absorb(GameObject entity);
    ParticleSystem ParticleSystem { get; set; }
}