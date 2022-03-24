using UnityEngine;

/// <summary>
/// Class for each finger position.
/// </summary>
public class FingerPosition : MonoBehaviour
{
    [SerializeField] private FingerEnum fingerTipEnum;
    public FingerEnum FingerEnum => fingerTipEnum;
}

public enum FingerEnum
{
    LeftIndex,
    LeftThumb,
    RightIndex,
    RightThumb,
    LeftPalm,
    RightPalm,
}


