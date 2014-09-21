using UnityEngine;
using System.Collections;

public class NPC_Controller : MonoBehaviour {
	public float maxDX;
	public float maxDY;	
	public float maxMag;
	ArrayList InactiveObjs;
	ArrayList ActiveObjs;   
	public enum EnemyTypes{
		Walker,
		Flyer
	}
	// Use this for initialization
	void Start () {
		InactiveObjs = new ArrayList ();
		ActiveObjs = new ArrayList ();
		init ();
	}
	
	// Update is called once per frame
	void Update () {
		//update each object
		foreach (GameObject e in ActiveObjs) {
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
		ActiveObjs.Add(GameObject.FindGameObjectWithTag("Enemy"));
		GameObject.FindGameObjectWithTag ("Enemy").GetComponent<Flyer>().Init ();
	}

	public GameObject CreateRandomEnemy(){
		bool walker = Random.value > .5f;
		return null;
	}

	public GameObject CreateEnemy(Vector2 pos, EnemyTypes et){
		if (InactiveObjs.Count == 0)
			return null;
		GameObject temp = (GameObject)InactiveObjs [0];
		InactiveObjs.Remove (temp);
		ActiveObjs.Add (temp);
		temp.rigidbody2D.position = pos;
		if (et == EnemyTypes.Walker)
			temp.GetComponent<Walker> ().Init ();
		else if (et == EnemyTypes.Flyer)
			temp.GetComponent<Flyer> ().Init ();
		return temp;
		
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
			temp.x = temp.x > 0 ? maxDX : -maxDX;
			g.rigidbody2D.velocity = temp;
		}
		//constrain velocity on vertical
		if (Mathf.Abs (g.rigidbody2D.velocity.y) > maxDY) {
			Vector2 temp = g.rigidbody2D.velocity;
			temp.y = temp.y > 0 ? maxDY : -maxDY;
			g.rigidbody2D.velocity = temp;
		}
	}
}
