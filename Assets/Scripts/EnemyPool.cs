using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _minXBound = -2f;
    [SerializeField] private float _maxXBound = 0.5f;
    [SerializeField] private float _minZBound = -1f;
    [SerializeField] private float _maxZBound = 41f;
    [SerializeField] private float _spawnRate = 5f;
    [SerializeField] private bool _collectionCheck = true;
    [SerializeField] private int _defaultCapacity = 100;
    [SerializeField] private int _maxSize = 100;
    [SerializeField] private int _maxActiveCount = 5;
    [SerializeField] private float _destroyDelay = 2f;

    [Inject] private DiContainer _diContainer;

    private ObjectPool<Enemy> _pool;
    private List<Enemy> _activeEnemies = new();

    public ObjectPool<Enemy> Pool => _pool;
    public List<Enemy> ActiveEnemies => _activeEnemies;

    private void OnEnable()
    {
         _pool = new(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, 
             _collectionCheck, _defaultCapacity, _maxSize);
        StartCoroutine(Spawn());
    }

    private void GetEnemy()
    {
        _pool.Get();
    }

    private Enemy CreatePooledItem()
    {
        return _diContainer.InstantiatePrefabForComponent<Enemy>(_enemyPrefab);
    }

    private void OnReturnedToPool(Enemy enemy)
    {
        Debug.Log(3);
        _activeEnemies.Remove(enemy);
        StartCoroutine(DestroyEnemy(enemy));
    }

    private void OnTakeFromPool(Enemy enemy)
    {
        enemy.transform.position = new Vector3(Random.Range(_minXBound, _maxXBound), 0f, Random.Range(_minZBound, _maxZBound));
        _activeEnemies.Add(enemy);
        enemy.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_spawnRate);

        while (true)
        {
            if (_pool.CountActive == _maxActiveCount)
                yield return null;
            else 
            {
                yield return wait;
                GetEnemy();
            }
        }
    }

    private IEnumerator DestroyEnemy(Enemy enemy)
    {
        yield return new WaitForSeconds(_destroyDelay);
        enemy.Reset();
        enemy.gameObject.SetActive(false);
    }
}
