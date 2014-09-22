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
	void Start () {
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
					if(b.state == Behaviour.States.Dead){
						b.Deactivate();
						ActiveObjs.Remove(e);
						InactiveObjs.Add(e);
						e.SetActive(false);
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
		bool walker = Random.value > .5f;
		Vector2 pos = Vector2.zero;
		Camera c = GameObject.FindGameObjectWithTag ("MainCamera").camera;
		GameObject sp = GameObject.FindGameObjectWithTag ("SpawnPoint");
		pos.x = sp.transform.position.x;
		pos.y = sp.transform.position.y + (walker ? 0f : 9f);
		return CreateEnemy (pos, (walker ? EnemyTypes.Walker : EnemyTypes.Flyer));
	}

	public GameObject CreateEnemy(Vector2 pos, EnemyTypes et){
		if (InactiveObjs.Count == 0)
			return null;
		GameObject temp = (GameObject)InactiveObjs [0];
		InactiveObjs.Remove (temp);
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
