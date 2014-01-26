using UnityEngine;
using System.Collections;

public class ExitTrigger : MonoBehaviour {

	public string level;

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name == "Alterego Player") {
			PersitentLevelManager.LevelCompleted(level);
			Application.LoadLevel("LevelSelect");
		}
	}
}