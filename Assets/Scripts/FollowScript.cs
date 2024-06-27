using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowScript : MonoBehaviour
{
		// Variable to keep track of collected "PickUp" objects.
		//public List<GameObject> kittens = new List<GameObject>();
		public GameObject parent;
		NavMeshAgent agent;

		//private List<Vector3> parentPosHistory = new List<Vector3>();


		/*
		private Vector3 parentPosHistory = new Vector3(0, 0, 0);

		private float timestep = 0.5f;
		private float lastTime = 0.0f;
		*/

		// Start is called before the first frame update
		void Start()
		{
				agent = GetComponent<NavMeshAgent>();
		}

		// Update is called once per frame
		void Update()
		{
				if (parent != null) {
						/*
						if (Time.time - lastTime > timestep) {
								lastTime = Time.time;

								parentPosHistory = parent.transform.position;
						}
						this.transform.position = parentPosHistory;
						*/
						agent.SetDestination(parent.transform.position);
				}
		}
}