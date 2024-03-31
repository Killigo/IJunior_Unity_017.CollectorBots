using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected Transform Container;
    [SerializeField] protected GameObject Prefab;

    protected Queue<GameObject> Pool;

    public IEnumerable<GameObject> PooledObject => Pool;

    private void Awake()
    {
        Pool = new Queue<GameObject>();
    }

    public GameObject GetObject()
    {
        if (Pool.Count == 0)
        {
            GameObject poolObject = Instantiate(Prefab);
            poolObject.transform.parent = Container;

            return poolObject;
        }

        GameObject returnObject = Pool.Dequeue();
        returnObject.SetActive(true);

        return returnObject;
    }

    public void PutObject(GameObject poolObject)
    {
        Pool.Enqueue(poolObject);
        poolObject.SetActive(false);
    }

    public void Reset()
    {
        Pool.Clear();
    }
}
