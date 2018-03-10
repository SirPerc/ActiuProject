using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureBehavior : MonoBehaviour {

	[SerializeField]
	Collider m_collider;

	public float GetMinX()
	{
		return m_collider.bounds.min.x + transform.position.x;
	}

	public float GetMinZ()
	{
		return m_collider.bounds.min.z + transform.position.z;
	}

	public float GetMaxX()
	{
		return m_collider.bounds.max.x + transform.position.x;
	}

	public float GetMaxZ()
	{
		return m_collider.bounds.max.z + transform.position.z;
	}
}
