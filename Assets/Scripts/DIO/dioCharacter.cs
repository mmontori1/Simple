using UnityEngine;
using System.Collections;

public class dioCharacter : MonoBehaviour {


	[SerializeField]
	private Sprite[] Sprites;
	[SerializeField]
	private PolygonCollider2D[] colliders;
	private int index = 0;
	private Rigidbody2D rb;
//	private Collider2D collide;
	private Animator animator;
	public SpriteRenderer sprite;
	public GameObject dio;
	public int walkSpeed;

	// Use this for initialization
	void Start () {
		dio = this.gameObject;
		dio.AddComponent<Rigidbody2D>();
		rb = GetComponent<Rigidbody2D>();
//		dio.AddComponent<PolygonCollider2D> ();
//		collide = GetComponent<PolygonCollider2D> ();
		animator = GetComponent<Animator>();
		sprite = dio.GetComponent<SpriteRenderer>();
//		sprite.sprite = Value[index];
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		walkSpeed = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("up")){
			rb.velocity = new Vector2 (rb.velocity.x, 4);
		}

		if(Input.GetKey("right")){
			rb.velocity = new Vector2 (walkSpeed, rb.velocity.y);
			animator.SetBool ("isWalking", true);
			sprite.flipX = false;
		}
		else if(Input.GetKey("left")){
			rb.velocity = new Vector2 (-walkSpeed, rb.velocity.y);
			animator.SetBool ("isWalking", true);
			sprite.flipX = true;
		}
		else{
			animator.SetBool ("isWalking", false);
		}
	}

	public void SetColliderForSprite(int spriteNum)
	{
		int direction = 0;
		if(sprite.flipX){
			++direction;
		}
		colliders[index].enabled = false;
		index = spriteNum + direction;
		colliders[index].enabled = true;
	}
}
