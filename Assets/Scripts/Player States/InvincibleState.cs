using UnityEngine;
using System.Collections;

public class InvincibleState : IPlayerState  {

	private readonly StatePatternPlayer player;

	public InvincibleState(StatePatternPlayer statePatternPlayer){
		player = statePatternPlayer;
	}

	public void UpdateState(){
//		if(player.invincible){
//			Debug.Log ("is start");
//			StartCoroutine(invincibleTime());
//			Debug.Log ("is finish");
//			player.invincible = false;
//		}
		if(!player.invincible){
			vulnerableStatePattern();
		}
		
	}

	public void OnCollisionEnter2D (Collision2D coll){
		Debug.Log ("AM COLLIDE DURING INVINCI");
//		if(coll.gameObject.tag == "enemy"){
//			if(((RedGuy)coll.gameObject.GetComponent (typeof(RedGuy))).isAbove){
////				Destroy(coll.gameObject);
//			} 
//		}
	}

	public void vulnerableStatePattern(){
		player.currentState = player.vulnerableState;
	}

	public void invincibleStatePattern(){
		Debug.Log("I'm invincible, I can't be in a more invincible state");
	}

	public IEnumerator invincibleTime(){
		yield return null;
	}
}
