using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private float _spawnDelay = 5;
    [SerializeField] private float _spawnRadius = 45;

    private void Start()
    {
        StartCoroutine(SpawnContinuously(_spawnDelay));
    }

    private IEnumerator SpawnContinuously(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SpawnResource();
        }
    }

    private void SpawnResource()
    {
        var positionX = Random.Range(transform.position.x - _spawnRadius, transform.position.x + _spawnRadius);
        var positionZ = Random.Range(transform.position.z - _spawnRadius, transform.position.z + _spawnRadius);
        var position = new Vector3(positionX, transform.position.y, positionZ);

        GameObject poolObject = _pool.GetObject();
        poolObject.transform.position = position;

        poolObject.GetComponent<Resource>().Destroyed += OnDestroyed;

        _base.AddResource(poolObject);
    }

    private void OnDestroyed(GameObject poolObject)
    {
        poolObject.transform.parent = _pool.transform;
        _pool.PutObject(poolObject);
        poolObject.GetComponent<Resource>().Destroyed -= OnDestroyed;
    }
}
