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

    private float horizontalDistance;
    private float verticalDistance;
    public void HorizontalDistance(float x)
    {
        PlayerPrefs.SetFloat(limb.ToString() + HV.Horizontal.ToString(), x);
        horizontalDistance = x;
    }
    public void VerticalDistance(float x)
    {
        PlayerPrefs.SetFloat(limb.ToString() + HV.Vertical.ToString(), x);
        verticalDistance = x;
    }

    [SerializeField] private Transform legsStretchTransform;
    [Range(0.1f, 2f)] [SerializeField] private float legsStretchLimit = 0.92f;

    [SerializeField] private Transform armsStretchTransform;
    [Range(0.1f, 2f)] [SerializeField] private float armsStretchLimit = 0.92f;

    private MarionetteParent marionetteParent;

    private void Awake()
    {
        cam = Camera.main;
        lastFramePosition = transform.position;
        marionetteParent = GetComponentInParent<MarionetteParent>();
    }

    /// <summary>
    /// Gets limbs input and updates their position.
    /// </summary>
    private void Update()
    {
        if (limb == Limb.Hips) return;

        if (marionetteParent.ControlWithKeyboard) return;

        Vector3 rectanglePosition;

        // Updates rectangles positions to control limbs
        switch (limb)
        {
            case Limb.LeftLeg:
                rectanglePosition =
                    leftPalm.gameObject.transform.position +
                    Vector3.right * horizontalDistance +
                    Vector3.up * verticalDistance;

                break;
            case Limb.LeftArm:
                rectanglePosition =
                    leftPalm.gameObject.transform.position +
                    Vector3.right * horizontalDistance +
                    Vector3.up * verticalDistance;
                break;
            case Limb.RightLeg:
                rectanglePosition =
                    rightPalm.gameObject.transform.position +
                    Vector3.right * horizontalDistance +
                    Vector3.up * verticalDistance;
                break;
            case Limb.RightArm:
                rectanglePosition =
                    rightPalm.gameObject.transform.position +
                    Vector3.right * horizontalDistance +
                    Vector3.up * verticalDistance;
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
        // If the marionette is being controlled with the keyboard, ignores everything else below
        #region Keyboard Control
        if (marionetteParent.ControlWithKeyboard)
        {
            Vector3 translateLeft = Vector3.zero;
            Vector3 translateUp = Vector3.zero;
            Vector3 translateDown = Vector3.zero;
            Vector3 translateRight = Vector3.zero;

            // Moves all members at the same time
            if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                    translateLeft = Vector3.left;

                if (Input.GetKey(KeyCode.RightArrow))
                    translateRight = Vector3.right;

                if (Input.GetKey(KeyCode.UpArrow))
                    translateUp = Vector3.up;

                if (Input.GetKey(KeyCode.DownArrow))
                    translateDown = Vector3.down;
            }
            else // Else it moves on at a time
            {
                if (Input.GetKey(KeyCode.A))
                {
                    if (limb == Limb.LeftLeg)
                    {
                        if (Input.GetKey(KeyCode.LeftArrow))
                            translateLeft = Vector3.left;

                        if (Input.GetKey(KeyCode.RightArrow))
                            translateRight = Vector3.right;

                        if (Input.GetKey(KeyCode.UpArrow))
                            translateUp = Vector3.up;

                        if (Input.GetKey(KeyCode.DownArrow))
                            translateDown = Vector3.down;
                    }
                }

                if (Input.GetKey(KeyCode.D))
                {
                    if (limb == Limb.RightLeg)
                    {
                        if (Input.GetKey(KeyCode.LeftArrow))
                            translateLeft = Vector3.left;

                        if (Input.GetKey(KeyCode.RightArrow))
                            translateRight = Vector3.right;

                        if (Input.GetKey(KeyCode.UpArrow))
                            translateUp = Vector3.up;

                        if (Input.GetKey(KeyCode.DownArrow))
                            translateDown = Vector3.down;
                    }
                }

                if (Input.GetKey(KeyCode.Q))
                {
                    if (limb == Limb.LeftArm)
                    {
                        if (Input.GetKey(KeyCode.LeftArrow))
                            translateLeft = Vector3.left;

                        if (Input.GetKey(KeyCode.RightArrow))
                            translateRight = Vector3.right;

                        if (Input.GetKey(KeyCode.UpArrow))
                            translateUp = Vector3.up;

                        if (Input.GetKey(KeyCode.DownArrow))
                            translateDown = Vector3.down;
                    }
                }

                if (Input.GetKey(KeyCode.E))
                {
                    if (limb == Limb.RightArm)
                    {
                        if (Input.GetKey(KeyCode.LeftArrow))
                            translateLeft = Vector3.left;

                        if (Input.GetKey(KeyCode.RightArrow))
                            translateRight = Vector3.right;

                        if (Input.GetKey(KeyCode.UpArrow))
                            translateUp = Vector3.up;

                        if (Input.GetKey(KeyCode.DownArrow))
                            translateDown = Vector3.down;
                    }
                }
            }

            // Final fector
            Vector3 moveTo = (translateDown + translateUp +
                translateLeft + translateRight).normalized *
                marionetteParent.TranslationForce * Time.deltaTime;

            // Constrain
            if (Vector3.Distance(transform.position + moveTo,
                        anchorOfRadiusOfAction.position) > radiusOfAction)
            {
                transform.position =
                    anchorOfRadiusOfAction.position +
                    anchorOfRadiusOfAction.position.Direction(
                        transform.position + moveTo) *
                    radiusOfAction;
            }

            transform.Translate(moveTo);

            return;
        }
        #endregion

        // If the ragdoll is being controlled with lipmotion
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

        // Limits limbs stretching
        if (limb == Limb.LeftLeg || limb == Limb.RightLeg)
        {
            if (Vector3.Distance(transform.position, legsStretchTransform.position) > legsStretchLimit)
            {
                transform.position =
                    legsStretchTransform.position +
                    legsStretchTransform.position.Direction(transform.position) * legsStretchLimit;
            }
        }

        // Limits limbs stretching
        if (limb == Limb.LeftArm || limb == Limb.RightArm)
        {
            if (Vector3.Distance(transform.position, armsStretchTransform.position) > armsStretchLimit)
            {
                transform.position =
                    armsStretchTransform.position +
                    armsStretchTransform.position.Direction(transform.position) * armsStretchLimit;
            }
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
