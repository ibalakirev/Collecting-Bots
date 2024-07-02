using UnityEngine;

public class SpawnerBases : Spawner<BasesPool>
{
    private void Start()
    {
        Create(transform.position, transform.rotation);
    }

    public void Create(Vector3 taregtPosition, Quaternion rotation)
    {
        ObjectsPool.GetObject(taregtPosition, rotation);
    }
}
