using System.Collections.Generic;
using UnityEngine;

public class ScannerResuorces : MonoBehaviour
{
    private float _radius = 100f;

    private void OnDrawGizmos()
    {
        Color colorGizmoz = Color.red;

        Gizmos.color = colorGizmoz;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public List<Resource> GetAllResources()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);

        List<Resource> resources = new List<Resource>();

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            if (overlappedColliders[i].TryGetComponent(out Resource resource))
            {
                resources.Add(resource);
            }
        }

        return resources;
    }
}
