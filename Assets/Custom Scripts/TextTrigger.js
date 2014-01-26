#pragma strict

var inCube : boolean = false;
var textManager : GameObject;
var msg : String;

function Start () {
	textManager = GameObject.FindGameObjectWithTag("TextManager");
}

function Update () {
}

function OnTriggerEnter (col : Collider) {
	if (col.gameObject.name == "Alterego Player") {
		inCube = true;
		textManager.guiText.text = msg;
	}
}

function OnTriggerExit (col : Collider) {
	if (col.gameObject.name == "Alterego Player") {
		inCube = false; 
		textManager.guiText.text = "";
	}
}