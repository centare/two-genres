using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerBehaviourScript : MonoBehaviour 
{

	const string RightLeanKey = "RightLean";
	public float MaxLean = 45.0f;
	public float LeanDeadValue = 2.0f;
	public float LeanCountdownSeconds = 5.0f;

	bool leanTimerExpired;
	bool forceFall;
	float currentLeanCountdownSeconds;
	float currentLean;
	Rigidbody body;

	public float leanSpeed = 1.0f;
	public Transform pivotPoint;
	// Use this for initialization
	void Start () {
		currentLean = 0.0f;
		forceFall = false;
		currentLeanCountdownSeconds = LeanCountdownSeconds;
		leanTimerExpired = false;
		body = GetComponent<Rigidbody> ();
	}

	void Awake() {
		if (pivotPoint == null) {
			pivotPoint = transform.parent;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var leanRightAxis = Input.GetAxis(RightLeanKey);
		if (!ShouldFall ()) {
			float direction = 0.0f;
			if (leanRightAxis > Mathf.Epsilon) {
				direction = -1.0f;
			} else if (leanRightAxis < -Mathf.Epsilon) {
				direction = 1.0f;
			}

			currentLean = Mathf.MoveTowardsAngle (currentLean, (MaxLean * 2.0f) * direction, leanSpeed * Time.deltaTime); 
			pivotPoint.localRotation = Quaternion.AngleAxis (currentLean, Vector3.forward);

			if (currentLean > LeanDeadValue || currentLean < -LeanDeadValue) {
				currentLeanCountdownSeconds -= Time.deltaTime;
				if (currentLeanCountdownSeconds <= 0.0f) {
					leanTimerExpired = true;
				} 
			} else {
				leanTimerExpired = false;
				currentLeanCountdownSeconds = LeanCountdownSeconds;
			}
		}

		if (ShouldFall ()) {
			body.constraints = RigidbodyConstraints.None;
			if (!forceFall) {
				forceFall = true;
				body.AddForce (Vector3.forward * 30.0f * (currentLean > 0.0f ? 1.0f : -1.0f), ForceMode.Impulse);
				body.AddForce (Vector3.up * -80.0f, ForceMode.Impulse);
			}
		}
	}

	bool ShouldFall() {
		return (currentLean > MaxLean || currentLean < -MaxLean) || leanTimerExpired;
	}
}
