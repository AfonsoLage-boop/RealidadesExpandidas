using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon_Split : MonoBehaviour
{
    [SerializeField]
    private GameObject[] shards;

    private void OnCollisionEnter(Collision collision)
    {
        foreach ( GameObject shard in shards)
        {
            shard.GetComponent<MeshCollider>().enabled = true;
        }
    }
}
