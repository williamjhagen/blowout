using UnityEngine;
using System.Collections;

public class fireballControlR : MonoBehaviour {
	public float speed;
	private Vector2 direction;
	public GameObject hand;
	// Use this for initialization
	void Start () {
		hand = GameObject.Find("right_wrist");
		//print ("timetodie");
	}
	
	// Update is called once per frame
	void Update () {
		HandControl h = hand.GetComponent<HandControl>();
		transform.Translate(h.handV * speed);
		Destroy(gameObject, 3f);
		/*if(timeToDie <= 0) {
			print ("destroy");
			Destroy(gameObject);
		}else {
			print ("time --");
			timeToDie -= Time.deltaTime;
		}*/
		//transform.Translate(new Vector2(0,1) * speed);
		//print (direction * speed);
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		print("Enter");
		//Destroy(gameObject);
	}
}
