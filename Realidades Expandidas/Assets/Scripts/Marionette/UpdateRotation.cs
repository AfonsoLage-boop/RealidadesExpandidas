using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

public class UpdateRotation : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.LookAt(Vector3.up * 100000f);
    }
}
