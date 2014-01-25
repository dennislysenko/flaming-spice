using UnityEngine;
using System.Collections;

public class LightFollowPlayer : MonoBehaviour {
	GameObject player;

	// Use this for initialization
	void Start () {
		player = transform.root.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + Vector3.up * 0.5f;
		transform.rotation = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().transform.rotation;
	}
}
