using UnityEngine;
using System;
using ExtensionMethods;

/// <summary>
/// Constrains limbs distance.
/// </summary>
public class DistanceConstrain : MonoBehaviour
{
    private Vector3 lastFramePosition;

    [SerializeField] private Limb limb;
    [SerializeField] private bool isMainAnchorPoint;
    [SerializeField] private Transform mainAnchorPoint;
    [SerializeField] private Transform anchorOfRadiusOfAction;
    [SerializeField] private float radiusOfAction;

    private FixedJoint joint;

    private void Awake()
    {
        joint = GetComponent<FixedJoint>();
        lastFramePosition = transform.position;

        // Fingers
        fingers = FindObjectsOfType<FingerTip>();
        foreach (FingerTip finger in fingers)
        {
            if (finger.FingerEnum == FingerEnum.LeftIndex)
            {
                leftIndex = finger;
                initialLeftFingerPosition = finger.transform.position;
                leftIndexPosition = Vector3.zero;
            }
        }
    }

    FingerTip[] fingers;
    FingerTip leftIndex;
    Vector3 initialLeftFingerPosition;
    Vector3 leftIndexPosition;

    private void Start()
    {
        
 
        
    }

    /// <summary>
    /// Gets limbs input and updates their position.
    /// </summary>
    private void Update()
    {
        leftIndexPosition = leftIndex.transform.position;



        if (limb == Limb.LeftArm)
        {
            leftIndexPosition.z = 0;
            transform.position = leftIndexPosition;
            
            
        }

        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(leftIndexPosition);

    }

    /// <summary>
    /// Constrains limbs positions.
    /// </summary>
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (transform.position != lastFramePosition)
        {
            if (Vector3.Distance(transform.position, anchorOfRadiusOfAction.position) > radiusOfAction)
            {
                transform.position =
                    anchorOfRadiusOfAction.position +
                    anchorOfRadiusOfAction.position.Direction(transform.position) *
                    radiusOfAction;
            }
            

            lastFramePosition = transform.position;
        }
    }

    /// <summary>
    /// Draws limbs radius of action gizmos.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.15f);
        Gizmos.DrawSphere(anchorOfRadiusOfAction.transform.position, radiusOfAction);
    }

    /// <summary>
    /// On limb collision.
    /// </summary>
    /// <param name="other">Other collider.q</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 0)
            joint.connectedBody = null;
    }

    /// <summary>
    /// Enum for limbs.
    /// </summary>
    public enum Limb { Hips, LeftArm, RightArm, LeftLeg, RightLeg }
}
