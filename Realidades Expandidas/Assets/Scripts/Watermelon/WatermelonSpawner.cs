using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermelonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject watermelonPrefab;
    private IList<Transform> positions;

    private void Awake()
    {
        positions = new List<Transform>();
        foreach(Transform t in transform.GetChild(0).transform)
        {
            positions.Add(t);
        }
    }

    public void SpawnWatermelon()
    {
        int randNum = Random.Range(0, positions.Count);

        GameObject watermelon =
            Instantiate(watermelonPrefab,
            positions[randNum].transform.position,
            Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

        if (watermelon.TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = positions[randNum].transform.forward * 5;
        }
    }
}
