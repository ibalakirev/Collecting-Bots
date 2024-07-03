using System.Collections;
using UnityEngine;

public class SpawnerResources : ResourcesPool
{
    private float _minPositionAxis = -20.0f;
    private float _maxPositionAxis = 20.0f;
    private float _positionAxisY = 0.5f;

    private void Start()
    {
        StartCoroutine(GenerateResource());
    }

    private IEnumerator GenerateResource()
    {
        float delay = 1f;

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

        Resource resource = GetObject(spawnPoint, transform.rotation);

        resource.Released += ReleaseResource;
    }

    private void ReleaseResource(Resource resource)
    {
        resource.Released -= ReleaseResource;

        ReturnObject(resource);
    }
}
