using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPointsGenerator : MonoBehaviour
{
		public static Vector3 RPointGe(Vector3 startPoint, float radius)
		{
				Vector3 direction = Random.insideUnitSphere * radius;
				direction += startPoint;
				NavMeshHit hit;
				Vector3 finalPos = Vector3.zero;

				if (NavMesh.SamplePosition(direction, out hit, radius, 1)) {
						finalPos = hit.position;
				}
				return finalPos;

		}
}
