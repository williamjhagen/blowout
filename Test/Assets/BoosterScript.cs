using UnityEngine;
using System.Collections;

public class BoosterScript : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	void OnTriggerStay2D(Collider2D c) {
		print("stay collider");
		if(c.tag == "Player") {
			c.rigidbody2D.AddForce(c.transform.right * speed);
		}else if(c.tag == "TPlayer") {
			c.rigidbody2D.AddForce(c.transform.right * speed);
			//CameraFade cf = GameObject.Find("Main Camera").GetComponent<CameraFade>();
			GameObject cf = GameObject.Find("Main_Camera");
			cf.SendMessage("FadeCamera");
		}
	}
}