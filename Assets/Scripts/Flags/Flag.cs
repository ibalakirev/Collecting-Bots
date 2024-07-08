using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public event Action<Flag> Released;

    public void Release()
    {
        Released?.Invoke(this);
    }
}
