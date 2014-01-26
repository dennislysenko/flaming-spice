using UnityEngine;
using System.Collections;

public class PersistentLevelManager : MonoBehaviour {

	private static string[] listOfLevels = new string[11];
	private static bool[] levelCompletion = new bool[11];
	private static int mapsCompleted = 0;
	private static int[] listOfUnlocks = {1, -1, 2, 3, 4, 5, 6, -1, 7, -1, -1}; 

	private static bool thiefUnlocked = false;


	// Use this for initialization
	void Start () {
				if (mapsCompleted == 0) {
						//read save data
						listOfLevels [0] = "map_tutorial";
						levelCompletion [0] = false;
						listOfLevels [1] = "map_thief1";
						levelCompletion [1] = false;
						listOfLevels [2] = "map_thief2";
						levelCompletion [2] = false;
						listOfLevels [3] = "map_inventor1";
						levelCompletion [3] = false;
						listOfLevels [4] = "map_birdman1";
						levelCompletion [4] = false;
						listOfLevels [5] = "map_ninja1";
						levelCompletion [5] = false;
						listOfLevels [6] = "map_miner1";
						levelCompletion [6] = false;
						listOfLevels [7] = "map_electrician1";
						levelCompletion [7] = false;
						listOfLevels [8] = "map_electrician2";
						levelCompletion [8] = false;
						listOfLevels [9] = "map_ghost1";
						levelCompletion [9] = false;
						listOfLevels [10] = "map_ghost2";
						levelCompletion [10] = false;

						if (PlayerPrefs.HasKey ("mapsCompleted")) {
								mapsCompleted = PlayerPrefs.GetInt ("mapsCompleted");
								for (int i = 0; i < mapsCompleted; i++)
										levelCompletion [i] = true;
						}
				}
		}
	
	// Update is called once per frame
	void Update () {

	}

	public static int GetUnlock(int index) {
		return listOfUnlocks [index];
	}

	public static void LevelCompleted(string justFinished) {
		// completed a new level
		if (IndexOf (justFinished) >= mapsCompleted) {
			levelCompletion[mapsCompleted] = true;
			mapsCompleted++;
			PlayerPrefs.SetInt ("mapsCompleted", mapsCompleted);
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
		return thiefUnlocked || mapsCompleted >= 1;
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
