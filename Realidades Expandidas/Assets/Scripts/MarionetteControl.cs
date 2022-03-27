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

    // Finger positions
    private FingerPosition fingerControl;

    // Screen positions
    private readonly static Vector2 FIRSTQUADRANTWIDTH = new Vector2(Screen.width / 3, Screen.width / 2);
    private readonly static Vector2 FIRSTQUADRANTHEIGHT = new Vector2(Screen.height / 2, Screen.height / 1.33f);
    private readonly static Vector2 SECONDQUADRANTWIDTH = new Vector2(Screen.width / 2, Screen.width / 1.5f);
    private readonly static Vector2 SECONDQUADRANTHEIGHT = new Vector2(Screen.height / 2, Screen.height / 1.33f);
    private readonly static Vector2 THIRDQUADRANTWIDTH = new Vector2(Screen.width / 2, Screen.width / 1.5f);
    private readonly static Vector2 THIRDQUADRANTHEIGHT = new Vector2(Screen.height / 4, Screen.height / 2f);
    private readonly static Vector2 FORTHQUADRANTWIDTH = new Vector2(Screen.width / 3, Screen.width / 2);
    private readonly static Vector2 FORTHQUADRANTHEIGHT = new Vector2(Screen.height / 4, Screen.height / 2f);

    private void Awake()
    {
        cam = Camera.main;
        joint = GetComponent<FixedJoint>();
        lastFramePosition = transform.position;

        // Fingers
        FingerPosition[] fingersControl = FindObjectsOfType<FingerPosition>();
        foreach (FingerPosition finger in fingersControl)
        {
            if (limb == Limb.LeftArm)
            {
                if (finger.FingerEnum == FingerEnum.LeftIndex)
                {
                    fingerControl = finger;
                    break;
                }
            }

            if (limb == Limb.LeftLeg)
            {
                if (finger.FingerEnum == FingerEnum.LeftThumb)
                {
                    fingerControl = finger;
                    break;
                }
            }

            if (limb == Limb.RightArm)
            {
                if (finger.FingerEnum == FingerEnum.RightIndex)
                {
                    fingerControl = finger;
                    break;
                }
            }

            if (limb == Limb.RightLeg)
            {
                if (finger.FingerEnum == FingerEnum.RightThumb)
                {
                    fingerControl = finger;
                    break;
                }
            }
        }
    }

    private void OnValidate()
    {
        
    }

    /// <summary>
    /// Gets limbs input and updates their position.
    /// </summary>
    private void Update()
    {
        if (fingerControl == null) return;

        float xForce = 0;
        float yForce = 0;

        // Updates position to screen point
        Vector3 screenPos = cam.WorldToScreenPoint(fingerControl.transform.position);

        // Moves each limb
        switch (limb)
        {
            case Limb.LeftArm:
                xForce = Mathf.InverseLerp(FIRSTQUADRANTWIDTH.x, FIRSTQUADRANTWIDTH.y, screenPos.x) - 0.5f;
                yForce = Mathf.InverseLerp(FIRSTQUADRANTHEIGHT.x, FIRSTQUADRANTHEIGHT.y, screenPos.y) - 0.5f;
                break;

            case Limb.RightArm:
                xForce = Mathf.InverseLerp(SECONDQUADRANTWIDTH.x, SECONDQUADRANTWIDTH.y, screenPos.x) - 0.5f;
                yForce = Mathf.InverseLerp(SECONDQUADRANTHEIGHT.x, SECONDQUADRANTHEIGHT.y, screenPos.y) - 0.5f;
                break;

            case Limb.RightLeg:
                xForce = Mathf.InverseLerp(THIRDQUADRANTWIDTH.x, THIRDQUADRANTWIDTH.y, screenPos.x) - 0.5f;
                yForce = Mathf.InverseLerp(THIRDQUADRANTHEIGHT.x, THIRDQUADRANTHEIGHT.y, screenPos.y) - 0.5f;
                break;

            case Limb.LeftLeg:
                xForce = Mathf.InverseLerp(FORTHQUADRANTWIDTH.x, FORTHQUADRANTWIDTH.y, screenPos.x) - 0.5f;
                yForce = Mathf.InverseLerp(FORTHQUADRANTHEIGHT.x, FORTHQUADRANTHEIGHT.y, screenPos.y) - 0.5f;
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
