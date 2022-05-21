using UnityEngine;

/// <summary>
/// Class used by every wall.
/// </summary>
public class MatchMaroionettePosition : SpawnableObject
{
    private UITextScoreEvaluation scoreEvaluation;
    private int positionsMatch;
    private bool collidedWithCheckWall;

    protected override void Awake()
    {
        base.Awake();
        scoreEvaluation = FindObjectOfType<UITextScoreEvaluation>();
    }

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
                Debug.Log("Perfect");
                scoreEvaluation.Perfect();
                statistics.AttemptsSucceeded++;
            }
            else if (positionsMatch == 3)
            {
                Debug.Log("Good");
                scoreEvaluation.Good();
                statistics.AttemptsSucceeded++;
            }
            else
            {
                Debug.Log("Bad");
                scoreEvaluation.Bad();
                statistics.AttemptsFailed++;
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
