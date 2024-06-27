//Emma Gonzales
//Root Motion Control Script
//Used for player character and animations

//Based heavily off of M assignment script of the same name with alterations for this project


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//require some things the bot control needs
[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(BoxCollider))]
[RequireComponent(typeof(CharacterInputController))]
public class RootMotionControlScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;
    private CharacterInputController cinput;

    public float initalMatchTargetsAnimTime = 0.25f;
    public float exitMatchTargetsAnimTime = 0.75f;

    public float animationSpeed = 1f;
    public float rootMovementSpeed = 1f;
    public float rootTurnSpeed = 1f;



    // classic input system only polls in Update()
    // so must treat input events like discrete button presses as
    // "triggered" until consumed by FixedUpdate()...
    bool _inputActionFired = false;

    // ...however constant input measures like axes can just have most recent value
    // cached.
    float _inputForward = 0f;
    float _inputTurn = 0f;




    //JUMPING VARAIBLES
    bool isJumping; //checks whether or not player is jumping & still in air
    bool canJump = true; //is player grounded and able to jump again? 
    public float jump; // Upward force of jump
    public float jumpForward; //forward force of jump

    private float velz; //vertical velocity (useful for jumping/falling animations)


    void Awake()
    {
        //get animator
        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");

        rbody = GetComponent<Rigidbody>();

        if (rbody == null)
            Debug.Log("Rigid body could not be found");

        cinput = GetComponent<CharacterInputController>();
        if (cinput == null)
            Debug.Log("CharacterInput could not be found");

        canJump = true;

        //set vertical velocity variable
        velz = rbody.velocity.y;

    }


    private void Update()
    {
        if (cinput.enabled)
        {
            anim.speed = animationSpeed;

            _inputForward = cinput.Forward;
            _inputTurn = cinput.Turn;

						//if player is moving backwards, play rotate player 180 degrees
						if (_inputForward < 0) {
								transform.Rotate(0, 180, 0);
						}

            //up and down velocity used for jumping
            velz = rbody.velocity.y;

            // Note that we don't overwrite a true value already stored
            // Is only cleared to false in FixedUpdate()
            // This makes certain that the action is handled!
            _inputActionFired = _inputActionFired || cinput.Action;

           
            //Jump Controls
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //makes sure cat is able to jump and isn't in the air
                if (canJump == true && isJumping == false)
                {
                    //set jumping varaible
                    isJumping = true;
                    anim.SetBool("isJumping", isJumping);
                    
                    //Extra force for control purposes
                    rbody.velocity += jump * Vector3.up; //upward force
                    rbody.AddRelativeForce(Vector3.forward * jumpForward * _inputForward, ForceMode.Impulse); //forward force

                    //Player CANNOT jump again until grounded
                    canJump = false;
                    anim.SetBool("canJump", canJump);

                }

            }

        }


    }


    void FixedUpdate()
    {
        //animation variables for blend tree purposes
        anim.SetFloat("velx", _inputTurn); //turn varaible
        anim.SetFloat("vely", _inputForward); //forwards varable
        anim.SetFloat("velz", velz); //upward variable
    }


   
    void OnCollisionEnter(Collision collision)
    {
        //When entering ground
        if (collision.transform.gameObject.tag == "ground")
        {
            //reset Jump Logic when back on ground
            canJump = true;
            anim.SetBool("canJump", canJump);

            isJumping = false;
            anim.SetBool("isJumping", isJumping);
        }

    }


    private void OnCollisionExit(Collision collision)
    {
        //When leaving ground
        if (collision.transform.gameObject.tag == "ground")
        {
            //Block jump logic
            canJump = false;
            anim.SetBool("canJump", canJump);
        }

    }

    void OnAnimatorMove()
    {

        Vector3 newRootPosition;
        Quaternion newRootRotation;

        bool isGrounded = true;

        if (isGrounded)
        {
            //use root motion as is if on the ground		
            newRootPosition = anim.rootPosition;
        }
        else
        {
            //Simple trick to keep model from climbing other rigidbodies that aren't the ground
            newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z);
        }

        //use rotational root motion as is
        newRootRotation = anim.rootRotation;

        //Root Motion movement
        newRootPosition = Vector3.LerpUnclamped(this.transform.position, newRootPosition, rootMovementSpeed);
        newRootRotation = Quaternion.LerpUnclamped(this.transform.rotation, newRootRotation, rootTurnSpeed);

        rbody.MovePosition(newRootPosition);
        rbody.MoveRotation(newRootRotation);
    }




}
