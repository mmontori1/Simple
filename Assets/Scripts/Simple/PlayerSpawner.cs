﻿using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public GameObject newPlayer;
	public Transform spawnerTransform;
	Vector3 spawnerLocation;
	GameObject players;
	public StatePatternPlayer statePattern;

	// Use this for initialization
	void Start () {
		spawnerTransform = this.transform;
		spawnerLocation = spawnerTransform.localPosition;
		players = GameObject.Find("Players"); 
		statePattern = newPlayer.GetComponent<StatePatternPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		createNewPlayer();
	}

	void createNewPlayer(){
		if(Input.GetKeyDown(KeyCode.R)){
			Instantiate(newPlayer, spawnerLocation, Quaternion.identity, players.transform);
//			statePattern = newPlayer.GetComponent<StatePatternPlayer>();
			statePattern.health = 1;
		}

	}
}
