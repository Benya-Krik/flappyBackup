using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierGenerator : ObjectPool
{
    [SerializeField] private GameObject _template;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private float _maxSpawnPositionY;
    [SerializeField] private float _minSpawnPositionY;

    private float _spawnTime = 0;

    private void Start()
    {
        Initialized(_template);
    }

    private void Update()
    {
        _spawnTime += Time.deltaTime;

        if(_spawnTime > _secondsBetweenSpawn )
        {
            if(TryGetObject(out GameObject wall))
            {
                _spawnTime = 0;

                float spawnPositionY = Random.Range(_minSpawnPositionY, _maxSpawnPositionY);
                Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);
                wall.SetActive(true);
                wall.transform.position = spawnPoint;

                DisableObjectBehindViewport();
            }
        }
    }
}
