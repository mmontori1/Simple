//#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
//using UnityEditor;

public class Health : MonoBehaviour {

	public Player player;
	public StatePatternPlayer statePattern;
//	public static int startingHealth = 5;
	public int startingHealth;
	public int currentHealth;
	public Vector3 firstHeart = new Vector3 (9.65f, 6.23f, 1f);
	public SpriteRenderer sprite;
	public GameObject health;
	public int k; // = startingHealth;
	public bool isDead;
	public GameObject playerObject;
	GameObject[] healthBar;

	// Use this for initialization
	void Start () {
		playerObject = GameObject.Find("Player");
		statePattern = playerObject.GetComponent<StatePatternPlayer>();
		startingHealth = statePattern.health;
		k = startingHealth;
//		player = this.player;
		healthBar = new GameObject[startingHealth];
		currentHealth = startingHealth;
		createHealth();

	}
	
	// Update is called once per frame
	void Update () {
		updateHealth();
		if(Input.GetKeyDown(KeyCode.R)){
			restartHealth();
		}
	}

	void createHealth(){
		for(int i = 0; i < startingHealth; ++i){
			healthBar[i] = health;
			if(healthBar [i] != null) {
				healthBar[i] = (GameObject) Instantiate (healthBar [i], new Vector3 (firstHeart.x - (i * 1.12918f), firstHeart.y, firstHeart.z), Quaternion.identity, GameObject.Find ("Health Bar").transform);
			}
			healthBar[i].name = "Health " + i;
		}
	}

	void updateHealth(){
		if(!isDead){
			currentHealth = statePattern.health;
		}
		if(currentHealth < k){
			//			Debug.Log ("Health " + (k - 1));
			Destroy (GameObject.Find("Health " + (k - 1)));
			k = k - 1;
		}
		if(currentHealth == 0){
			Debug.Log ("wow");
			Destroy(GameObject.Find("Player"));
//			restartHealth ();
//			isDead = true;
		}
	}

	void restartHealth(){
//		playerObject = GameObject.Find("Player");
//		statePattern = playerObject.GetComponent<StatePatternPlayer>();
		k = startingHealth;
		currentHealth = startingHealth;
		createHealth();
	}
}

//#endif