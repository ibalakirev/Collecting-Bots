using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ScannerResuorces), typeof(SpawnerBots))]

public class BaseBots : MonoBehaviour
{
    private ScannerResuorces _scannerResuorces;
    private SpawnerBots _spawnerBots;
    private ResourceData _resourceData;
    private int _startCountBots = 3;
    private int _resourceCount = 0;
    private float _spawnRadius = 3f;

    private List<Unit> _bots = new List<Unit>();

    public event UnityAction<int> ResourcesChanged;

    private void Start()
    {
        _scannerResuorces = GetComponent<ScannerResuorces>();
        _spawnerBots = GetComponent<SpawnerBots>();
        _resourceData = FindAnyObjectByType<ResourceData>();

        CreateBots(_startCountBots);

        StartCoroutine(CollectResourcesRoutine());
    }

    public void TakeResource(Resource resource)
    {
        _resourceCount++;

        ResourcesChanged?.Invoke(_resourceCount);

        _resourceData.ReleaseResource(resource);
    }

    private IEnumerator CollectResourcesRoutine()
    {
        float delay = 0.2f;

        WaitForSeconds waitSeconds = new WaitForSeconds(delay);

        while (enabled)
        {
            CollectFreeResources();

            yield return waitSeconds;
        }
    }

    private void CollectFreeResources()
    {
        int minValue = 0;

        List<Resource> availableResources = _scannerResuorces.GetAllResources()
       .Where(resource => !_resourceData.IsResourceBusy(resource))
       .ToList();

        if (availableResources.Count > minValue)
        {
            Resource resource = availableResources.First();

            foreach (Unit bot in _bots)
            {
                if (bot.IsBusy == false)
                {
                    _resourceData.OccupyResource(resource);

                    bot.SetTargetPosition(resource);

                    break;
                }
            }
        }
    }

    private void CreateBots(int startBotsCount)
    {
        for (int i = 0; i < startBotsCount; i++)
        {
            float randomAxisX = Random.Range(-_spawnRadius, _spawnRadius);
            float randomAxisZ = Random.Range(-_spawnRadius, _spawnRadius);
            float axisY = 0;


            Vector3 randomPosition = transform.position + new Vector3(randomAxisX, axisY, randomAxisZ);

            Unit bot = _spawnerBots.Create(randomPosition, transform.rotation);

            bot.SetBaseBot(this);

            _bots.Add(bot);
        }
    }
}

