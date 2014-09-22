using UnityEngine;
using System.Collections;

public class fireballControlR : MonoBehaviour {
	public float speed;
	private Vector2 direction;
	public GameObject hand;
	private Vector2 handVector;
	// Use this for initialization
	void Start () {
		hand = GameObject.Find("right_wrist");
	}
	
	// Update is called once per frame
	void Update () {
		HandControl h = hand.GetComponent<HandControl>();
		handVector = h.handV;
		transform.Translate(handVector * speed);
		Destroy(gameObject, 3f);
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		print("Enter");
	}
}
