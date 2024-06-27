//Emma Gonzales
//Root Motion Control Script
//Used for player input

//Based heavily off of M assignment script of the same name with alterations for this project

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class CharacterInputController : MonoBehaviour {

    public string Name = "name goes here";

    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;

    public bool InputMapToCircular = true;

    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;

    private float forwardSpeedLimit = 1f;


    public float Forward
    {
        get;
        private set;
    }

    public float Turn
    {
        get;
        private set;
    }

    public bool Action
    {
        get;
        private set;
    }

    public bool Jump
    {
        get;
        private set;
    }

        

	void Update () {

				float h = 0f;
				float v = 0f;

				//if left mouse button is pressed, get horizontal and vertical input axis based on mouse position relative to player
				if (Input.GetMouseButton(0)) {
							Vector3 mousePos = Input.mousePosition;
						mousePos.z = 10; // distance from the camera
						Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
						mousePos.x = mousePos.x - objectPos.x;
						mousePos.y = mousePos.y - objectPos.y;
						 h = Mathf.Clamp(mousePos.x,-100f,100f)/100;
						 v = Mathf.Clamp(mousePos.y,-100f,100f)/100;;

						//debug
						//Debug.Log("Mouse Position: " + mousePos);

				} else {
						//keyboard/controller input
						//GetAxisRaw() so we can do filtering here instead of the InputManager
						 h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
						 v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis
				}
        
    


        if (InputMapToCircular)
        {
            // make coordinates circular
            //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
            h = h * Mathf.Sqrt(1f - 0.5f * v * v);
            v = v * Mathf.Sqrt(1f - 0.5f * h * h);

        }


        //BEGIN ANALOG ON KEYBOARD DEMO CODE
        if (Input.GetKey(KeyCode.Q))
            h = -0.5f;
        else if (Input.GetKey(KeyCode.E))
            h = 0.5f;

        if (Input.GetKeyUp(KeyCode.Alpha1))
            forwardSpeedLimit = 0.1f;
        else if (Input.GetKeyUp(KeyCode.Alpha2))
            forwardSpeedLimit = 0.2f;
        else if (Input.GetKeyUp(KeyCode.Alpha3))
            forwardSpeedLimit = 0.3f;
        else if (Input.GetKeyUp(KeyCode.Alpha4))
            forwardSpeedLimit = 0.4f;
        else if (Input.GetKeyUp(KeyCode.Alpha5))
            forwardSpeedLimit = 0.5f;
        else if (Input.GetKeyUp(KeyCode.Alpha6))
            forwardSpeedLimit = 0.6f;
        else if (Input.GetKeyUp(KeyCode.Alpha7))
            forwardSpeedLimit = 0.7f;
        else if (Input.GetKeyUp(KeyCode.Alpha8))
            forwardSpeedLimit = 0.8f;
        else if (Input.GetKeyUp(KeyCode.Alpha9))
            forwardSpeedLimit = 0.9f;
        else if (Input.GetKeyUp(KeyCode.Alpha0))
            forwardSpeedLimit = 1.0f;
				//END ANALOG ON KEYBOARD DEMO CODE  

				//https://gamedev.stackexchange.com/questions/188430/how-to-get-the-correct-input-direction-based-on-the-camera-angle-to-use-in-a-roo
				//vector of input
				Vector3 groundNormal = Vector3.up;

				Vector3 controllerSpaceInput = new Vector3(v,
																									 0f,
																									 h);

				Vector3 worldSpaceInput = CameraRelativeFlatten(controllerSpaceInput, groundNormal);

				Vector3 localSpaceInput = transform.TransformDirection(worldSpaceInput);

				//do some filtering of our input as well as clamp to a speed limit
				filteredForwardInput = Mathf.Clamp(Mathf.Lerp(localSpaceInput.x, v, 
            Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);

        filteredTurnInput = Mathf.Lerp(localSpaceInput.z, h, 
            Time.deltaTime * turnInputFilter);


				Forward = filteredForwardInput;
        Turn = filteredTurnInput;
        Jump = Input.GetButtonDown("Jump");

	}

		Vector3 CameraRelativeFlatten(Vector3 input, Vector3 localUp)
		{
				Transform cam = Camera.main.transform; // You can cache this to save a search.

				Quaternion flatten = Quaternion.LookRotation(
																						-localUp,
																						cam.forward
																			 )
																				* Quaternion.Euler(-90f, 0, 0);

				return flatten * input;
		}
}
