using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveComponentName : MonoBehaviour
{
	public void RemoveComponents()
	{
		Component[] joints = GetComponentsInChildren(typeof(CharacterJoint), true);

		foreach (var c in joints)
		{
			DestroyImmediate(c);
		}

		Component[] capsuleCols = GetComponentsInChildren(typeof(CapsuleCollider), true);

		foreach (var c in capsuleCols)
		{
			DestroyImmediate(c);
		}

		Component[] sphereCols = GetComponentsInChildren(typeof(SphereCollider), true);

		foreach (var c in sphereCols)
		{
			DestroyImmediate(c);
		}

		Component[] boxCols = GetComponentsInChildren(typeof(BoxCollider), true);

		foreach (var c in boxCols)
		{
			DestroyImmediate(c);
		}

		Component[] rigidbodies = GetComponentsInChildren(typeof(Rigidbody), true);

		foreach (var c in rigidbodies)
		{
			DestroyImmediate(c);
		}

		Component[] vfxLines = GetComponentsInChildren(typeof(UpdateRotation), true);

		foreach (var c in vfxLines)
		{
			DestroyImmediate(c.gameObject);
		}
	}
}