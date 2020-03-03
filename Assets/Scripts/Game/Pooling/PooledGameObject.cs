using UnityEngine;

public class PooledGameObject : MonoBehaviour, IGameObjectPooled
{
    public ObjectPool Pool { get; set; }
}