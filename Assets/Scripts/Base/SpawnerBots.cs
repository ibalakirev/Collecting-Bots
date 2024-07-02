using UnityEngine;

public class SpawnerBots : Spawner<BotsPool>
{
    public Unit Create(Vector3 taregtPosition, Quaternion rotation)
    {
        Unit unit = ObjectsPool.GetObject(taregtPosition, rotation);

        return unit;
    }
}
