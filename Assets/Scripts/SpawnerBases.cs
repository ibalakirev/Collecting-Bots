using UnityEngine;

public class SpawnerBases : MonoBehaviour
{
    [SerializeField] private BaseBots _baseBots;
    [SerializeField] private ResourceData _resourceData;
    [SerializeField] private SpawnerBots _spawnerBots;

    private void Start()
    {
        Create(transform.position);
    }

    public BaseBots Create(Vector3 randomPosition)
    {
        BaseBots baseBots = Instantiate(_baseBots, randomPosition, transform.rotation);

        baseBots.Initialize(_resourceData, _spawnerBots);

        return baseBots;
    }
}
