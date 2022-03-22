using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for creating damage hit pools.
/// </summary>
public class ObjectPoolCreator : MonoBehaviour
{
    /// <summary>
    /// Prefab of the damage hit prefab.
    /// </summary>
    [SerializeField] private List<BasePool> pool;

    // IList with pool for every base pool
    private IList<BasePool> listOfGameObjects;

    public static ObjectPool Pool { get; private set; }

    private void Awake()
    {
        GameObject poolParent = new GameObject();

        // Creates a dictionary with gameobject name
        Pool = new ObjectPool(new Dictionary<string, Queue<GameObject>>());

        // Creates a list for prefabs or hits/muzzles
        listOfGameObjects = new List<BasePool>();

        // Foreach pool
        for (int i = 0; i < pool.Count; i++)
        {
            BasePool spawnedGameObject = new BasePool(pool[i].Name, pool[i].Prefab, pool[i].Size);
            listOfGameObjects.Add(spawnedGameObject);
        }

        Pool.CreatePool(poolParent, listOfGameObjects);
    }
}