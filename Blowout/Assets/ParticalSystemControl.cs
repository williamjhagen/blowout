using UnityEngine;
using System.Collections;

public class ParticalSystemControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		particleSystem.renderer.sortingLayerName = "ForeGround";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
