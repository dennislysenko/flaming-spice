using UnityEngine;
using System.Collections;

public class TileYZ : MonoBehaviour {

	public float ScaleToTiles = 0.667f;

	void Start () {
		Vector2 v = renderer.material.mainTextureScale;
		Debug.Log (transform.lossyScale);
		v.x = transform.lossyScale.z*ScaleToTiles;
		v.y = transform.lossyScale.y*ScaleToTiles;
		renderer.material.mainTextureScale = v;
	}
}
