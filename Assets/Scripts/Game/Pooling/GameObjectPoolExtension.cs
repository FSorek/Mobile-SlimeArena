using UnityEngine;

public static class GameObjectPoolExtension
{
    public static void ReturnToPool(this GameObject obj)
    {
        var pool = obj.GetComponent<IGameObjectPooled>()?.Pool;
        if (pool == null) 
            return;
        
        pool.ReturnToPool(obj);
        obj.transform.SetParent(pool.transform);
    }
}