using System.Collections;
using UnityEngine;

public class SpawnerResources : Spawner<ResourcesPool>
{
    private float _minPositionAxis = -25.0f;
    private float _maxPositionAxis = 25.0f;
    private float _positionAxisY = 0.5f;

    private void Start()
    {
        StartCoroutine(GenerateResource());
    }

    private IEnumerator GenerateResource()
    {
        float delay = 0.09f;

        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            Create();

            yield return wait;
        }
    }

    private void Create()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(_minPositionAxis, _maxPositionAxis),
            _positionAxisY, Random.Range(_minPositionAxis, _maxPositionAxis));

        Resource resource = ObjectsPool.GetObject(spawnPoint, transform.rotation);

        resource.Released += ReleaseResource;
    }

    private void ReleaseResource(Resource resource)
    {
        resource.Released -= ReleaseResource;

        ObjectsPool.ReturnObject(resource);
    }
}
