using UnityEngine;
using System.Collections;

public class TileXZ : MonoBehaviour {

	float ScaleToTiles = 0.667f;
	
	void Start () {
		Vector2 v = renderer.material.mainTextureScale;
		Debug.Log (transform.lossyScale);
		v.x = transform.lossyScale.x*ScaleToTiles;
		v.y = transform.lossyScale.z*ScaleToTiles;
		renderer.material.mainTextureScale = v;
	}
}
