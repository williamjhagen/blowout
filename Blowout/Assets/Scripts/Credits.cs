﻿using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
		((MovieTexture)(GameObject.FindGameObjectWithTag ("Finish").renderer.material.mainTexture)).Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
