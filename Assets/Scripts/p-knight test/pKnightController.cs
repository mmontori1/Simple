using UnityEngine;
using System.Collections;

public class pKnightController : MonoBehaviour {

	private GameObject pKnight;
	private Rigidbody2D rb;
	public int walkSpeed;
	public int jumpHeight;


	// Use this for initialization
	void Start () {
		pKnight = this.gameObject;
		rb = pKnight.GetComponent<Rigidbody2D>();
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		walkSpeed = 50;
		jumpHeight = 150;
	}
	
	// Update is called once per frame
	void Update () {
		movement ();
	}

	void movement(){
		if(Input.GetKeyDown("up")){
			rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
		}
		if(Input.GetKey("right")){
			rb.velocity = new Vector2 (walkSpeed, rb.velocity.y);
		}
		if(Input.GetKey("left")){
			rb.velocity = new Vector2 (-walkSpeed, rb.velocity.y);
		}
	}
}
