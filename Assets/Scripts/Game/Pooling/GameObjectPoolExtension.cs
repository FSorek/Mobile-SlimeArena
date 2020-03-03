using UnityEngine;

public static class GameObjectPoolExtension
{
    public static void ReturnToPool(this GameObject obj)
    {
        obj.GetComponent<IGameObjectPooled>()?.Pool.ReturnToPool(obj);
    }
}