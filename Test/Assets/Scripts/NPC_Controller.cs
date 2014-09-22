using UnityEngine;
using System.Collections;

public class NPC_Controller : MonoBehaviour {
	public float maxDX;
	public float maxDY;	
	public float maxMag;
	public float floorHeight;
	private float timer = 0;
	public float spawnTime;
	ArrayList InactiveObjs;
	ArrayList ActiveObjs;   
	public enum EnemyTypes{
		Walker,
		Flyer
	}
	// Use this for initialization
	void Awake () {
		init ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//update each object
		int len = ActiveObjs.Count;
		for(int i = len - 1 ;i >= 0; --i){
			GameObject e = (GameObject)ActiveObjs[i];
			//get all behaviours on the object
			Behaviour[] ba = e.GetComponents<Behaviour>();
			//check if any are enabled
			foreach(Behaviour b in ba){
				if(b.enabled){
					if(b.state == Behaviour.States.Dead){
						InactiveObjs.Add(e);
						ActiveObjs.RemoveAt(i);
						b.Deactivate();
						e.SetActive(false);
						break;
					}
					else{
						b.Tick();
						ConstrainVelocity(e);
					}
				}
			}
		}
	}

	void init(){
		InactiveObjs = new ArrayList ();
		ActiveObjs = new ArrayList ();
		GameObject[] e = GameObject.FindGameObjectsWithTag ("Enemy");
		int len = e.Length;
		for (int i = 0; i < len; ++i) {
			InactiveObjs.Add(e[i]);
			e[i].SetActive(false);
		}
		StartCoroutine ("SpawnEnemies");
	}

	public GameObject CreateRandomEnemy(){
		Vector2 pos = Vector2.zero;
		Camera c = GameObject.FindGameObjectWithTag ("MainCamera").camera;
		int rand = Random.Range (0, 4);
		int count = 0;
		GameObject sp;
		bool cont = false;
		do {
			if(count == 4) return null;
			sp = GameObject.FindGameObjectsWithTag ("SpawnPoint") [(rand + count++) % 4];
			foreach(GameObject e in ActiveObjs){
				Vector2 sp2d;
				sp2d.x = sp.transform.position.x;
				sp2d.y = sp.transform.position.y;
				if((e.rigidbody2D.position - sp2d).magnitude < 2f) cont = true;
			}
		} while(cont);
		bool walker = sp.transform.position.y < 1;
		pos.x = sp.transform.position.x;
		pos.y = sp.transform.position.y + (walker ? 0f : 9f);
		return CreateEnemy (pos, (walker ? EnemyTypes.Walker : EnemyTypes.Flyer));
	}

	public GameObject CreateEnemy(Vector2 pos, EnemyTypes et){
		if (InactiveObjs.Count == 0)
			return null;
		GameObject temp = (GameObject)InactiveObjs [0];
		InactiveObjs.RemoveAt(0);
		temp.SetActive(true);
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

	IEnumerator SpawnEnemies(){
		while (true) {
			timer += Time.deltaTime;
			if(timer > spawnTime){
				CreateRandomEnemy();
				timer -= spawnTime;
			}
			yield return null;
		}
	}
}
