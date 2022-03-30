/// <summary>
/// Slows wall speed when picked up.
/// </summary>
public class SlowTime : PowerUp
{
    // Components
    private PowerUpCoroutines coroutineRunner;

    protected override void Awake()
    {
        base.Awake();
        coroutineRunner = FindObjectOfType<PowerUpCoroutines>();
    }

    public override void ExecuteBoost()
    {
        StartCoroutine(coroutineRunner.SlowTimeCoroutine());
        gameObject.SetActive(false);
    }
}
