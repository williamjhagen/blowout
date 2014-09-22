using UnityEngine;
using System.Collections;

public class OpeningPortal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player")
						Application.LoadLevel ("GameScene");
		print (coll.gameObject.name);
		
	}
}
