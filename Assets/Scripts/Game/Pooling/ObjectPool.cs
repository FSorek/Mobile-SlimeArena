using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int prespawnAmount = 0;
    private Queue<GameObject> objects = new Queue<GameObject>();


    private void Start()
    {
        AddObjects(prespawnAmount);
    }

    public GameObject Get()
    {
        if (objects.Count == 0)
        {
            AddObjects(1);
        }

        return objects.Dequeue();
    }

    public GameObject ReadAssignedPrefab()
    {
        return prefab;
    }

    private void AddObjects(int v)
    {
        for (int i = 0; i < v; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.gameObject.SetActive(false);
            objects.Enqueue(obj);
            obj.GetComponent<IGameObjectPooled>().Pool = this;
            obj.transform.SetParent(this.transform);
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }
}