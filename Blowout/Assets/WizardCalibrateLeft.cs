using UnityEngine;
using System.Collections;

public class WizardCalibrateLeft : MonoBehaviour {
	float recordZ;
	float lockRotation;
	// Use this for initialization
	void Start () {
		recordZ = transform.position.z;
		//lockRotation = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, transform.position.y, recordZ);
		//transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, 0);
	}
}
