using UnityEngine;
using System.Collections;

public class TutorialExit : MonoBehaviour {
	
	public string level;
	private bool firstWin = true;
	
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name == "Alterego Player") {
			if (firstWin) {
				firstWin = false;
				//PersitentLevelManager.listOfLevels[0] = "map_tutorial_secondtime";
				PersistentLevelManager.unlockThief();
				Application.LoadLevel ("map_tutorial_secondtime");
			} else {
				PersistentLevelManager.LevelCompleted(level);
				Application.LoadLevel("LevelSelect");
			}
		}
	}
}