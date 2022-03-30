using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private YieldInstruction wfs;
    private ObjectPoolCreator objectPool;

    // Positions
    private IList<Transform> positions;

    private void Awake()
    {
        wfs = new WaitForSeconds(2.5f);
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

        do
        {
            randomTransform = positions[Random.Range(0, positions.Count)];
            randomIndex = (int)Random.Range(0, objectPool.Pool.PoolCount);

            //objectPool.Pool.InstantiateFromPool(
            //    "SlowTime", randomTransform.position, randomTransform.rotation);

            objectPool.Pool.InstantiateFromPool(
                randomIndex, randomTransform.position, randomTransform.rotation);

            yield return wfs;

        } while (true);
    }
}
