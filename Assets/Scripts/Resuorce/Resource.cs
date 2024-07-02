using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public event Action<Resource> Released;

    public void Release()
    {
        Released?.Invoke(this);
    }
}
