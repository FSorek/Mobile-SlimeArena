using UnityEngine;

public interface IPowerUp
{
    void Carry(GameObject carrier);
    void Apply(GameObject absorber);
    GameObject Effect { get; set; }
}