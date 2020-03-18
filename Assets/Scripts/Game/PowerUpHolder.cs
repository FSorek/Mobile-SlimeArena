using UnityEngine;

public class PowerUpHolder : MonoBehaviour, IPowerUpHolder
{
    public IPowerUp HeldPowerUp { get; private set; }
    public void AddPowerUp(IPowerUp power)
    {
        HeldPowerUp = power;
        power.Carry(gameObject);
        var effect = power.Effect;
        effect.transform.SetParent(transform);
        effect.transform.localPosition = Vector3.zero;
        effect.SetActive(true);
    }
}