using UnityEngine;

/// <summary>
/// Base class used by power ups.
/// </summary>
public abstract class PowerUp : SpawnableObject, IPowerUp
{
    public abstract void ExecuteBoost();

    protected override void OnMarionetteCollision(Collider collider) =>
        ExecuteBoost();
}
