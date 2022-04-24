using UnityEngine;

/// <summary>
/// Class used by every wall.
/// </summary>
public class MatchMaroionettePosition : SpawnableObject
{
    private int positionsMatch;
    private bool collidedWithCheckWall;

    protected override void OnMarionetteCollision(Collider collider)
    {
        // In case it collides more than once
        if (collidedWithCheckWall) return;

        // Layer of the invisible wall behind the marionette
        if (collider.gameObject.layer == 14)
        {
            // All positions hit
            if (positionsMatch == 4)
            {
                Debug.Log("Good");
            }
            else
            {
                Debug.Log("Bad");
            }
        }

        collidedWithCheckWall = true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        positionsMatch = 0;
        collidedWithCheckWall = false;
    }

    public void MatchPosition()
    {
        positionsMatch++;
    }
}
