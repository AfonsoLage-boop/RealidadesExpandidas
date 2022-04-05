using UnityEngine;

/// <summary>
/// Class used by every wall.
/// </summary>
public class MatchMaroionettePosition : SpawnableObject
{
    private int positionsMatch;

    protected override void OnMarionetteCollision(Collider collider)
    {
        if (collider.gameObject.layer == 14)
        {
            if (positionsMatch == 4)
            {
                Debug.Log("Good");
            }
            else
            {
                Debug.Log("Bad");
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
