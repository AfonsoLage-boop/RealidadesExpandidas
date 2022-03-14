using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ExtensionMethods;

public class DistanceConstrain : MonoBehaviour
{
    private Vector3 lastFramePosition;

    [SerializeField] private bool isMainAnchorPoint;
    [SerializeField] private Transform mainAnchorPoint;
    [SerializeField] private Transform anchorOfRadiusOfAction;
    [SerializeField] private float radiusOfAction;

    private FixedJoint joint;

    private void Awake()
    {
        joint = GetComponent<FixedJoint>();
        lastFramePosition = transform.position;
    }
    private void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, constrains[1].Constrainer.position));
    }

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

    [Serializable]
    public struct Constrain
    {
        [SerializeField] private Transform constrainer;
        [SerializeField] private Vector2 distanceLimit;

        public Transform Constrainer => constrainer;
        public Vector2 DistanceLimit => distanceLimit;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.15f);
        Gizmos.DrawSphere(anchorOfRadiusOfAction.transform.position, radiusOfAction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 0)
            joint.connectedBody = null;
    }
}
