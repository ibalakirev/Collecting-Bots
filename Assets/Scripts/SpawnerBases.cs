using UnityEngine;

public class SpawnerBases : MonoBehaviour
{
    [SerializeField] private BaseBots _baseBots;
    [SerializeField] private ResourceData _resourceData;

    private void Start()
    {
        Create(transform.position);
    }

    public void Create(Vector3 randomPosition)
    {
        BaseBots baseBots = Instantiate(_baseBots, randomPosition, transform.rotation);

        baseBots.Initialize(_resourceData);
    }
}
