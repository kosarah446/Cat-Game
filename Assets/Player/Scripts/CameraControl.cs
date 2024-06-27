using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;

		void Start()
		{
				player = GameObject.Find("Player").transform;
				//transform.position = player.transform.position + new Vector3(0.26f, 1.3f, -1.5f);
				transform.rotation = Quaternion.Euler(45, 0, 0);
		}

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 5, -5);
    }
}

