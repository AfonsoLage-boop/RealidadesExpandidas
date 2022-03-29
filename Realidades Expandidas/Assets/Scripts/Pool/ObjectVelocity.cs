using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVelocity : MonoBehaviour
{
    private Rigidbody rb;
    private YieldInstruction wffu;
    private BoxCollider boxCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        boxCollider.isTrigger = false;
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * 4 * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out MarionetteControl marionette))
        {
            marionette.Collided();
            gameObject.SetActive(false);
        }
    }
}
