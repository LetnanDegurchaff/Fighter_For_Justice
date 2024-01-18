using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour 
{
    private List<T> _objects = new List<T>();
    
    public Pool(List<T> objects)
    {
        _objects = objects;
        Reset();
    }

    public void Reset()
    {
        foreach (var obj in _objects)
        {
            obj.gameObject.SetActive(false);
        }
    }

    public bool TrySpawnObject(Vector3 position)
    {
        T spawned = null;

        if (_objects.Count == 0)
            return false;
        
        foreach (var obj in _objects)
        {
            if (obj.gameObject.activeSelf == false)
            {
                obj.transform.position = position;
                obj.gameObject.SetActive(true);
                Debug.Log("создал");
                
                return true;
            }            
        }
        
        return false;
    }

    public void Clear()
    {
        foreach (var obj in _objects)
            Object.Destroy(obj.gameObject);

        _objects = new List<T>();
    }
}