/// <summary>
/// Gains a life when picked.
/// </summary>
public class GainLife : PowerUp
{
    public override void ExecuteBoost()
    {
        if (spawner.InInitialMenu == false)
            statistics.Lives++;
        gameObject.SetActive(false);
    }
}
