using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _objectsPool;

    protected T ObjectsPool => _objectsPool;
}
