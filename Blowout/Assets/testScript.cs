﻿using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.right * Time.deltaTime * 5);
	}
}
