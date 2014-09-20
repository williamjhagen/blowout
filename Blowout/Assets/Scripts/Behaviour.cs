using UnityEngine;
using System.Collections;

public abstract class Behaviour : MonoBehaviour {
	public bool active = false;
	public float speed;
	public GameObject target;
	public float threatRange;

	public enum States{
		Idle,
		Attacking, 
		Boasting,
		Running
	}
	public States state;

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

	public IEnumerator UpdateState(){
		while (true) {
			Vector2 d = rigidbody2D.position - target.rigidbody2D.position;
			if(state == States.Idle){
				if(d.magnitude < threatRange){
					state = States.Attacking;
				}
			}
			else if(state == States.Attacking){
				if(d.magnitude > threatRange * 2){
					state = States.Idle;
				}
			}
			yield return state;
		}
	}
}
