using UnityEngine;

/// <summary>
/// Class used by every wall.
/// </summary>
public class Wall : MonoBehaviour
{
    private BoxCollider boxCollider;
    private MarionetteParent marionetteParent;
    private MarionetteControl marionetteLimb;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        marionetteParent = FindObjectOfType<MarionetteParent>();
    }

    private void OnEnable()
    {
        boxCollider.isTrigger = false;
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * 4 * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out marionetteLimb))
        {
            marionetteParent.WallCollide();
            gameObject.SetActive(false);
        }
    }
}
