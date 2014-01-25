using UnityEngine;
using System.Collections;

public class FlameFollowPlayer : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
		Debug.Log ("Jesus just rose again");
	}
	
	// Update is called once per frame
	void Update () {
		if (false) { // TEMP DISABLE THIS BEHAVIOR
			transform.position = player.transform.position;
		}
	}
}
