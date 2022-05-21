using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMemberPositionToScreen : MonoBehaviour
{
    [SerializeField] private Transform member;

    private void FixedUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(member.position);
    }
}
