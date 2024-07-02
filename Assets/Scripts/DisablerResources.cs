using UnityEngine;

public class DisablerResources : MonoBehaviour
{
    [SerializeField] private SpawnerResources _spawnerResources;
    [SerializeField] private ResourcesPool _resourcesPool;

    private void OnEnable()
    {
        _spawnerResources.ResourceReleased += ReturnResourceInPool;
    }

    private void OnDisable()
    {
        _spawnerResources.ResourceReleased -= ReturnResourceInPool;
    }

    private void ReturnResourceInPool(Resource resource)
    {
        _resourcesPool.ReturnObject(resource);
    }
}
