using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPosition : MonoBehaviour
{
    private MatchMaroionettePosition marionettePosition;

    private void Awake()
    {
        marionettePosition = GetComponentInParent<MatchMaroionettePosition>();
    }
}
