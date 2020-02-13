using System;

public class ProjectilePool : ObjectPool
{
    public static ProjectilePool Instance;

    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);
        Instance = this;
    }
}