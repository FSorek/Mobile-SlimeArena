using System;
using UnityEngine;

public class PowerUpHolder : MonoBehaviour, IPowerUpHolder
{
    private ITakeDamage entity;
    private IPowerUp heldPowerUp;
    private ParticleSystem currentParticleEffect;

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
            if(currentParticleEffect.GetComponent<PooledGameObject>() != null)
                currentParticleEffect.gameObject.ReturnToPool();
            
            heldPowerUp = null;
        }
    }

    public void AddPowerUp(IPowerUp powerUp, ParticleSystem particleEffect)
    {
        heldPowerUp = powerUp;
        currentParticleEffect = particleEffect;
        particleEffect.transform.position = transform.position;
        particleEffect.transform.SetParent(transform);
        particleEffect.gameObject.SetActive(true);
        
        if(entity != null)
            heldPowerUp.PickUpPower(entity);
    }
}