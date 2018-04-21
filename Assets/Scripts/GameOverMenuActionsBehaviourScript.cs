using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuActionsBehaviourScript : MonoBehaviour {

	public void OnQuit() {
		Application.Quit ();
	}

	public void OnPlay(string StartLevel) {
		SceneManager.LoadScene (StartLevel);
	}
}
