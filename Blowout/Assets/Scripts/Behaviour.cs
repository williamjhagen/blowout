using UnityEngine;
using System.Collections;

public abstract class Behaviour : MonoBehaviour {
	private bool active = false;
	public float speed;
	public GameObject target;

	protected enum States{
		Idle,
		Attacking, 
		Boasting,
		Running
	}
	protected States state;

	//start
	void Start(){
		state = States.Idle;
		target = GameObject.FindGameObjectWithTag ("Player");

	}

	public abstract void Tick ();

	public void Deactivate(){
		active = false;
		renderer.enabled = false;
		Destroy (this);
	}


	public abstract bool Idle();
	public abstract bool Attacking();
	public abstract bool Boasting();
	public abstract bool Running();
}
