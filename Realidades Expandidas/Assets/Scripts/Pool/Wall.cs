using UnityEngine;

/// <summary>
/// Class used by every wall.
/// </summary>
public class Wall : SpawnableObject
{
    /// <summary>
    /// Disables marionette.
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnMarionetteCollision(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out marionetteLimb))
        {
            marionetteParent.WallCollide();
            gameObject.SetActive(false);
        }
    }
}
