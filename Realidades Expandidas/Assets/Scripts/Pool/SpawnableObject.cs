using UnityEngine;

/// <summary>
/// Base class for all spawnable objects.
/// </summary>
public abstract class SpawnableObject : MonoBehaviour
{
    [SerializeField] protected SpawnableObjectStatsSO stats;
    [SerializeField] private SpawnType spawnType;

    // Object disable
    private readonly float TIMETODISABLE = 10f;
    private float timeSinceSpawned;

    protected BoxCollider boxCollider;
    protected MarionetteParent marionetteParent;
    protected MarionetteControl marionetteLimb;
    protected bool canCollide;

    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        marionetteParent = FindObjectOfType<MarionetteParent>();
    }

    protected virtual void OnEnable()
    {
        canCollide = true;
        boxCollider.isTrigger = false;
        timeSinceSpawned = 0;
    }

    protected virtual void Update()
    {
        timeSinceSpawned += Time.deltaTime;
        if (timeSinceSpawned > TIMETODISABLE) gameObject.SetActive(false);
    }

    protected virtual void FixedUpdate()
    {
        float speed = spawnType == 
            SpawnType.MatchPosition ? stats.WallSpeed : stats.PowerUpSpeed;
        
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canCollide)
        {
            OnMarionetteCollision(collision);
            canCollide = false;
        }
    }

    /// <summary>
    /// Executes collision logic.
    /// </summary>
    /// <param name="collision">Marionette collision.</param>
    protected abstract void OnMarionetteCollision(Collision collision);

    protected enum SpawnType { MatchPosition, PowerUp, }
}
