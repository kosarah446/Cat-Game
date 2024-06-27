using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PickupController : MonoBehaviour
{
    AudioManager audioManager;
    // Variable to keep track of collected "PickUp" objects.
    private int count;

    private GameObject lastKitten = null;

    public Stack<GameObject> kittensList = new Stack<GameObject>();
    public int numKittens = 0;

    public UnityEvent onWinEvent;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            GameObject kitten = other.gameObject;
            KittenAI kittenAI = kitten.GetComponent<KittenAI>();
						if (kittenAI.state == KittenAI.KittenState.Idle) {
								if (kittenAI != null && kittenAI.parent == null) {
                    audioManager.PlaySFX(audioManager.pickup);
										kittenAI.parent = this.gameObject;
                    kittenAI.agent.speed = kittenAI.speed;
                    kittenAI.agent.angularSpeed = kittenAI.angularSpeed;
                    kittenAI.agent.acceleration = kittenAI.acceleration;
                    // kittenAI.agent.speed = 30f;
                    // kittenAI.agent.angularSpeed = 3000;
                    // kittenAI.agent.acceleration = 5;
                    kittenAI.agent.stoppingDistance = 0.2f;
										if (lastKitten != null) {
												lastKitten.GetComponent<KittenAI>().parent = kitten;
												kittenAI.child = lastKitten;
										}

										lastKitten = kitten;
								}

								kittensList.Push(kitten);
								numKittens++;

								kitten.GetComponent<Rigidbody>().isKinematic = true;  //makes the kitten not be acted upon by forces    

								//kitten.GetComponent<Collider>().enabled = false; //makes the kitten not collide with anything

								// Increment the count of "PickUp" objects collected.
								count = count + 1;
						}
        }

        //When player touches portal deposit all kittens
        if (other.gameObject.CompareTag("Portal"))
        {
						GameObject portal = other.gameObject;
            //if player has kittens
            if (count > 0)
            {
                //deposit all kittens
                while (kittensList.Count > 0)
                {
                    GameObject kittenToDeposit = kittensList.Pop();
                    audioManager.PlaySFX(audioManager.deposit);
										if (kittenToDeposit.GetComponent<KittenAI>().state == KittenAI.KittenState.Follow) {
												kittenToDeposit.GetComponent<Collider>().enabled = false;
												kittenToDeposit.GetComponent<KittenAI>().parent = portal;
												kittenToDeposit.GetComponent<KittenAI>().state = KittenAI.KittenState.Deposit;
										}
                }
								lastKitten = null;
						}
        }

				if (other.gameObject.CompareTag("Dog")) {

						//reset dog position
						other.gameObject.GetComponent<DogAI>().state = DogAI.DogState.Reset;

						//if player has kittens
						if (count > 0) {
								//lose all kittens back to their original location
								while (kittensList.Count > 0) {
										GameObject kittenToDeposit = kittensList.Pop();
										if (kittenToDeposit.GetComponent<KittenAI>().state == KittenAI.KittenState.Follow) {
												kittenToDeposit.GetComponent<Collider>().enabled = false;
												kittenToDeposit.GetComponent<KittenAI>().parent = null;
												kittenToDeposit.GetComponent<KittenAI>().state = KittenAI.KittenState.ResetPosition;
										}
										
								}
								lastKitten = null;
						}
				}		
    }
}
