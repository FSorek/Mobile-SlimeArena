using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    public event Action<T> OnObjectCreated = delegate {  };
    [SerializeField] private T prefab;
    [SerializeField] private int prespawnAmount;
    private readonly Queue<T> objects = new Queue<T>();
    private Transform poolRoot;
    
    public static ObjectPool<T> Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        if (Instance.poolRoot == null)
            Instance.poolRoot = transform;
    }

    private void Start()
    {
        AddObjects(prespawnAmount);
    }

    public T Get()
    {
        if (objects.Count == 0) AddObjects(1);
        var obj = objects.Dequeue();
        return obj;
    }

    private void AddObjects(int v)
    {
        for (int i = 0; i < v; i++)
        {
            var obj = Instantiate(prefab, transform, true);
            OnObjectCreated(obj);
            obj.gameObject.SetActive(false);
            objects.Enqueue(obj);
        }
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }
}