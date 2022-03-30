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

    // Finger related
    [SerializeField] private FingerPosition finger;
    [SerializeField] private FingerPosition leftPalm;
    [SerializeField] private FingerPosition rightPalm;

    [SerializeField] private Canvas rectCanvas;
    [SerializeField] private RectTransform rectTrans;
    [SerializeField] private Vector2 rectDistance;

    private void Awake()
    {
        cam = Camera.main;
        lastFramePosition = transform.position;
    }

    /// <summary>
    /// Gets limbs input and updates their position.
    /// </summary>
    private void Update()
    {
        if (limb == Limb.Hips) return;

        Vector3 rectanglePosition;

        // Updates rectangles positions to control limbs
        switch(limb)
        {
            case Limb.LeftLeg:
                rectanglePosition =
                    leftPalm.gameObject.transform.position +
                    Vector3.right * rectDistance.x +
                    Vector3.up * rectDistance.y;

                break;
            case Limb.LeftArm:
                rectanglePosition =
                    leftPalm.gameObject.transform.position +
                    Vector3.right * rectDistance.x +
                    Vector3.up * rectDistance.y;
                break;
            case Limb.RightLeg:
                rectanglePosition =
                    rightPalm.gameObject.transform.position +
                    Vector3.right * rectDistance.x +
                    Vector3.up * rectDistance.y;
                break;
            case Limb.RightArm:
                rectanglePosition =
                    rightPalm.gameObject.transform.position +
                    Vector3.right * rectDistance.x +
                    Vector3.up * rectDistance.y;
                break;
            default:
                rectanglePosition = Vector3.zero;
                break;
        }

        rectTrans.position = cam.WorldToScreenPoint(rectanglePosition);
    }

    /// <summary>
    /// Constrains limbs positions.
    /// </summary>
    private void FixedUpdate()
    {
        if (finger == null) return;

        float xForce;
        float yForce;

        // Updates position to screen point
        Vector3 screenPos;

        // For hip, it gets the middle point between both hands
        if (limb == Limb.Hips)
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
        xForce = Mathf.InverseLerp(rectTrans.position.x, 
            rectTrans.position.x + rectTrans.sizeDelta.x * rectCanvas.scaleFactor, 
            screenPos.x) - 0.5f;
        yForce = Mathf.InverseLerp(rectTrans.position.y, 
            rectTrans.position.y + rectTrans.sizeDelta.y * rectCanvas.scaleFactor, 
            screenPos.y) - 0.5f;

        // Moves limbs
        transform.Translate(Vector3.right * Time.deltaTime * xForce * 5);
        transform.Translate(Vector3.up * Time.deltaTime * yForce * 5);

        // Positions Z correction
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        // Positions limit correction
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

        Gizmos.color = new Color(0, 0, 1, 0.25f);
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}

/// <summary>
/// Enum for limbs.
/// </summary>
public enum Limb { Hips, LeftArm, RightArm, LeftLeg, RightLeg }
