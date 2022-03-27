using UnityEngine;
using System;

/// <summary>
/// Class for each finger position.
/// </summary>
public class FingerPosition : MonoBehaviour
{
    [SerializeField] private FingerEnum fingerTipEnum;
    public FingerEnum FingerEnum => fingerTipEnum;
}

[Flags]
public enum FingerEnum
{
    LeftIndex = 1,
    LeftThumb = 2,
    RightIndex = 4,
    RightThumb = 8,
    LeftPalm = 16,
    RightPalm = 32,
}


