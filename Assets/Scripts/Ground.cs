using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		GameObject ground = GameObject.Find ("Ground");
		ground.AddComponent<Rigidbody2D>();
		ground.AddComponent<BoxCollider2D>();
		rb = ground.GetComponent<Rigidbody2D> ();
		ground.GetComponent<BoxCollider2D> ();
		rb.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
