using UnityEngine;

/// <summary>
/// Base class for all spawnable objects.
/// </summary>
public abstract class SpawnableObject : MonoBehaviour
{
    [Header("Serialized Components")]
    [SerializeField] protected int layerToCollideWith;
    [SerializeField] protected SpawnableObjectStatsSO stats;
    [SerializeField] protected GameplayStatisticsSO statistics;
    [SerializeField] private SpawnType spawnType;

    // Object disable
    private readonly float TIMETODISABLE = 10f;
    private float timeSinceSpawned;

    // Collisions
    protected MarionetteParent marionetteParent;
    protected MarionetteControl marionetteLimb;
    protected bool canCollide;

    // Statistics
    private TextStatistics textStatistics;

    private float speed;

    private ObjectSpawner spawner;

    protected virtual void Awake()
    {
        marionetteParent = FindObjectOfType<MarionetteParent>();
        textStatistics = FindObjectOfType<TextStatistics>();
        spawner = FindObjectOfType<ObjectSpawner>();
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
        if (spawner.InMenu || spawner.IsPaused)
        {
            speed = 0;
        }
        else
        {
            speed = spawnType ==
                SpawnType.MatchPosition ? stats.WallSpeed : stats.PowerUpSpeed;
        }
        
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
                textStatistics.UpdateText();
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
