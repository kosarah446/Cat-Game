using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class DogAI : MonoBehaviour
{
    NavMeshAgent agent;
    private VelocityReporter velReporter;
    private NavMeshHit hit;
    private bool blocked = false;
		private float blockStart = -1;
		private float blockTime = -1;
		public float heatTimer = 3f;
		private Vector3 startingLocation = new Vector3(0, 0, 0);

		private Animator anim;

    public enum DogState
    {
        Idle,
        Search,
        Chase,
				Reset
    }

    public DogState state;

    public Transform target;
    //public float searchRadius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        velReporter = target.GetComponent<VelocityReporter>();
        state = DogState.Search;

        //get animator
        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");
				
				startingLocation = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case DogState.Idle:
                Idle();
                break;
            case DogState.Search:
                Search();
                break;
            case DogState.Chase:
                Chase();
                break;
						case DogState.Reset:
								Reset();
								break;
        }
    }

    void Idle()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
    }

    void Search()
    {
				//Debug.Log("Searching");
        transform.RotateAround(transform.position, Vector3.up, 0.25f);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        RaycastHit hit;


        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", true);

        if (Physics.Raycast(transform.position, transform.forward * 10, out hit, 100))
        {
            if (hit.collider.tag == "Player")
            {
                state = DogState.Chase;
            }
        }

    }

    void Chase()
    {
				//Debug.Log("Chasing");
        agent.speed = 3;
        agent.angularSpeed = 360;
        float distance = Vector3.Distance(target.transform.position, agent.transform.position);
        float lookAheadT = Mathf.Clamp(distance / agent.speed, 0.0f, 0.01f);
        Vector3 futureTarget = target.transform.position + (velReporter.velocity * lookAheadT);

        anim.SetBool("isRunning", true);
        anim.SetBool("isWalking", false);

        blocked = NavMesh.Raycast(agent.transform.position, target.transform.position, out hit, NavMesh.AllAreas);
				if (blocked) {
						if(blockTime < 0) {
							blockStart = Time.time;
						}
						blockTime = Time.time - blockStart;
						//Debug.Log("Blocked: " + blockTime);
				} else {
						blockTime = -1;
				}
        if (blockTime< heatTimer)
        {
            agent.SetDestination(futureTarget);
				} else {
						blockStart = -1;
						blockTime = -1;
						state = DogState.Search;
				}
    }

		void Reset()
		{
				agent.SetDestination(startingLocation);
				if (Vector3.Distance(gameObject.transform.position, startingLocation) < 1) {
						state = DogState.Search;
				}
		}
}
