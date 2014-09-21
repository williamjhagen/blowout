using UnityEngine;
using System.Collections;

public abstract class Behaviour : MonoBehaviour {
	public bool active = false;
	public float speed;
	protected GameObject target;
	public float threatRange;

	public enum States{
		Idle,
		Attacking, 
		Boasting,
		Running
	}
	public States state;
	
	public void Tick(){
		//update based on state
		if (state == Behaviour.States.Idle) {
			Idle ();
		}
		else if (state == Behaviour.States.Attacking) {
			Attacking ();
		}
		else if (state == Behaviour.States.Boasting) {
			Boasting ();
		}
		else if (state == Behaviour.States.Running) {
			Running ();
		}
	}

	public void Deactivate(){
		this.enabled = false;
	}

	public abstract void Init();
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
