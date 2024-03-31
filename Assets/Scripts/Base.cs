using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Robot[] _robots;

    private Queue<GameObject> _resources;

    private void Awake()
    {
        _resources = new Queue<GameObject>();
    }

    private void Update()
    {
        foreach (var robot in _robots)
        {
            if (robot.IsIdle && _resources.Count > 0)
                robot.SetResource(_resources.Dequeue());
        }
    }

    public void AddResource(GameObject resource)
    {
        _resources.Enqueue(resource);
    }
}
