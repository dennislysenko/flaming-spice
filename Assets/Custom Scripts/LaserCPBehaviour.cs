using UnityEngine;
using System.Collections;

public class LaserCPBehaviour : MonoBehaviour {
	public GameObject[] lasersControlled;

	public void DisableLasers() {
		foreach (GameObject laser in lasersControlled) {
			laser.transform.Find ("Beam").gameObject.SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
