using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Generic struct that creates -T- pools of gameobjects.
/// </summary>
public struct ObjectPool
{
    private readonly IDictionary<string, Queue<GameObject>> poolDictionary;

    public float PoolCount => poolDictionary.Count;

    public ObjectPool(IDictionary<string, Queue<GameObject>> poolDictionary)
    {
        this.poolDictionary = poolDictionary;
    }

    /// <summary>
    /// Creates pools for every pool and adds them to a dictionary with all pools.
    /// </summary>
    /// <param name="parent">Gameobject that created this instance (used to determin the 
    /// gameobject parent).</param>
    /// <param name="pools">List with pools</param>
    public void CreatePool(GameObject parent, IList<BasePool> pools)
    {
        for (int i = 0; i < pools.Count; i++)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int j = 0; j < pools[i].Size; j++)
            {
                // Spawns at 5000,5000,5000 so it doesn't mess with initial points like Vector3.zero
                GameObject obj = MonoBehaviour.Instantiate(
                    pools[i].Prefab, new Vector3(5000, 5000, 5000), Quaternion.identity);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.parent = parent.transform;
            }

            try
            {
                poolDictionary.Add(pools[i].Name, objectPool);
            }
            catch (System.ArgumentException)
            {
                Debug.Log(pools[i].Name + " name already exists on another gameobject. Change name.");
            }
        }
    }

    /// <summary>
    /// Spawns an object from a pool.
    /// </summary>
    /// <param name="name">Name of the object.</param>
    /// <param name="position">Position of the object.</param>
    /// <param name="rotation">Rotation of the object.</param>
    /// <returns>Returns spawned gameobject.</returns>
    public GameObject InstantiateFromPool(string name, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(name))
        {
            MonoBehaviour.print($"Pool with name " + name + " doesn't exist.");
            return null;
        }

        GameObject obj = poolDictionary[name].Dequeue();
        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(position, rotation);
        poolDictionary[name].Enqueue(obj);

        return obj;
    }

    public GameObject InstantiateFromPool(int index, Vector3 position, Quaternion rotation)
    {

        GameObject obj = poolDictionary.ElementAt(index).Value.Dequeue();
        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(position, rotation);
        poolDictionary.ElementAt(index).Value.Enqueue(obj);

        return obj;
    }
}