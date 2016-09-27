using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	//Variables
	private Rigidbody2D rb;
	public BoxCollider2D collide;
	public float walkSpeed = 4f;
	public float accel = 1.5f;
	public bool grounded;
	public bool invincible = false;
	public LayerMask enemyLayer;
	public Health health;
	public GameObject player;
	public RedGuy enemy;
	private float time = 0f;
	bool enableMovement = true;
	//public class healthBar{
//	public int health;
	//}

	//Initialization
	void Start () {
		player = GameObject.Find("Player");
		//Add and applies Rigidbody2D to our yellow box player
		player.AddComponent<Rigidbody2D>();
		rb = player.GetComponent<Rigidbody2D>();
		rb.gravityScale = 2f;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		//Add and applies BoxCollider2D to our yellow box player
		player.AddComponent<BoxCollider2D>();
		player.GetComponent<BoxCollider2D> ();
		rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);

		GameObject healthBars = GameObject.Find("Health Bar");
		health = healthBars.GetComponent<Health>();
//		StartCoroutine(invincibility());
	}
	// Update is called once per frame
	void Update (){
		movement();
//		StartCoroutine(invincibility());
	}

	void OnCollisionEnter2D (Collision2D coll){
		if(coll.gameObject.tag == "floor"){
			grounded = true;
		}

		if(coll.gameObject.tag == "enemy" && !invincible){
			health.currentHealth = health.currentHealth - 1;
			if(health.currentHealth == 0){
				Destroy(GameObject.Find("Player"));
			}
//			if(!enemy.rightDirection){
//				rb.velocity = new Vector2 (-3, 3);
//				rb.AddForce (new Vector2 (-3, 3), ForceMode2D.Impulse);
//			}
//			else {
//				rb.velocity = new Vector2 (3, 3);
//				rb.AddForce (new Vector2 (3, 3), ForceMode2D.Impulse);
//			}
//			invincible = true;
		}
	}
		
	void OnCollisionExit2D (Collision2D coll){
		if(coll.gameObject.tag == "floor"){
			grounded = false;
		}
	}
	void movement(){
		if(enableMovement){
			if(Input.GetKeyDown("up") && grounded){
				rb.velocity = new Vector2 (rb.velocity.x, 10);
			}
			if(Input.GetKey("left shift") && grounded){
				accel = 1.5f;
			} 
			if(!Input.GetKey("left shift") && grounded){
				accel = 1f;
			}
			if(Input.GetKey("right")){
				rb.velocity = new Vector2 (walkSpeed * accel, rb.velocity.y);
			}
			if(Input.GetKey("left")){
				rb.velocity = new Vector2 (-walkSpeed * accel, rb.velocity.y);
			}
		}
	}
	bool rightFaced(){
		bool right = true;
		if(rb.velocity.x > 0){
			right = true;
		}
		else if(rb.velocity.x < 0){
			right = false;
		}
		return right;
	}

	IEnumerator invincibility(){
		if(invincible){
			yield return new WaitForSeconds(4);
			invincible = false;
		}
	}
}
