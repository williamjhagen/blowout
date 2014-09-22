using UnityEngine;
using System.Collections;

public class HandControl : MonoBehaviour {
	public GameObject endOfHand;
	public GameObject body;
	public GameObject rootJoint;
	public GameObject fireball;
	public float force;
	private float nextFireball;
	private float currentTime;
	public Vector2 offSet;
	public Vector2 handV;
	public float timer;
	public GameObject hand;
	// Use this for initialization
	void Start () {
		nextFireball = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer < 0) {
			handV = new Vector2((transform.position - endOfHand.transform.position).x, (transform.position - endOfHand.transform.position).y);
		//_fireball.SendMessage ("SetVector", handV);

		//fireballControl f = fireball.GetComponent<fireballControl>();
		//f.SetVector(handV);

			//Vector2 handV = new Vector2((transform.position - endOfHand.transform.position).x, (transform.position - endOfHand.transform.position).y);
			body.rigidbody2D.AddForce(-handV *force);
			Vector2 fireball_Position = new Vector2(hand.transform.position.x , hand.transform.position.y) + offSet;
			if(Time.time > currentTime + nextFireball) {
				Instantiate(fireball, fireball_Position, Quaternion.identity);
				currentTime = Time.time;
			}else {
				nextFireball -= Time.deltaTime;
			}
		}else {
			timer -= Time.deltaTime;
		}
	}

	void StraightBody() {
		if(Vector2.Angle(body.transform.up, new Vector2(0,1)) > 45) {
			transform.up = new Vector2(0,1);
		}
	}
}
