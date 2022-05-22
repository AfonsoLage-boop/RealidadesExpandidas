using UnityEngine;

/// <summary>
/// Class used by every wall.
/// </summary>
public class MatchMarionettePosition : SpawnableObject
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
                scoreEvaluation.Perfect();

                if (spawner.InInitialMenu == false)
                {
                    statistics.AttemptsSucceeded++;
                    statistics.AddPerfectScore();
                }
            }
            else if (positionsMatch == 3)
            {
                scoreEvaluation.Good();

                if (spawner.InInitialMenu == false)
                {
                    statistics.AttemptsSucceeded++;
                    statistics.AddGoodScore();
                }
            }
            else
            {
                scoreEvaluation.Bad();

                if (spawner.InInitialMenu == false)
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
