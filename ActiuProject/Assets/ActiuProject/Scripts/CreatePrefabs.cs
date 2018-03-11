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
	void Start()
	{
		Randomizer();
	}

	public void Randomizer()
	{
		Debug.Log("Randomizer");

		Vector3 v3Position;
		Quaternion qRotation;
		Transform auxTransform;
		FurnitureBehavior furnitureBehavior;

		float fMinX, fMaxX, fMinZ, fMaxZ;
		int j = 0;
		bool bRepeat = true;

		for (int k = 0; k < 50 && bRepeat; k++)
		{
			bRepeat = false;

			if (m_lCreatedObjects == null)
			{
				m_lCreatedObjects = new List<Transform>();
			}
			else
			{
				foreach (Transform tObject in m_lCreatedObjects)
				{
					tObject.gameObject.SetActive(false);
				}
				m_lCreatedObjects.Clear();
			}

			for (int i = 0; i < m_iNumberOfObjects && j < 50; i++)
			{
				auxTransform = Transform.Instantiate(m_vListOfPrefabs[i % m_vListOfPrefabs.Length], m_ParentTransform);
				furnitureBehavior = auxTransform.GetComponent<FurnitureBehavior>();

				j = 0;

				do
				{
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
					j++;

				} while (IsIntersect(auxTransform) && j < 50);

				auxTransform.name = m_vListOfPrefabs[i % m_vListOfPrefabs.Length].name + " " + i / m_vListOfPrefabs.Length;

				m_lCreatedObjects.Add(auxTransform);
			}

			if (j >= 50)
			{
				Debug.LogWarning("Nos hemos pasado de iteraciones");

				j = 0;
				bRepeat = true;
			}

			if (k >= 49)
			{
				Debug.LogWarning("Demasiadas iteraciones generales");
			}
		}
	}

	bool IsIntersect(Transform tCurrentObject)
	{

		bool bIsIntersect = false;

		foreach (Transform tCompareObject in m_lCreatedObjects)
		{
			bIsIntersect |= tCurrentObject.GetComponent<FurnitureBehavior>().IntersectsCollider(tCompareObject.GetComponent<Collider>());
			Debug.Log(tCurrentObject.name + ": is on " + bIsIntersect + " with " + tCompareObject.name);
		}

		return bIsIntersect;
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			Randomizer();
		}
	}
}
