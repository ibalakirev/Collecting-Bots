    using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefabObject;

    private List<T> _objects = new List<T>();

    public void Initialize()
    {
        T newObject = Instantiate(_prefabObject);

        AddObject(newObject);

        DeactiveObject(newObject.gameObject);
    }

    public T GetObject(Vector3 targetPosition, Quaternion rotation)
    {
        TryExpand();

        T newObject = _objects[_objects.Count - 1];

        newObject.transform.position = targetPosition;
        newObject.transform.rotation = rotation;

        ActiveObject(newObject.gameObject);

        _objects.Remove(newObject);

        return newObject;
    }

    public void ReturnObject(T obj)
    {
        DeactiveObject(obj.gameObject);

        AddObject(obj);
    }

    private void TryExpand()
    {
        float minValue = 0;

        if (_objects.Count <= minValue)
        {
            T instantiateObject = Instantiate(_prefabObject);

            instantiateObject.name = _prefabObject.name;

            DeactiveObject(instantiateObject.gameObject);

            AddObject(instantiateObject);
        }
    }

    private void AddObject(T obj)
    {
        _objects.Add(obj);
    }

    private void ActiveObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void DeactiveObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
