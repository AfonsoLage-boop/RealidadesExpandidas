using UnityEngine;

/// <summary>
/// Class used by every wall.
/// </summary>
public class MatchMarionettePosition : SpawnableObject
{
    private UITextScoreEvaluation scoreEvaluation;
    private int positionsMatch;
    private bool collidedWithCheckWall;
    //[SerializeField] private Animator anim;

    protected override void Awake()
    {
        base.Awake();
        scoreEvaluation = FindObjectOfType<UITextScoreEvaluation>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        positionsMatch = 0;
        collidedWithCheckWall = false;

        //anim.ResetTrigger("Good");
        //anim.ResetTrigger("Bad");
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
                //anim.SetTrigger("Good");
            }
            else if (positionsMatch == 3)
            {
                scoreEvaluation.Good();

                if (spawner.InInitialMenu == false)
                {
                    statistics.AttemptsSucceeded++;
                    statistics.AddGoodScore();
                }
                //anim.SetTrigger("Good");
            }
            else
            {
                scoreEvaluation.Bad();

                if (spawner.InInitialMenu == false)
                    statistics.AttemptsFailed++;
                //anim.SetTrigger("Bad");
            }
        }

        collidedWithCheckWall = true;
    }

    public void MatchPosition()
    {
        positionsMatch++;
    }
}
