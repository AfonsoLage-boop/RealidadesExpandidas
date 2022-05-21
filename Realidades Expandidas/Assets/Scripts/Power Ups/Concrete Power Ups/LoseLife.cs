/// <summary>
/// Loses a life when picked.
/// </summary>
public class LoseLife : PowerUp
{
    public override void ExecuteBoost()
    {
        if (spawner.InInitialMenu == false)
            statistics.Lives--;
        gameObject.SetActive(false);
    }
}
