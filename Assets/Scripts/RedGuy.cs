using UnityEngine;
using System.Collections;

public class RedGuy : MonoBehaviour {
	public GameObject enemy;
	private Rigidbody2D rb;
	public BoxCollider2D collide;
	public Collider2D coll;
	public float walkSpeed = 2f;
	public Vector2 speed;
	public Vector2 direction = new Vector2 (1,-1);
	public int facing = -1;
	public LayerMask groundLayer;
	public LayerMask playerLayer;
	public bool rightDirection;
	public bool isAbove;
	public float enemyHeight;
	// Use this for initialization
	void Start () {
		enemy = this.gameObject;
		enemy.AddComponent<Rigidbody2D>();
		rb = enemy.GetComponent<Rigidbody2D>();
		enemy.AddComponent<BoxCollider2D>();
		collide = enemy.GetComponent<BoxCollider2D>();

		speed = new Vector2(walkSpeed,rb.velocity.y);
		rb.gravityScale = 2f;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		rb.velocity = new Vector2(walkSpeed,rb.velocity.y);

		coll = GetComponent<Collider2D>();
		PhysicsMaterial2D material = new PhysicsMaterial2D();
		material.friction = 0;
		material.bounciness = 0;
		coll.sharedMaterial = material;

		enemyHeight = collide.size.y;
	}

	// Update is called once per frame
	void Update(){
		speed = new Vector2(walkSpeed,rb.velocity.y);
		rb.velocity = speed;
		if(rightFaced()){
			direction = new Vector2(1, direction.y);
		}
		else if(!rightFaced()){
			direction = new Vector2(-1, direction.y);
		}
		if(!edgeCheck() && fallCheck()){
			walkSpeed = walkSpeed * facing;
			direction = new Vector2(direction.x * facing, direction.y);
		}
		if(edgeCheck() && !fallCheck()){
			walkSpeed = walkSpeed * facing;
			direction = new Vector2(direction.x * facing, direction.y);
		}
		aboveCheck();
	}

	void OnCollisionEnter2D (Collision2D collision){
		if (collision.gameObject.tag == "player") {
			isAbove = aboveCheck();
			Debug.Log (isAbove);
		}
	}

	void OnCollisionExit2D (Collision2D collision){
		if (collision.gameObject.tag == "player") {
			isAbove = aboveCheck();
		}
	}

	bool edgeCheck() {
		Vector2 position = transform.position;
		float distance = 1.0f;
		Debug.DrawRay(position, direction, Color.green);
		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
		if (hit.collider != null) {
			return true;
		}

		return false;
	}

	bool fallCheck(){
		Vector2 position = transform.position;
		float distance = 0.7f;
		Debug.DrawRay(position, new Vector2(0,-0.7f), Color.green);
		RaycastHit2D hit = Physics2D.Raycast(position, new Vector2(0,-0.7f), distance, groundLayer);
		if (hit.collider != null) {
			return true;
		}

		return false;
	}

	bool aboveCheck(){
		Vector2 position = transform.position;
		float distanceAbove = 0.7f;
		float distanceSides = 0.8321f;
		Debug.DrawRay(position, new Vector2(0,0.7f), Color.green);
		RaycastHit2D hitAbove = Physics2D.Raycast(position, new Vector2(0,0.7f), distanceAbove, playerLayer);
		Debug.DrawRay(position, new Vector2(-0.45f,0.7f), Color.green);
		RaycastHit2D hitLeft = Physics2D.Raycast(position, new Vector2(-0.45f,0.7f), distanceSides, playerLayer);
		Debug.DrawRay(position, new Vector2(0.45f,0.7f), Color.green);
		RaycastHit2D hitRight = Physics2D.Raycast(position, new Vector2(0.45f,0.7f), distanceSides, playerLayer);
		if (hitAbove.collider != null || hitLeft.collider != null || hitRight.collider != null) {
			return true;
		}

		return false;
	}

	bool rightFaced(){
		if(walkSpeed > 0){
			rightDirection = true;
			return rightDirection;
		} 
		else{
			rightDirection = false;
			return false;
		}
	}
}
