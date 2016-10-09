using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	private Rigidbody2D rb;
	public BoxCollider2D col;
	// Use this for initialization
	void Start () {
		GameObject water = GameObject.Find ("Water");
		water.AddComponent<Rigidbody2D>();
		water.AddComponent<BoxCollider2D>();
		rb = water.GetComponent<Rigidbody2D> ();
		col = water.GetComponent<BoxCollider2D> ();
		rb.isKinematic = true;
		col.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
