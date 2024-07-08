using UnityEngine;

public class SpawnerBots : MonoBehaviour
{
    [SerializeField] private Unit _botPrefab;
    [SerializeField] private SpawnerBases _spawnerBases;

    public Unit Create(Vector3 randomPosition)
    {
        Unit unit = Instantiate(_botPrefab, randomPosition, transform.rotation);

        unit.Initialize(_spawnerBases);

        return unit;
    }
}
