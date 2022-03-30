using UnityEngine;

/// <summary>
/// Base class for all spawnable objects.
/// </summary>
public abstract class SpawnableObject : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] private float speed = 4;

    protected BoxCollider boxCollider;
    protected MarionetteParent marionetteParent;
    protected MarionetteControl marionetteLimb;

    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        marionetteParent = FindObjectOfType<MarionetteParent>();
    }

    protected virtual void OnEnable() =>
        boxCollider.isTrigger = false;

    protected virtual void FixedUpdate() =>
        transform.position += transform.forward * speed * Time.fixedDeltaTime;

    private void OnCollisionEnter(Collision collision) =>
        OnMarionetteCollision(collision);

    /// <summary>
    /// Executes collision logic.
    /// </summary>
    /// <param name="collision">Marionette collision.</param>
    protected abstract void OnMarionetteCollision(Collision collision);
}
