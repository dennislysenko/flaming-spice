#pragma strict

function Start () {

}

function Update () {

}

function OnGUI () {

	if (GUI.Button(new Rect(162, 290, 60, 50), "Tutorial")) {
		Debug.Log("I am clickzor");
		Application.LoadLevel("map_tutorial");
	}

}