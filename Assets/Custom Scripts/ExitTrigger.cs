using UnityEngine;
using System.Collections;

public class ExitTrigger : MonoBehaviour {

	public string level;

	void Update() {
		if (Input.GetKeyUp (KeyCode.Escape))
			Application.LoadLevel ("LevelSelect");
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name == "Alterego Player") {
			PersistentLevelManager.LevelCompleted(level);
			Application.LoadLevel("LevelSelect");
		}
	}
}