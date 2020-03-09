using UnityEngine;

public interface IPowerUp
{
    void PickUpPower(ITakeDamage entity);
    void Absorb(ITakeDamage entity);
}