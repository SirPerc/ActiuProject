using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePrefabs : MonoBehaviour
{
	[SerializeField]
	Transform[] m_vListOfPrefabs;

	[SerializeField]
	Transform m_ParentTransform;

	List<Transform> m_lCreatedObjects;

	[SerializeField]
	float m_fMinX, m_fMaxX, m_fMinZ, m_fMaxZ;
	int m_iNumberOfObjects = 4;


	// Use this for initialization
	void Start () {
		Vector3 v3Position;
		Quaternion qRotation;
		Transform auxTransform;
		FurnitureBehavior furnitureBehavior;

		float fMinX, fMaxX, fMinZ, fMaxZ;
		
		m_lCreatedObjects = new List<Transform>();
		for (int i = 0; i < m_iNumberOfObjects; i++)
		{
			auxTransform = Transform.Instantiate(m_vListOfPrefabs[i % m_vListOfPrefabs.Length], m_ParentTransform);
			furnitureBehavior = auxTransform.GetComponent<FurnitureBehavior>();
			m_lCreatedObjects.Add(auxTransform);

			//Set random rotation
			qRotation = Quaternion.Euler(0, (float)Random.Range(0, 360), 0);

			auxTransform.SetPositionAndRotation(auxTransform.position, qRotation);

			//Set random position but inside the room
			fMinX = m_fMinX - furnitureBehavior.GetMinX();
			Debug.Log(auxTransform.name + ": fMinxX:" + fMinX);
			fMaxX = m_fMaxX - furnitureBehavior.GetMaxX();
			Debug.Log(auxTransform.name + ": fManxZ:" + fMinX);
			fMinZ = m_fMinZ - furnitureBehavior.GetMinZ();
			Debug.Log(auxTransform.name + ": fMinxX:" + fMinX);
			fMaxZ = m_fMaxZ - furnitureBehavior.GetMaxZ();
			Debug.Log(auxTransform.name + ": fManxZ:" + fMinX);

			v3Position = new Vector3(Random.Range(fMinX, fMaxX), 0, Random.Range(fMinZ, fMaxZ));
				
			auxTransform.SetPositionAndRotation(v3Position, auxTransform.rotation);

			auxTransform.name = m_vListOfPrefabs[i % m_vListOfPrefabs.Length].name + " " + i / m_vListOfPrefabs.Length;
		}
	}
}
