using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanger : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.25f);
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
