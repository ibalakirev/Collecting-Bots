using UnityEngine;

public class SpawnerBots : MonoBehaviour
{
    [SerializeField] private Unit _botPrefab;

    public Unit Create(Vector3 randomPosition)
    {
        return Instantiate(_botPrefab, randomPosition, transform.rotation);
    }
}
