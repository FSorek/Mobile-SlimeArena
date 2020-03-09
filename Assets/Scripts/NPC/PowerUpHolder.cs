using System;
using UnityEngine;

public class PowerUpHolder : MonoBehaviour, IPowerUpHolder
{
    private ITakeDamage entity;
    private IPowerUp heldPowerUp;

    private void Awake()
    {
        entity = GetComponent<ITakeDamage>();
        entity.Health.OnDeath += EntityOnDeath;
    }

    private void EntityOnDeath(GameObject source)
    {
        if(heldPowerUp == null)
            return;
        var absorber = source.GetComponent<IPowerUpAbsorber>();
        if (absorber != null)
        {
            absorber.Absorb(heldPowerUp);
            heldPowerUp = null;
        }
    }

    public void AddPowerUp(IPowerUp powerUp)
    {
        heldPowerUp = powerUp;
        
        if(entity != null)
            heldPowerUp.PickUpPower(entity);
    }
}