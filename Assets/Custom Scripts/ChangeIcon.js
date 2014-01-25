#pragma strict
var standard : Texture2D;
var thief : Texture2D;
var inventor : Texture2D;
var birdman : Texture2D;
var ninja : Texture2D;
var miner : Texture2D;
var electrician : Texture2D;

static var currentEgo = 1;

function Update () {
	switch(currentEgo)
	{
		case 1:
			guiTexture.texture = standard;
		break;
		case 2:
			guiTexture.texture = thief;
		break;
		case 3:
			guiTexture.texture = birdman;
		break;
		case 4:
			guiTexture.texture = inventor;
		break;
		case 5:
			guiTexture.texture = miner;
		break;
	}
}