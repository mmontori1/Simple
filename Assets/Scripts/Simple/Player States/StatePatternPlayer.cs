using UnityEngine;
using System.Collections;

public class StatePatternPlayer : MonoBehaviour {

	public int health;
	public bool invincible;
	public bool once;

	[HideInInspector] public IPlayerState currentState;
	[HideInInspector] public InvincibleState invincibleState;
	[HideInInspector] public VulnerableState vulnerableState;

	private void Awake(){
		invincibleState = new InvincibleState (this);
		vulnerableState = new VulnerableState (this);
	}

	// Use this for initialization
	void Start () {
		currentState = vulnerableState;
		health = 8;
		invincible = false;
		once = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(once){
			once = false;
			StartCoroutine (invincibleT());
			currentState.UpdateState();
		}
		else{
			currentState.UpdateState();
		}
	}

	private void OnCollisionEnter2D (Collision2D coll){
		currentState.OnCollisionEnter2D(coll);
	}

	private IEnumerator invincibleT(){
//		Debug.Log (Time.time);
		yield return new WaitForSeconds(3f);
		invincible = false;
//		Debug.Log (Time.time);
	}
}
