using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float life = 7.5f;

	// Use this for initialization
	void Start () 
	{
		Invoke ("Destroy", life);
	}

	void Destroy()
	{
		Destroy (gameObject);
	}
}
