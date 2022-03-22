using UnityEngine;

public class FingerTip : MonoBehaviour
{
    [SerializeField] private FingerEnum fingerTipEnum;
    public FingerEnum FingerEnum => fingerTipEnum;

    public Vector3 FingerTipPosition(FingerEnum fingerEnum)
    {
        switch (fingerEnum)
        {
            case FingerEnum.LeftIndex:
                return transform.position;
            case FingerEnum.LeftThumb:
                return transform.position;
            case FingerEnum.RightIndex:
                return transform.position;
            case FingerEnum.RightThumb:
                return transform.position;
            default:
                return Vector3.zero;
        }
    }
}

public enum FingerEnum
{
    LeftIndex,
    LeftThumb,
    RightIndex,
    RightThumb,
}


