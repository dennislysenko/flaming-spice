using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class TutorialExit : MonoBehaviour {
	
	public string level;
	private bool firstWin = true;
	
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name == "Alterego Player") {
			if (firstWin) {
				firstWin = false;
				Application.LoadLevel ("map_tutorial_secondtime");
			} else {
				PersitentLevelManager.LevelCompleted(level);
				Application.LoadLevel("LevelSelect");
			}
		}
	}
}