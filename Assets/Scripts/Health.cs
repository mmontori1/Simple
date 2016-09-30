﻿#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;

public class Health : MonoBehaviour {

	public Player player;

	public static int startingHealth = 5;
	public int currentHealth;
	public Vector3 firstHeart = new Vector3 (9.65f, 6.23f, 1f);
	public SpriteRenderer sprite;
	public GameObject health;
	public int k = startingHealth;
	GameObject[] healthBar;

	// Use this for initialization
	void Start () {
		healthBar = new GameObject[startingHealth];
		currentHealth = startingHealth;
		createHealth();

	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth < k){
			Debug.Log ("Health " + (k - 1));
			Destroy (GameObject.Find("Health " + (k - 1)));
			k = k - 1;
		}
	}

	void createHealth(){
		for(int i = 0; i < startingHealth; ++i){
			healthBar[i] = health;
			if(healthBar [i] != null) {
				healthBar[i] = (GameObject) Instantiate (healthBar [i], new Vector3 (firstHeart.x - (i * 1.12918f), firstHeart.y, firstHeart.z), Quaternion.identity, GameObject.Find ("Health Bar").transform);
			}
			healthBar[i].name = "Health " + i;
//			healthBar[i] = Resources.Load ("Health Bar") as GameObject;
//			Debug.Log (Resources.Load ("Health Bar") as GameObject);
//			Debug.Log (Resources.Load ("Health Bar"));
		}
	}
}

#endif