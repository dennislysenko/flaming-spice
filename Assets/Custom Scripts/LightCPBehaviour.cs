using UnityEngine;
using System.Collections;

public class LightCPBehaviour : MonoBehaviour {
	public GameObject[] lightsControlled;
	public GameObject[] guardsLinked;

	private bool lightsEnabled = true;

	public void DisableLights() {
		lightsEnabled = false;
		if(guardsLinked.Length > 0)
		foreach (GameObject guard in guardsLinked) {
			guard.GetComponent<GuardScript>().inLight = false;
		}
	}
	public bool lightsOn(){
		return lightsEnabled;

	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!lightsEnabled) {
			foreach(GameObject light in lightsControlled){
				light.light.color -= Color.white * Time.deltaTime / 2.0f;
			}
		}
	}
}