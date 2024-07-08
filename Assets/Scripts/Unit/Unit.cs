using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Resource _carriedResource;
    private BaseBots _baseBots;
    private SpawnerBases _spawnerBases;
    private Flag _targetFlag;
    private float _moveSpeed = 18f;
    private float _carryDistance = 0.5f;

    public bool IsBusy { get; private set; } = false;

    public void Initialize(SpawnerBases spawnerBases)
    {
        _spawnerBases = spawnerBases;
    }

    public void SetTargetPosition(Component targetComponent)
    {
        if (targetComponent == null)
        {
            return;
        }

        EnableStatusBusy();

        if (targetComponent is Resource resource)
        {
            _carriedResource = resource;

            StartCoroutine(MoveToTarget(resource.transform, TakeResource));
        }
        else if (targetComponent is Flag flag)
        {
            _targetFlag = flag;

            StartCoroutine(MoveToTarget(flag.transform, CreateNewBase));
        }
    }

    public void SetBaseBot(BaseBots baseBots)
    {
        _baseBots = baseBots;
    }

    public void DisableStatusBusy()
    {
        IsBusy = false;
    }

    private void EnableStatusBusy()
    {
        IsBusy = true;
    }

    private void CreateNewBase()
    {
        _baseBots.Reset—ounterMovingFlag();

        _baseBots.RemoveFlag();

        Vector3 newBasePosition = new Vector3(_targetFlag.transform.position.x, 1.01f, _targetFlag.transform.position.z);

        BaseBots newBase = _spawnerBases.Create(newBasePosition);

        newBase.SetUnitCreated();

        _baseBots.DetachUnit(this);
        _baseBots = newBase;

        newBase.AddBot(this);

        _targetFlag = null;

        DisableStatusBusy();
    }

    private void TakeResource()
    {
        if (_carriedResource == null)
        {
            return;
        }

        _carriedResource.transform.SetParent(transform);
        _carriedResource.transform.localPosition = Vector3.forward * _carryDistance;
        _carriedResource.transform.localRotation = Quaternion.identity;

        StartCoroutine(MoveToTarget(_baseBots.transform, DropResource));
    }

    private void DropResource()
    {
        if (_carriedResource == null)
        {
            return;
        }

        _carriedResource.transform.SetParent(null);
        _baseBots.TakeResource(_carriedResource);
        _carriedResource.Release();
        _carriedResource = null;

        DisableStatusBusy();
    }

    private IEnumerator MoveToTarget(Transform target, Action onComplete)
    {
        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);

            yield return null;
        }

        onComplete();
    }
}
