using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private SpawnableObjectStatsSO spawnableObjectsStats;
    [SerializeField] private ObjectPoolCreator marionettePool;
    [SerializeField] private ObjectPoolCreator powerUpPool;
    [Range(0f,100f)] [SerializeField] private float powerUpChance;
    private YieldInstruction wfs;

    [Header("Force spawn for tests")]
    [Tooltip("Marionette on pool position index 0 will always spawn.")]
    [SerializeField] private bool forceFirstMarionetteSpawn;

    // Minimum distance
    [SerializeField] private GameObject minimumDistanceGameObject;
    private float minimumDistance;

    // Positions
    private IList<Transform> powerUpPositions;
    private IList<Transform> marionettePositions;

    private void Awake()
    {
        wfs = new WaitForSeconds(spawnableObjectsStats.DefaultSpawnDelay);
        marionettePositions = new List<Transform>();
        powerUpPositions = new List<Transform>();
        minimumDistance = 
            Vector3.Distance(transform.position, minimumDistanceGameObject.transform.position);
    }

    private void Start()
    {
        foreach (Transform childTransform in transform.GetChild(0))
        {
            marionettePositions.Add(childTransform);
        }
        foreach (Transform childTransform in transform.GetChild(1))
        {
            powerUpPositions.Add(childTransform);
        }

        StartCoroutine(SpawnObjectCoroutine());
    }

    private IEnumerator SpawnObjectCoroutine()
    {
        Transform randomTransform;
        int randomIndex;
        System.Random rand = new System.Random();
        
        do
        {
            GameObject spawnedObj;

            if (forceFirstMarionetteSpawn)
            {
                randomTransform = marionettePositions[0];
                randomIndex = 0;

                spawnedObj = marionettePool.Pool.InstantiateFromPool(
                    randomIndex, randomTransform.position, randomTransform.rotation);
            }
            else
            {
                if (rand.Next(0, 100) < powerUpChance)
                {
                    randomTransform = powerUpPositions[rand.Next(0, powerUpPositions.Count)];
                    randomIndex = rand.Next(0, (int)powerUpPool.Pool.PoolCount);

                    spawnedObj = powerUpPool.Pool.InstantiateFromPool(
                        randomIndex, randomTransform.position, randomTransform.rotation);
                }
                else
                {
                    randomTransform = marionettePositions[rand.Next(0, marionettePositions.Count)];
                    randomIndex = rand.Next(0, (int)marionettePool.Pool.PoolCount);

                    spawnedObj = marionettePool.Pool.InstantiateFromPool(
                        randomIndex, randomTransform.position, randomTransform.rotation);
                }
            }

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
