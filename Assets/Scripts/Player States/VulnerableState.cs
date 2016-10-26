using UnityEngine;
using System.Collections;

public class VulnerableState : IPlayerState {

	private readonly StatePatternPlayer player;

	public VulnerableState(StatePatternPlayer statePatternPlayer){
		player = statePatternPlayer;
	}

	public void UpdateState(){
		if(player.invincible){
			invincibleStatePattern();
		}
	}

	public void OnCollisionEnter2D (Collision2D coll){
		if (coll.gameObject.tag == "enemy"){
			if(((RedGuy)coll.gameObject.GetComponent (typeof(RedGuy))).isAbove){
//				Destroy (coll.gameObject);
			} 
			else {
				player.health = player.health - 1;
				player.invincible = true;
				if(player.health == 0){
//					Destroy(GameObject.Find("Player"));
				}
			}
		}
	}

	public void vulnerableStatePattern(){
		Debug.Log("I'm vulnerable, I can't be in a more vulnerable state");
	}

	public void invincibleStatePattern(){
		player.currentState = player.invincibleState;
	}

	public IEnumerator invincibleTime(){
		yield return null;
		Debug.Log("I'm vulnerable, I can't be using a Coroutine");
	}
}
