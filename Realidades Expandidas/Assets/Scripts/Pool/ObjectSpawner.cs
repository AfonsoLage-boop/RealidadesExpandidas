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

        do
        {
            randomTransform = positions[Random.Range(0, positions.Count)];

            objectPool.Pool.InstantiateFromPool("Low1", randomTransform.position, randomTransform.rotation);

            yield return wfs;

        } while (true);
    }
}
