using UnityEngine;
using ExtensionMethods;

/// <summary>
/// Controls marionette while constraining limbs distance.
/// </summary>
public class MarionetteControl : MonoBehaviour
{
    private Camera cam;

    // Limb constrain
    private Vector3 lastFramePosition;
    [SerializeField] private Limb limb;
    [SerializeField] private bool isMainAnchorPoint;
    [SerializeField] private Transform mainAnchorPoint;
    [SerializeField] private Transform anchorOfRadiusOfAction;
    [SerializeField] private float radiusOfAction;

    // Limb disable
    private FixedJoint joint;

    // Finger related
    private QuadrantsLimits quadrants;
    [SerializeField] private FingerPosition finger;
    [SerializeField] private FingerPosition leftPalm;
    [SerializeField] private FingerPosition rightPalm;

    [SerializeField] private RectTransform rectTrans;
    [SerializeField] private Vector2 rectDistance;

    private void Awake()
    {
        cam = Camera.main;
        joint = GetComponent<FixedJoint>();
        quadrants = GetComponentInParent<QuadrantsLimits>();
        lastFramePosition = transform.position;
    }

    private void OnValidate()
    {
        
    }


    /// <summary>
    /// Gets limbs input and updates their position.
    /// </summary>
    private void Update()
    {
        if (finger == null) return;

        float xForce;
        float yForce;

        // Updates rectangles positions to control limbs
        switch(limb)
        {
            case Limb.LeftLeg:
                rectTrans.position = cam.WorldToScreenPoint(
                    leftPalm.gameObject.transform.position) + 
                    Vector3.right * rectDistance.x +
                    Vector3.up * rectDistance.y;
                break;
            case Limb.LeftArm:
                rectTrans.position = cam.WorldToScreenPoint(
                    leftPalm.gameObject.transform.position) +
                    Vector3.right * rectDistance.x +
                    Vector3.up * rectDistance.y;
                break;
            case Limb.RightLeg:
                rectTrans.position = cam.WorldToScreenPoint(
                    rightPalm.gameObject.transform.position) +
                    Vector3.right * rectDistance.x +
                    Vector3.up * rectDistance.y;
                break;
            case Limb.RightArm:
                rectTrans.position = cam.WorldToScreenPoint(
                    rightPalm.gameObject.transform.position) +
                    Vector3.right * rectDistance.x +
                    Vector3.up * rectDistance.y;
                break;
        }
        
        // Updates position to screen point
        Vector3 screenPos;

        // For hip, it gets the middle point between both hands
        if(limb == Limb.Hips)
        {
            screenPos = cam.WorldToScreenPoint(
                (leftPalm.gameObject.transform.position + 
                rightPalm.gameObject.transform.position) / 2);
        }
        // For the rest of the limbs, gets finger position
        else
        {
            screenPos = cam.WorldToScreenPoint(finger.gameObject.transform.position);
        }

        // Gets movement force depending on the distance from the center
        xForce = Mathf.InverseLerp(rectTrans.position.x, rectTrans.position.x +
            rectTrans.sizeDelta.x, screenPos.x) - 0.5f;
        yForce = Mathf.InverseLerp(rectTrans.position.y, rectTrans.position.y +
            rectTrans.sizeDelta.y, screenPos.y) - 0.5f;

        // Moves limbs
        transform.Translate(Vector3.right * Time.deltaTime * xForce * 5);
        transform.Translate(Vector3.up * Time.deltaTime * yForce * 5);
    }

    /// <summary>
    /// Constrains limbs positions.
    /// </summary>
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (transform.position != lastFramePosition)
        {
            if (Vector3.Distance(transform.position, anchorOfRadiusOfAction.position) > 
                radiusOfAction)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 0)
        {
            joint.connectedBody = null;
        }
    }
}

/// <summary>
/// Enum for limbs.
/// </summary>
public enum Limb { Hips, LeftArm, RightArm, LeftLeg, RightLeg }
