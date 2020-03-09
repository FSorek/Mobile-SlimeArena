using UnityEngine;

public class PowerUpAbsorber : MonoBehaviour, IPowerUpAbsorber
{
    private ITakeDamage entity;

    private void Awake()
    {
        entity = GetComponent<ITakeDamage>();
    }

    public void Absorb(IPowerUp powerUp)
    {
        powerUp.Absorb(entity);
    }
}