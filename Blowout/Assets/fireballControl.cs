using UnityEngine;
using System.Collections;

public class fireballControl : MonoBehaviour {
	public float speed;
	private Vector2 direction;
	public GameObject hand;
	private float timeToDie;
	// Use this for initialization
	void Start () {
		hand = GameObject.Find("left_wrist");
		timeToDie = 2;
		//print ("timetodie");
	}

	// Update is called once per frame
	void Update () {
		HandControl h = hand.GetComponent<HandControl>();
		transform.Translate(h.handV * speed);
		Destroy(gameObject, 3f);
		/*
		if(timeToDie <= 0) {
			print ("destroy");
			Destroy(gameObject);
		}else {
			print ("time --");
			timeToDie -= Time.deltaTime;
		}*/
	}

	void OnCollisionEnter2D(Collision2D coll) {
		print("Enter");
		//Destroy(gameObject);
	}

}
