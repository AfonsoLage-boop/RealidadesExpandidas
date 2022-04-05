using UnityEngine;

/// <summary>
/// Class used by every wall.
/// </summary>
public class MatchMaroionettePosition : SpawnableObject
{
    private int positionsMatch;

    protected override void OnMarionetteCollision(Collision collision)
    {
        if (collision.gameObject.layer == 14)
        {
            if (positionsMatch == 4)
            {
                Debug.Log("4 Matches");
            }
            else
            {
                Debug.Log("Not 4 Matches");
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        positionsMatch = 0;
    }

    public void MatchPosition()
    {
        positionsMatch++;
    }
}
