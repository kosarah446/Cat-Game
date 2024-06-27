using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KittenController : MonoBehaviour
{

		// Variable to keep track of collected "PickUp" objects.
		public List<GameObject> kittens = new List<GameObject>();
		public GameObject player;

		private List<Vector3> playerPosHistory = new List<Vector3>();
		
		private float timestep = 0.1f;
		private float lastTime = 0.0f;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
				if (Time.time - lastTime > timestep) {
						lastTime = Time.time;
						playerPosHistory.Add(player.transform.position);
				}

				if(playerPosHistory.Count > 10) {
						playerPosHistory.RemoveAt(0);
				}

				int count = 0;
				foreach (GameObject kitten in kittens) {
						count++;
						if (count > playerPosHistory.Count) {
								count = playerPosHistory.Count;
						}
						kitten.transform.position = playerPosHistory[count];
				}
		}
}
