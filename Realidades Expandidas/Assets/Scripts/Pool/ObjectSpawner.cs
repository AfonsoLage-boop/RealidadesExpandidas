using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private SpawnableObjectStatsSO spawnableObjectsStats;
    private YieldInstruction wfs;
    private ObjectPoolCreator objectPool;

    // Minimum distance
    [SerializeField] private GameObject minimumDistanceGameObject;
    private float minimumDistance;

    // Positions
    private IList<Transform> positions;

    private void Awake()
    {
        wfs = new WaitForSeconds(spawnableObjectsStats.DefaultSpawnDelay);
        objectPool = FindObjectOfType<ObjectPoolCreator>();
        positions = new List<Transform>();
        minimumDistance = 
            Vector3.Distance(transform.position, minimumDistanceGameObject.transform.position);
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

            GameObject spawnedObj = objectPool.Pool.InstantiateFromPool(
                randomIndex, randomTransform.position, randomTransform.rotation);

            yield return wfs;

            // Safe distance to spawn next object
            if (spawnedObj.activeSelf)
            {
                while (Vector3.Distance(randomTransform.transform.position,
                    spawnedObj.transform.position) < minimumDistance)
                {
                    if (spawnedObj.activeSelf == false) break;

                    yield return null;
                }
            }

        } while (true);
    }
}
