using System.Collections.Generic;
using UnityEngine;
public class Pool<T> where T : MonoBehaviour
{
    public T prefab { get; }
    public Transform container { get; }
    private List<T> pool;
    public Pool(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;

        CreatePool(count);
    }
    private void CreatePool(int count)
    {
        pool = new List<T>(count);
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }
    private T CreateObject(bool isActive = false)
    {
        var obj = Object.Instantiate(prefab, container);
        obj.gameObject.SetActive(isActive);
        pool.Add(obj);
        return obj;
    }
    public bool HasFreeElement(out T element)
    {
        foreach (var a in pool)
        {
            if (!a.gameObject.activeInHierarchy)
            {
                element = a;
                a.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetElement()
    {
        if (HasFreeElement(out var element))
            return element;
        return CreateObject(true);
    }
}
