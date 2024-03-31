using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public event Action<GameObject> Destroyed;

    public void Destroy()
    {
        Destroyed?.Invoke(gameObject);
    }
}
