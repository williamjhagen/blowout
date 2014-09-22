using UnityEngine;
using System.Collections;

public class DestroyControl : MonoBehaviour {
	public int hp;
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		//Change Foreground to the layer you want it to display on 
		//You could prob. make a public variable for this
		//particleSystem.renderer.sortingLayerName = "Foreground";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D c) {
		if(c.gameObject.tag == "fireball") {
			hp--;
			if(hp <= 0) {
				Instantiate(explosion, transform.position + new Vector3(0,0,2), Quaternion.identity);
				Destroy(gameObject,0.5f);
			}
		}
	}
}
