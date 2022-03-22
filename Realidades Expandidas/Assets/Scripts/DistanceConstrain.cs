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
    private readonly float translationForce = 2f;

    private void Awake()
    {
        joint = GetComponent<FixedJoint>();
        lastFramePosition = transform.position;
    }

    /// <summary>
    /// Gets limbs input and updates their position.
    /// </summary>
    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (limb == Limb.Hips)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(Vector3.right * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(Vector3.left * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(Vector3.up * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(Vector3.down * translationForce * Time.deltaTime);
                }
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (limb == Limb.LeftLeg)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(Vector3.right * translationForce* Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(Vector3.left * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(Vector3.up * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(Vector3.down * translationForce * Time.deltaTime);
                }
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (limb == Limb.RightLeg)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(Vector3.right * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(Vector3.left * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(Vector3.up * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(Vector3.down * translationForce * Time.deltaTime);
                }
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (limb == Limb.LeftArm)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(Vector3.right * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(Vector3.left * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(Vector3.up * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(Vector3.down * translationForce * Time.deltaTime);
                }
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if (limb == Limb.RightArm)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(Vector3.right * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(Vector3.left * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(Vector3.up * translationForce * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(Vector3.down * translationForce * Time.deltaTime);
                }
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
