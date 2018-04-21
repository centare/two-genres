using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuActionsBehaviourScript : MonoBehaviour {
	void Start() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void OnQuit() {
		Application.Quit ();
	}

	public void OnPlay(string StartLevel) {
		SceneManager.LoadScene (StartLevel);
	}
}
