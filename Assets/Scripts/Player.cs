using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	//Variables
	private Rigidbody2D rb;
	public BoxCollider2D collide;
	public SpriteRenderer sprite;
	public float walkSpeed = 4f;
	public float accel = 1.5f;
	public bool grounded;
	public bool invincible = false;
	public LayerMask enemyLayer;
	public Health health;
	public GameObject player;
	public Sprite crouch;
	public Sprite stand;
	public RedGuy enemy;
	private float time = 0f;
	bool enableMovement = true;
	bool crouching = false;
	//public class healthBar{
//	public int health;
	//}

	//Initialization
	void Start () {
		player = GameObject.Find("Player");
		//Add and applies Rigidbody2D to our player
		player.AddComponent<Rigidbody2D>();
		rb = player.GetComponent<Rigidbody2D>();
		rb.gravityScale = 2f;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		//Add and applies BoxCollider2D to our player
		player.AddComponent<BoxCollider2D>();
		collide = player.GetComponent<BoxCollider2D> ();
		rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);

		//Gets the sprite component
		sprite = player.GetComponent<SpriteRenderer>();

		GameObject healthBars = GameObject.Find("Health Bar");
		health = healthBars.GetComponent<Health>();
//		StartCoroutine(invincibility());
	}
	// Update is called once per frame
	void Update (){
		movement();
		crouchAction();
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
				sprite.flipX = false;
			}
			if(Input.GetKey("left")){
				rb.velocity = new Vector2 (-walkSpeed * accel, rb.velocity.y);
				sprite.flipX = true;
			}
		}
	}

	void crouchAction(){
		if(Input.GetKeyDown("down") && grounded && !crouching){
			collide.size = new Vector2(collide.size.x, collide.size.y - 0.25f);
			collide.offset = new Vector2(collide.offset.x, collide.offset.y - 0.125f);
			sprite.sprite = crouch;
			crouching = true;
		}
		else if(Input.GetKeyDown("down") && grounded && crouching) {
			sprite.sprite = stand;
			collide.size = new Vector2 (collide.size.x, collide.size.y + 0.25f);
			collide.offset = new Vector2(collide.offset.x, collide.offset.y + 0.125f);
			crouching = false;
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
