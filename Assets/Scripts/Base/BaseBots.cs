using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ScannerResuorces))]

public class BaseBots : MonoBehaviour
{
    private ScannerResuorces _scannerResuorces;
    private SpawnerBots _spawnerBots;
    private ResourceData _resourceData;

    private int _counterMovingFlag = 0;
    private int _startCountBots = 3;
    private int _resourceCount = 0;
    private float _spawnRadius = 3f;
    private int _resourcesForNewBase = 5;
    private int _resourcesForNewBot = 3;

    private bool _isFlagPlaced = false;
    private bool _isCreatedUnit = false;

    private Flag _flag;
    private List<Unit> _bots = new List<Unit>();

    public event UnityAction<int> ResourcesChanged;

    private void Start()
    {
        _scannerResuorces = GetComponent<ScannerResuorces>();

        if (_isCreatedUnit == false)
        {
            CreateBots(_startCountBots);
        }

        StartCoroutine(CollectResourcesRoutine());
    }

    public void Initialize(ResourceData resourceData, SpawnerBots spawnerBots)
    {
        _resourceData = resourceData;
        _spawnerBots = spawnerBots;
    }

    public void SetFlagPlaced()
    {
        _isFlagPlaced = false;
    }

    public void AddBot(Unit bot)
    {
        if (bot != null && _bots.Contains(bot) == false)
        {
            _bots.Add(bot);
        }
    }

    public void SetUnitCreated()
    {
        _isCreatedUnit = true;
    }

    public void TakeResource(Resource resource)
    {
        _resourceCount++;

        _resourceData.ReleaseResource(resource);

        ResourcesChanged?.Invoke(_resourceCount);
    }

    public void SetFlag(Flag flag)
    {
        _flag = flag;

        _isFlagPlaced = true;
    }

    public void RemoveFlag()
    {
        _isFlagPlaced = false;

        _flag.Release();

        _flag = null;   
    }

    public void DetachUnit(Unit unit)
    {
        if (unit != null && _bots.Contains(unit))
        {
            _bots.Remove(unit);

            unit.SetBaseBot(null);

            unit.DisableStatusBusy();
        }
    }

    public void Reset—ounterMovingFlag()
    {
        _counterMovingFlag = 0;
    }

    private IEnumerator CollectResourcesRoutine()
    {
        float delay = 0.2f;

        WaitForSeconds waitSeconds = new WaitForSeconds(delay);

        while (enabled)
        {
            yield return waitSeconds;

            AllocateResources();
        }
    }

    private void AllocateResources()
    {
        if (_isFlagPlaced)
        {
            CreateNewBase();
        }
        else
        {
            CreateNewBot();
        }

        CollectFreeResources();
    }

    private void CollectFreeResources()
    {
        int minValue = 0;

        List<Resource> availableResources = _scannerResuorces.GetAllResources()
       .Where(resource => _resourceData.IsBotBusy(resource) == false)
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

    private void CreateNewBase()
    {
        int maxValue = 1;

        if (_resourceCount >= _resourcesForNewBase && _counterMovingFlag < maxValue)
        {
            foreach (Unit bot in _bots)
            {
                if (bot.IsBusy == false)
                {
                    bot.SetTargetPosition(_flag);

                    _counterMovingFlag++;

                    _resourceCount -= _resourcesForNewBase;

                    ResourcesChanged?.Invoke(_resourceCount);

                    break;
                }
            }
        }
    }

    private void CreateNewBot()
    {
        if (_resourceCount >= _resourcesForNewBot)
        {
            int countNewBots = 1;

            _resourceCount -= _resourcesForNewBot;

            ResourcesChanged?.Invoke(_resourceCount);

            CreateBots(countNewBots);
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

            Unit bot = _spawnerBots.Create(randomPosition);

            bot.SetBaseBot(this);

            _bots.Add(bot);
        }
    }
}

