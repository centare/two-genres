using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerBehaviourScript : MonoBehaviour 
{
	const string JumpKey = "Jump";
	const string RightAxisKey = "RightMove";
	const string RightLeanKey = "RightLean";
	const float MaxLean = 0.4f;
	const float LeanDeadValue = 0.05f;
	const float LeanCountdownSeconds = 5.0f;

	bool leanTimerExpired;
    bool isJumping;
	float currentLeanCountdownSeconds;
	Rigidbody body;

	public float jumpSpeed = 800.0f;
	public float moveSpeed = 100.0f;
	public float leanSpeed = 200.0f;

	// Use this for initialization
	void Start () {
		currentLeanCountdownSeconds = LeanCountdownSeconds;
		leanTimerExpired = false;
        isJumping = false;
		body = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		var jumpPressed = Input.GetButtonDown(JumpKey);

		var rightAxis = Input.GetAxis(RightAxisKey);
		var leanRightAxis = Input.GetAxis(RightLeanKey);

		if(jumpPressed && !isJumping)
        {
            isJumping = true;
			body.AddForce (Vector3.up * jumpSpeed, ForceMode.Impulse);
        }

		body.AddForce (Vector3.right * moveSpeed * rightAxis, ForceMode.Impulse);

		if (!ShouldFall ()) {
			transform.Rotate (new Vector3 (leanRightAxis * leanSpeed * Time.deltaTime, 0.0f, 0.0f));

			if (transform.rotation.x < LeanDeadValue && transform.rotation.x > -LeanDeadValue) {
				currentLeanCountdownSeconds = LeanCountdownSeconds;
			} else {
				currentLeanCountdownSeconds -= Time.deltaTime;
			}
		}

		if (ShouldFall ()) {
			body.constraints = RigidbodyConstraints.None;
		}
	}

	bool ShouldFall() {
		return (transform.rotation.x > MaxLean || transform.rotation.x < -MaxLean) || leanTimerExpired;
	}

	void OnCollisionStay() {
		isJumping = false;
	}
}
