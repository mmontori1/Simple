using UnityEngine;
using System.Collections;

public class pKnightController : MonoBehaviour {

	private GameObject pKnight;
	private Rigidbody2D rb;
	public SpriteRenderer spriteRender;
	charMovement pKnightMovement;

	// Use this for initialization
	void Start () {
		pKnight = this.gameObject;
		rb = pKnight.GetComponent<Rigidbody2D>();
		rb.gravityScale = 40f;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;

		spriteRender = pKnight.GetComponent<SpriteRenderer>();

		pKnightMovement = new charMovement(70, 150, spriteRender.bounds);

		Debug.Log (spriteRender.bounds);
	}
	
	// Update is called once per frame
	void Update () {
		movement ();
//		if(rb.velocity == pKnightMovement.stop){
//			Debug.Log (spriteRender.bounds);
//		}
	}

	void movement(){
		if(Input.GetKeyDown("up")){
			rb.velocity = new Vector2 (rb.velocity.x, pKnightMovement.jumpHeight);
		}
		if(Input.GetKey("right")){
			rb.velocity = new Vector2 (pKnightMovement.walkSpeed, rb.velocity.y);
			spriteRender.flipX = false;
		}
		else if(Input.GetKeyUp("right")){
			rb.velocity = new Vector2 (0, rb.velocity.y);
			spriteRender.flipX = false;
		}
		if(Input.GetKey("left")){
			rb.velocity = new Vector2 (-pKnightMovement.walkSpeed, rb.velocity.y);
			spriteRender.flipX = true;
		}
		else if(Input.GetKeyUp("left")){
			rb.velocity = new Vector2 (0, rb.velocity.y);
			spriteRender.flipX = true;
		}
	}
		
	struct charMovement{
		public int walkSpeed;
		public int jumpHeight;
		public Bounds bound;
		public Vector2 stop;

		public charMovement(int speed, int jump, Bounds bounds){
			walkSpeed = speed;
			jumpHeight = jump;
			bound = bounds;
			stop = new Vector2 (0, 0);
		}
	}
}
