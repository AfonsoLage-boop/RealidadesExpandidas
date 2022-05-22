using UnityEngine;

/// <summary>
/// Class responsible for sending an upwards message when a trigger matches.
/// </summary>
public class MatchPosition : MonoBehaviour
{
    private MatchMarionettePosition marionettePosition;
    private bool contactCount;

    private void Awake() =>
        marionettePosition = GetComponentInParent<MatchMarionettePosition>();

    private void OnEnable()
    {
        contactCount = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Marionette position triggers
        if (other.gameObject.layer == 13)
        {
            if (contactCount == false)
            {
                marionettePosition.MatchPosition();
                contactCount = true;
            }
        }
    }
}
