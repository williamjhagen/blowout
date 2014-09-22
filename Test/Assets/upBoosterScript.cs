using UnityEngine;
using System.Collections;

public class upBoosterScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay2D(Collider2D c) {
		print("stay collider");
		if(c.tag == "Player") {
			c.rigidbody2D.AddForce(c.transform.up * 100);
			//Instantiate(explosion, transform.position, Quaternion.identity);
		}
	}
}
