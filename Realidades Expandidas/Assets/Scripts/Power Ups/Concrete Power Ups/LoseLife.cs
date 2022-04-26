/// <summary>
/// Loses a life when picked.
/// </summary>
public class LoseLife : PowerUp
{
    public override void ExecuteBoost()
    {
        statistics.Lives--;
        gameObject.SetActive(false);
    }
}
