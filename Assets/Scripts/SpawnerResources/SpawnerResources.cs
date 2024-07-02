using System;
using System.Collections;
using UnityEngine;

public class SpawnerResources : Spawner<ResourcesPool>
{
    private float _minPositionAxis = -20.0f;
    private float _maxPositionAxis = 20.0f;
    private float _positionAxisY = 0.5f;

    public event Action<Resource> ResourceReleased;

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
        Vector3 spawnPoint = new Vector3(UnityEngine.Random.Range(_minPositionAxis, _maxPositionAxis),
            _positionAxisY, UnityEngine.Random.Range(_minPositionAxis, _maxPositionAxis));

        Resource resource = ObjectsPool.GetObject(spawnPoint, transform.rotation);

        resource.Released += ReportAboutReleasedResource;
    }

    private void ReportAboutReleasedResource(Resource resource)
    {
        resource.Released -= ReportAboutReleasedResource;

        ResourceReleased?.Invoke(resource);
    }
}
