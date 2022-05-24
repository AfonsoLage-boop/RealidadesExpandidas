using UnityEngine;

public class Watermelon_Split : MonoBehaviour
{
    [SerializeField] private GameObject watermelonParticles;
    [SerializeField] private Rigidbody[] rbs;
    [SerializeField] private MeshCollider[] shards;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 15||
            collision.gameObject.layer == 16)
        {
            watermelonParticles.transform.position =
                transform.position + Vector3.up * 0.5f;
            watermelonParticles.transform.LookAt(Vector3.up * 500f);
            watermelonParticles.transform.parent = null;
            watermelonParticles.SetActive(true);

            foreach (Rigidbody rb in rbs)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            foreach (MeshCollider shard in shards)
            {
                shard.enabled = true;
            }
        }
    }
}
