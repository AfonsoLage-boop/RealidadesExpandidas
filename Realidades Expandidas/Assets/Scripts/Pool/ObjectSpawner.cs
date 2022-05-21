using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private SpawnableObjectStatsSO spawnableObjectsStats;
    [SerializeField] private ObjectPoolCreator marionettePool;
    [SerializeField] private ObjectPoolCreator powerUpPool;
    [Range(0f,100f)] [SerializeField] private float powerUpChance;
    [SerializeField] private Animator[] doorAnims;
    [SerializeField] private Animator pauseToGameplayAnim;

    [Header("Force spawn for tests")]
    [Tooltip("Marionette on pool position index 0 will always spawn.")]
    [SerializeField] private bool forceFirstMarionetteSpawn;

    // Minimum distance
    [SerializeField] private GameObject minimumDistanceGameObject;
    private float minimumDistance;

    private float timePassedMarionetteTry;

    // Positions
    private IList<Transform> powerUpPositions;
    private IList<Transform> marionettePositions;

    public bool InInitialMenu { get; private set; }
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        InInitialMenu = true;
        IsPaused = true;
        marionettePositions = new List<Transform>();
        powerUpPositions = new List<Transform>();
        minimumDistance = 
            Vector3.Distance(transform.position, minimumDistanceGameObject.transform.position);
    }

    private void Start()
    {
        timePassedMarionetteTry = -3;

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

    public void PauseSpawn(bool value) => IsPaused = value;
    public void InitialMenu(bool value) => InInitialMenu = value;

    public void SpawnOneMarionette()
    {
        if (Time.time < timePassedMarionetteTry + 3) return;

        Transform randomTransform;
        int randomIndex;
        System.Random rand = new System.Random();

        randomTransform = marionettePositions[rand.Next(0, marionettePositions.Count)];
        randomIndex = rand.Next(0, (int)marionettePool.Pool.PoolCount);

        marionettePool.Pool.InstantiateFromPool(
            randomIndex, randomTransform.position, randomTransform.rotation);

        timePassedMarionetteTry = Time.time;

        foreach (Animator anim in doorAnims)
            anim.SetTrigger("Open");
    }

    public void StartGame()
    {
        foreach(GameObject marionette in 
            GameObject.FindGameObjectsWithTag("MarionetteMatchGameObject"))
        {
            marionette.SetActive(false);
        }

        foreach (Animator anim in doorAnims)
            anim.SetTrigger("Open");

        pauseToGameplayAnim.SetTrigger("Count");
    }

    private IEnumerator SpawnObjectCoroutine()
    {
        Transform randomTransform;
        int randomIndex;
        System.Random rand = new System.Random();
        float currentTimeToSpawn = 0;

        do
        {
            while (InInitialMenu)
                yield return null;

            while (IsPaused)
                yield return null;

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

            while (currentTimeToSpawn < spawnableObjectsStats.DefaultSpawnDelay)
            {
                if (IsPaused == false)
                {
                    currentTimeToSpawn += Time.deltaTime;
                }
                yield return null;
            }
            currentTimeToSpawn = 0;

            // Safe distance to spawn next object
            if (spawnedObj.activeSelf)
            {
                while (Vector3.Distance(randomTransform.transform.position,
                    spawnedObj.transform.position) < minimumDistance)
                {
                    if (spawnedObj.activeSelf == false) break;

                    while (IsPaused)
                        yield return null;

                    yield return null;
                }
            }

        } while (true);
    }
}
