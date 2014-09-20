using UnityEngine;
using System.Collections;

public class NPC_Controller : MonoBehaviour {
	public float maxDX;
	public float maxDY;	
	public float maxMag;
	ArrayList ObjectPool;
	// Use this for initialization
	void Start () {
		ObjectPool = new ArrayList ();
		init ();
	}
	
	// Update is called once per frame
	void Update () {
		//update each object
		foreach (GameObject e in ObjectPool) {
			//get all behaviours on the object
			Behaviour[] ba = e.GetComponents<Behaviour>();
			//check if any are enabled
			foreach(Behaviour b in ba){
				if(b.enabled){
					b.Tick();
					ConstrainVelocity(e);
				}
			}
		}
	}

	void init(){
		//temp enemy for testing
		ObjectPool.Add(GameObject.FindGameObjectWithTag("Enemy"));
		GameObject.FindGameObjectWithTag ("Enemy").GetComponent<Behaviour> ().StartCoroutine ("UpdateState");
	}

	//make sure no NPC moves faster than is reasonable to react to.
	void ConstrainVelocity(GameObject g){
		//first constrain overall velocity
		if (g.rigidbody2D.velocity.magnitude > maxMag) {
			Vector2 temp = g.rigidbody2D.velocity.normalized;
			temp = temp * maxMag;
			g.rigidbody2D.velocity = temp;
		}
		//constrain velocity on horizontal
		if (Mathf.Abs (g.rigidbody2D.velocity.x) > maxDX) {
			Vector2 temp = g.rigidbody2D.velocity;
			temp.x = maxDX;
			g.rigidbody2D.velocity = temp;
		}
		//constrain velocity on vertical
		if (Mathf.Abs (g.rigidbody2D.velocity.y) > maxDY) {
			Vector2 temp = g.rigidbody2D.velocity;
			temp.y = maxDY;
			g.rigidbody2D.velocity = temp;
		}
	}
}
