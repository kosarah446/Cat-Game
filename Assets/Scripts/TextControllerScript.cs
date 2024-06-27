using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextControllerScript : MonoBehaviour
{

		public GameObject instructionsTextObject;

    // Start is called before the first frame update
    void Start()
    {
        instructionsTextObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad > 5) {
						instructionsTextObject.SetActive(false);
				}
    }
}
