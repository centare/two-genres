using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZoneTriggerBehaviour : MonoBehaviour {
	public string objectTagToKill = "Player";
	public static string gameOverSceneName = "GameOver";

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag(objectTagToKill)) {
			SceneManager.LoadScene (gameOverSceneName);
		}
	}
}
