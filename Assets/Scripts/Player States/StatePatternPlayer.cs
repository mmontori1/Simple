using UnityEngine;
using System.Collections;

public class StatePatternPlayer : MonoBehaviour {

//	public bool isVulnerable;
	public int health;
	public bool invincible;
//	public bool 

	[HideInInspector] public IPlayerState currentState;
	[HideInInspector] public InvincibleState invincibleState;
	[HideInInspector] public VulnerableState vulnerableState;

	private void Awake(){
		invincibleState = new InvincibleState (this);
		vulnerableState = new VulnerableState (this);
		//		GetComponent<InvincibleState>();
		//		GetComponent<VulnerableState>();
		//		gameObject.AddComponent ( typeof ( InvincibleState ) ) as InvincibleState;
		//		gameObject.AddComponent ( typeof ( VulnerableState ) ) as VulnerableState;
		//		gameObject.AddComponent<InvincibleState>();
		//		gameObject.AddComponent<VulnerableState>();
//				invincibleState = GetComponent<InvincibleState>();
//				vulnerableState = GetComponent<VulnerableState>();
	}

	// Use this for initialization
	void Start () {
		currentState = vulnerableState;
		health = 5;
		invincible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentState == invincibleState){
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
		Debug.Log (Time.time);
		yield return new WaitForSeconds(3f);
		invincible = false;
		Debug.Log (Time.time);
	}
}
