using UnityEngine;

/// <summary>
/// Base class for all spawnable objects.
/// </summary>
public abstract class SpawnableObject : MonoBehaviour
{
    [SerializeField] protected int layerToCollideWith;
    [SerializeField] protected SpawnableObjectStatsSO stats;
    [SerializeField] protected GameplayStatisticsSO statistics;
    [SerializeField] private SpawnType spawnType;

    // Object disable
    private readonly float TIMETODISABLE = 10f;
    private float timeSinceSpawned;

    protected MarionetteParent marionetteParent;
    protected MarionetteControl marionetteLimb;
    protected bool canCollide;

    protected virtual void Awake()
    {
        marionetteParent = FindObjectOfType<MarionetteParent>();
    }

    protected virtual void OnEnable()
    {
        canCollide = true;
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == layerToCollideWith)
        {
            if (canCollide)
            {
                OnMarionetteCollision(collider);
                canCollide = false;
            }
        }
    }

    /// <summary>
    /// Executes collision logic.
    /// </summary>
    /// <param name="collider">Marionette collision.</param>
    protected abstract void OnMarionetteCollision(Collider collider);

    protected enum SpawnType { MatchPosition, PowerUp, }
}
