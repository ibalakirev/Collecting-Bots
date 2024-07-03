using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Resource _carriedResource;
    private BaseBots _baseBot;
    private float _moveSpeed = 10f;
    private float _carryDistance = 0.5f;

    public bool IsBusy { get; private set; } = false;

    public void SetTargetPosition(Component targetComponent)
    {
        if (targetComponent == null)
        {
            return;
        }

        IsBusy = true;

        if (targetComponent is Resource resource)
        {
            _carriedResource = resource;

            StartCoroutine(MoveToTarget(resource.transform, TakeResource));
        }
    }

    public void SetBaseBot(BaseBots baseBots)
    {
        _baseBot = baseBots;
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

        StartCoroutine(MoveToTarget(_baseBot.transform, DropResource));
    }

    private void DropResource()
    {
        if (_carriedResource == null)
        {
            return;
        }

        _carriedResource.transform.SetParent(null);
        _baseBot.TakeResource(_carriedResource);
        _carriedResource.Release();
        _carriedResource = null;

        IsBusy = false;
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
