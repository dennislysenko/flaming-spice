using UnityEngine;
using System.Collections;

public class PersitentLevelManager : MonoBehaviour {

	private static string[] listOfLevels = new string[2];
	private static bool[] levelCompletion = new bool[2];
	private static int mapsCompleted = 0;

	private static bool thiefUnlocked = false;


	// Use this for initialization
	void Start () {
		if (mapsCompleted == 0) {
			listOfLevels [0] = "map_tutorial";
			levelCompletion [0] = false;
			listOfLevels [1] = "map_thief1";
			levelCompletion [1] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void LevelCompleted(string justFinished) {
		// completed a new level
		if (IndexOf (justFinished) >= mapsCompleted) {
			levelCompletion[mapsCompleted] = true;
			mapsCompleted++;
		}
	}

	public static bool IsLevelCompleted(string level) {
		if (IndexOf(level) >= 0) {
			return levelCompletion [IndexOf (level)];
		}
		else {
			return false;
		}
	}

	public static bool IsNextLevel(string level) {
		if (IndexOf(level) >= 1) {
			return levelCompletion [IndexOf (level) - 1];
		}
		else {
			return !IsLevelCompleted(level);
		}
	}

	public static int GetMapsCompleted() {
		return mapsCompleted;
	}

	public static bool thiefIsUnlocked() {
		return thiefUnlocked;
	}

	static int IndexOf (string target) {
		int counter = 0;
		foreach (string val in listOfLevels) {
			if (val == target) {
				return counter;
			}
			counter++;
		}
		return -1;
	}
	public static void unlockThief() {
		thiefUnlocked = true;
	}
}
