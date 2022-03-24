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
    private Vector2 leftIndexPosition;

    // Screen positions
    private static Vector2 FIRSTQUADRANTWIDTH = new Vector2(Screen.width / 4, Screen.width / 2);
    private static Vector2 FIRSTQUADRANTHEIGHT = new Vector2(Screen.width / 4, Screen.width / 2.35f);
    private static Vector2 FIRSTQUADRANTMIDPOINT = 
        new Vector2((Screen.width / 4 + Screen.width / 2) / 2, (Screen.width / 4 + Screen.width / 2.35f) / 2);

    private void Awake()
    {
        cam = Camera.main;
        joint = GetComponent<FixedJoint>();
        lastFramePosition = transform.position;

        // Fingers
        FingerPosition[] fingersControl = FindObjectsOfType<FingerPosition>();
        foreach (FingerPosition finger in fingersControl)
        {
            if (finger.FingerEnum == FingerEnum.LeftIndex)
            {
                fingerControl = finger;
                break;
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
        //leftIndexPosition = leftIndex.transform.position;

        if (limb == Limb.LeftArm)
        {
            //leftIndexPosition.z = 0;
            //transform.position = leftIndexPosition;



            Vector3 screenPos = cam.WorldToScreenPoint(fingerControl.transform.position);

            if (screenPos.x.IsInside(FIRSTQUADRANTWIDTH) &&
                screenPos.y.IsInside(FIRSTQUADRANTHEIGHT))
            {
                float xForce = Mathf.InverseLerp(FIRSTQUADRANTWIDTH.x, FIRSTQUADRANTWIDTH.y, screenPos.x) - 0.5f;
                float yForce = Mathf.InverseLerp(FIRSTQUADRANTHEIGHT.x, FIRSTQUADRANTHEIGHT.y, screenPos.y) - 0.5f;

                transform.Translate(Vector3.right * Time.deltaTime * xForce * 5);
                transform.Translate(Vector3.up * Time.deltaTime * yForce * 5);
            }
        }

        

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
