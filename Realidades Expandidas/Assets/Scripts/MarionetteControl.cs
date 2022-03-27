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
    [SerializeField] private FingerPosition rightPalm;

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

        float xForce = 0;
        float yForce = 0;

        // Updates position to screen point
        Vector3 screenPos = cam.WorldToScreenPoint(finger.gameObject.transform.position);

        // Moves each limb
        switch (limb)
        {
            case Limb.LeftArm:
                xForce = Mathf.InverseLerp(quadrants.FirstQuadrantWidth.x, quadrants.FirstQuadrantWidth.y, screenPos.x) - 0.5f;
                yForce = Mathf.InverseLerp(quadrants.FirstQuadrantHeight.x, quadrants.FirstQuadrantHeight.y, screenPos.y) - 0.5f;
                break;

            case Limb.RightArm:
                xForce = Mathf.InverseLerp(quadrants.SecondQuadrantWidth.x, quadrants.SecondQuadrantWidth.y, screenPos.x) - 0.5f;
                yForce = Mathf.InverseLerp(quadrants.SecondQuadrantHeight.x, quadrants.SecondQuadrantHeight.y, screenPos.y) - 0.5f;
                break;

            case Limb.RightLeg:
                xForce = Mathf.InverseLerp(quadrants.ThirdQuadrantWidth.x, quadrants.ThirdQuadrantWidth.y, screenPos.x) - 0.5f;
                yForce = Mathf.InverseLerp(quadrants.ThirdQuadrantHeight.x, quadrants.ThirdQuadrantHeight.y, screenPos.y) - 0.5f;
                break;

            case Limb.LeftLeg:
                xForce = Mathf.InverseLerp(quadrants.ForthQuadrantWidth.x, quadrants.ForthQuadrantWidth.y, screenPos.x) - 0.5f;
                yForce = Mathf.InverseLerp(quadrants.ForthQuadrantHeight.x, quadrants.ForthQuadrantHeight.y, screenPos.y) - 0.5f;
                break;

            case Limb.Hips:

                screenPos = cam.WorldToScreenPoint(
                    (finger.gameObject.transform.position + rightPalm.gameObject.transform.position) /2);

                xForce = Mathf.InverseLerp(quadrants.AllQuadrantsWidth.x, quadrants.AllQuadrantsWidth.y, screenPos.x) - 0.5f;
                yForce = Mathf.InverseLerp(quadrants.AllQuadrantsHeight.x, quadrants.AllQuadrantsHeight.y, screenPos.y) - 0.5f;
                break;
        }

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
}

/// <summary>
/// Enum for limbs.
/// </summary>
public enum Limb { Hips, LeftArm, RightArm, LeftLeg, RightLeg }
