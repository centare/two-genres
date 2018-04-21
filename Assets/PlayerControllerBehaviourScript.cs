using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var jumpPressed = Input.GetButtonDown("Jump");

        var rightAxis = Input.GetAxis("RightMove");
        var leanRightAxis = Input.GetAxis("RightLean");
	}
}
