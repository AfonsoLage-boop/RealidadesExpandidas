/// <summary>
/// Gains a life when picked.
/// </summary>
public class GainLife : PowerUp
{
    public override void ExecuteBoost()
    {
        statistics.Lives++;
        gameObject.SetActive(false);
    }
}
