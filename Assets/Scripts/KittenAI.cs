using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static DogAI;

public class KittenAI : MonoBehaviour
{
		public NavMeshAgent agent;
		public GameObject parent;
		public GameObject child;
		//public GameObject portal;
		public GameObject UIController;
		public GameObject GameController;
		public float speed;
		public float angularSpeed;
		public float acceleration;

		public bool isWandering = true;
		public bool kittensScatterWhenHit = false;
		private float wanderTimer;

		private Vector3 startingLocation = new Vector3(0, 0, 0);

		//wandering code
		private float roamingRadius = 1f;
		Vector3 nextPos;


		public enum KittenState
		{
				Idle,
				Follow,
				Deposit,
				ResetPosition
		}
		public KittenState state;

		// Start is called before the first frame update
		void Start()
		{
				agent = GetComponent<NavMeshAgent>();
				speed = agent.speed;
				angularSpeed = agent.angularSpeed;
				acceleration = agent.acceleration;
				agent.speed = 2.5f;
				agent.angularSpeed = 720;
				agent.acceleration = 0.5f;
				state = KittenState.Idle;
				UIController = GameObject.Find("UIController");
				GameController = GameObject.Find("GameController");
				startingLocation = gameObject.transform.position;
				nextPos = gameObject.transform.position;
		}



		// Update is called once per frame
		void Update()
		{
				switch (state) {
						case KittenState.Idle:
								Idle();
								break;
						case KittenState.Follow:
								Follow();
								break;
						case KittenState.Deposit:
								Deposit();
								break;
						case KittenState.ResetPosition:
								ResetPosition();
								break;
				}
		}

		void Idle()
		{
				if (isWandering == true && (Time.time - wanderTimer > Random.Range(0.75f,2f))) {
						//wandering code
						if (Vector3.Distance(nextPos, transform.position) <= 0.5f) {
								if (Vector3.Distance(startingLocation, transform.position) <= roamingRadius) {
										nextPos = RandomPointsGenerator.RPointGe(transform.position, roamingRadius);
								} else {
										nextPos = startingLocation;
								}
								agent.SetDestination(nextPos);
								wanderTimer = Time.time;
						}
				}
				if (parent != null) {
						state = KittenState.Follow;
				}
		}

		void Follow()
		{
				if (parent != null) {
						agent.SetDestination(parent.transform.position);
				} else {
						state = KittenState.ResetPosition;
				}
		}

		void Deposit()
		{
				if (parent != null) {

						Vector3 dropLoc = new Vector3(parent.transform.position.x, gameObject.transform.position.y, parent.transform.position.z);
						agent.SetDestination(dropLoc);
						//Debug.Log(Vector3.Distance(gameObject.transform.position, dropLoc));
						if (Vector3.Distance(gameObject.transform.position, dropLoc) < 1) {
								if (UIController != null) {
										UIController.GetComponent<UIController>().chainCount += 1;
								}
								if (GameController != null && UIController != null) {
										// added score calculated as follows for each kitten 10 points awarded
										// for a chain of 3:
										// 10 first kitten , (10 + 5*(current chain size(2))) second,(10 + 5*(current chain size (3))) third
										int currentChain = UIController.GetComponent<UIController>().chainCount;
										int addedValue = 10 + (5*currentChain);
										GameController.GetComponent<GameController>().AddScore(addedValue);
								}
								Destroy(gameObject);
						}
				}
		}

		void ResetPosition()
		{
				parent = null;
				child = null;
				agent.SetDestination(startingLocation);
				if (Vector3.Distance(gameObject.transform.position, startingLocation) < 1) {
						gameObject.GetComponent<Collider>().enabled = true;
						state = KittenState.Idle;
				}
		}

		//this is code to scatter kittens if dog hits them
		void OnTriggerEnter(Collider other)
		{
				if (other.gameObject.CompareTag("Dog") && kittensScatterWhenHit == true) {
					if (state == KittenState.Follow) {
						state = KittenState.ResetPosition;
						if(child != null) {
							child.GetComponent<KittenAI>().parent = parent;
						}
					}
				}
		}
}
