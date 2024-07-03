using System.Collections.Generic;
using UnityEngine;

public class ResourceData : MonoBehaviour
{
    private Dictionary<Resource, bool> _resourceStates = new Dictionary<Resource, bool>();

    public bool IsResourceBusy(Resource resource)
    {
        if(_resourceStates.TryGetValue(resource, out bool isBusy) == false)
        {
            _resourceStates[resource] = false;
        }

        return isBusy;
    }

    public void OccupyResource(Resource resource)
    {
        _resourceStates[resource] = true;
    }

    public void ReleaseResource(Resource resource)
    {
        _resourceStates[resource] = false;
    }
}
