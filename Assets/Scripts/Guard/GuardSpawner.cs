
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GuardSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _spawnBounds;
    [SerializeField] private GameObject _guardPrefab;
    [SerializeField] private float _delayBeforeFirstSpawn;
    [SerializeField] private float _minDelayBetweenSpawns, _maxDelayBetweenSpawns;
    [SerializeField] private int _maxGuards;
    [SerializeField] private int _initialGuards;

    private int _guardCount = 0;
    private IEnumerator Start()
    {
        
        for (int i = 0; i < _initialGuards; i++)
        {
            SpawnGuard();
        }
        
        yield return new WaitForSeconds(_delayBeforeFirstSpawn);

        // _guardCount = transform.childCount;
        
        while (_guardCount < _maxGuards)
        {
            SpawnGuard();
            yield return new WaitForSeconds(Random.Range(_minDelayBetweenSpawns, _maxDelayBetweenSpawns));
        }
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, ((Vector3)_spawnBounds + Vector3.forward) * 2f);
    }

    private void SpawnGuard()
    {
        NavMeshHit hit;
        Vector3 pos;
        do
        {
            pos = new Vector3(
                Random.Range(-_spawnBounds.x, _spawnBounds.x),
                Random.Range(-_spawnBounds.y, _spawnBounds.y),
                _guardPrefab.transform.position.z
            );

        }
        while(IsInView(pos) ||
              !NavMesh.SamplePosition(pos, out hit, .3f, NavMesh.AllAreas));

        Instantiate(_guardPrefab, hit.position, Quaternion.identity, transform);
        _guardCount++;
        Debug.Log($"Guardia Spawneado en {hit.position}");
    }

    private bool IsInView(Vector3 pos)
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(pos);

        return (viewportPos.x >= 0 && viewportPos.x <= 1 &&
                viewportPos.y >= 0 && viewportPos.y <= 1);
    }
}
