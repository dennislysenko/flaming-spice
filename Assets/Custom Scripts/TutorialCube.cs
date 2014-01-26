using UnityEngine;
using System.Collections;

public class TutorialCube : MonoBehaviour {

	bool inCube = false;
	bool enterable = false;
	GameObject textManager;
	GameObject unlockShower;
	public Texture2D thiefUnlocked; // 1
	public Texture2D inventorUnlocked; // 2
	public Texture2D birdmanUnlocked; // 3
	public Texture2D ninjaUnlocked; // 4
	public Texture2D minerUnlocked; // 5
	public Texture2D electricianUnlocked; // 6
	public Texture2D ghostUnlocked; // 7


	void Start () {
		DontDestroyOnLoad(GameObject.FindGameObjectWithTag("PersistentLevelManager"));
		textManager = GameObject.FindGameObjectWithTag("TextManager");
		unlockShower = GameObject.FindGameObjectWithTag("UnlockShower");
		StartCoroutine(DisplayUnlockedEgoIfNeeded ());
		//TODO: stop this from showing up past the first load
		textManager.guiText.text = "Welcome to Alterego!";
	}

	IEnumerator DisplayUnlockedEgoIfNeeded() {
		if (PersistentLevelManager.GetMapsCompleted () > 0) {
			switch (PersistentLevelManager.GetUnlock (PersistentLevelManager.GetMapsCompleted () - 1)) {
			case -1:
				break;
			case 1:
				unlockShower.guiTexture.texture = thiefUnlocked;
				unlockShower.guiTexture.enabled = true;
				yield return new WaitForSeconds (5);
				unlockShower.guiTexture.enabled = false;
				break;
			case 2:
				unlockShower.guiTexture.texture = inventorUnlocked;
				unlockShower.guiTexture.enabled = true;
				yield return new WaitForSeconds (5);
				unlockShower.guiTexture.enabled = false;
				break;
			case 3:
				unlockShower.guiTexture.texture = birdmanUnlocked;
				unlockShower.guiTexture.enabled = true;
				yield return new WaitForSeconds (5);
				unlockShower.guiTexture.enabled = false;
				break;
			case 4:
				unlockShower.guiTexture.texture = ninjaUnlocked;
				unlockShower.guiTexture.enabled = true;
				yield return new WaitForSeconds (5);
				unlockShower.guiTexture.enabled = false;
				break;
			case 5:
				unlockShower.guiTexture.texture = minerUnlocked;
				unlockShower.guiTexture.enabled = true;
				yield return new WaitForSeconds (5);
				unlockShower.guiTexture.enabled = false;
				break;
			case 6:
				unlockShower.guiTexture.texture = electricianUnlocked;
				unlockShower.guiTexture.enabled = true;
				yield return new WaitForSeconds (5);
				unlockShower.guiTexture.enabled = false;
				break;
			case 7:
				unlockShower.guiTexture.texture = ghostUnlocked;
				unlockShower.guiTexture.enabled = true;
				yield return new WaitForSeconds (5);
				unlockShower.guiTexture.enabled = false;
				break;
			}
		}
	}
	
	void Update () {
		
		if (inCube && enterable && Input.GetKey("e")) {
			Application.LoadLevel("map_tutorial");
		}
		
	}
	
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name == "Alterego Player") {
			inCube = true;
			if (PersistentLevelManager.IsLevelCompleted("map_tutorial")) {
				textManager.guiText.text = "Level already completed. Press E to enter the level...";
				enterable = true;
			}
			else {
				textManager.guiText.text = "Press E to enter the level...";
				if (PersistentLevelManager.IsNextLevel("map_tutorial") ) {
					enterable = true;
				}
				else {
					textManager.guiText.text = "You need to complete all previous levels.";
					enterable = false;
				}
			}

		}
	}
	
	void OnTriggerExit (Collider col) {
		if (col.gameObject.name == "Alterego Player") {
			inCube = false; 
			textManager.guiText.text = "";
		}
	}
}
