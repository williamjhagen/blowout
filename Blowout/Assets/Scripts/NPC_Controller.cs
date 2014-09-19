using UnityEngine;
using System.Collections;

public class NPC_Controller : MonoBehaviour {
	
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
				}
			}
		}
	}

	void init(){
		//temp enemy for testing
		ObjectPool.Add(GameObject.FindGameObjectWithTag("Enemy"));

	}
}
