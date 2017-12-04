using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

    // Use this for initialiaztion
	void Start () {
        offset = transform.position - player.transform.position;
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position + offset;

    }
		
}

