using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private SpawnableObjectStatsSO spawnableObjectsStats;
    private YieldInstruction wfs;
    private ObjectPoolCreator objectPool;

    // Positions
    private IList<Transform> positions;

    private void Awake()
    {
        wfs = new WaitForSeconds(spawnableObjectsStats.DefaultSpawnDelay);
        objectPool = FindObjectOfType<ObjectPoolCreator>();
        positions = new List<Transform>();
    }

    private void Start()
    {
        foreach (Transform childTransform in transform)
        {
            positions.Add(childTransform);
        }

        StartCoroutine(SpawnObjectCoroutine());
    }

    private IEnumerator SpawnObjectCoroutine()
    {
        Transform randomTransform;
        int randomIndex;
        System.Random rand = new System.Random(5);
        
        do
        {
            randomTransform = positions[rand.Next(0, positions.Count)];
            randomIndex = rand.Next(0, (int)objectPool.Pool.PoolCount);

            //objectPool.Pool.InstantiateFromPool(
            //    "SlowTime", randomTransform.position, randomTransform.rotation);

            GameObject spawnedObj = objectPool.Pool.InstantiateFromPool(
                randomIndex, randomTransform.position, randomTransform.rotation);

            yield return wfs;

            // Safe distance to spawn next object
            if (spawnedObj.activeSelf)
            {
                while (Vector3.Distance(randomTransform.transform.position,
                    spawnedObj.transform.position) < 7f)
                {
                    if (spawnedObj.activeSelf == false) break;

                    yield return null;
                }
            }

        } while (true);
    }
}
