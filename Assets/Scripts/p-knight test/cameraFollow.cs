using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	public GameObject focusObject;
	public Rigidbody2D rb;
	public Rigidbody2D pKnightRB;
	public SpriteRenderer cameraSpriteRender;
	public pKnightController controller;
	public focusArea cFocusArea;
	public charMovement pKnightMovement;

	// Use this for initialization
	void Start () {
		focusObject = this.gameObject;
		controller = GameObject.Find("Purple Knight").GetComponent<pKnightController>();
		cameraSpriteRender = focusObject.GetComponent<SpriteRenderer>();
		cFocusArea = new focusArea (cameraSpriteRender.bounds, focusObject);
		rb = focusObject.GetComponent<Rigidbody2D>();
		pKnightRB = controller.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		pKnightMovement = controller.pKnightMovement;
		if(pKnightMovement.getRight() > cFocusArea.getRight() && pKnightRB.velocity.x != 0){
			cFocusArea.setRight(cameraSpriteRender.bounds, pKnightMovement.getRight(), pKnightRB, rb);
			Debug.Log ("a");
		}
		else if(pKnightMovement.getLeft() < cFocusArea.getLeft() && pKnightRB.velocity.x != 0){
			cFocusArea.setLeft(cameraSpriteRender.bounds, pKnightMovement.getLeft(), pKnightRB, rb);
			Debug.Log ("b");
		}
		else{
			cFocusArea.resetHorizontalSpeed(cameraSpriteRender.bounds, rb);
			Debug.Log ("c");
		}
		if(pKnightMovement.getRight() > cFocusArea.getRight() && pKnightRB.velocity.x == 0){
			cFocusArea.setRight(pKnightMovement.getRight());
			Debug.Log ("d");
		}
		if(pKnightMovement.getLeft() < cFocusArea.getLeft() && pKnightRB.velocity.x == 0){
			Debug.Log (pKnightMovement.getLeft());
			Debug.Log (cFocusArea.getLeft ());
			if(pKnightMovement.getLeft() < cFocusArea.getLeft()){
				rb.velocity = new Vector3(-70, rb.velocity.y, 0);
				cFocusArea.setLeft(cameraSpriteRender.bounds.center.x - cameraSpriteRender.bounds.extents.x);
			}
			else{
				rb.velocity = new Vector3(0, rb.velocity.y, 0);
			}
//			cFocusArea.setLeft(pKnightMovement.getLeft());
		}
	}
}

public class focusArea{
	public GameObject focusAreaObject;
	public Bounds bound;
	float top, bottom;
	float left, right;

	public focusArea(Bounds bounds, GameObject newObject){
		bound = bounds;
		right = bound.center.x + bound.extents.x;
		left = right - 2 * bound.extents.x;
		top = bound.center.y + bound.extents.y;
		bottom = top - 2 * bound.extents.y;
		focusAreaObject = newObject;
	}

	public void resetHorizontalSpeed(Bounds bounds, Rigidbody2D newRB){
//		left = focusAreaObject.transform.position.x - bound.extents.x;
//		right = focusAreaObject.transform.position.x + bound.extents.x;
		newRB.velocity = new Vector3(0, newRB.velocity.y, 0);
	}
		
	public void setRight(Bounds bounds, float newRight, Rigidbody2D oldRB, Rigidbody2D newRB){
		bounds = bound;
		left = focusAreaObject.transform.position.x - bound.extents.x;
		right = focusAreaObject.transform.position.x + bound.extents.x;
		newRB.velocity = new Vector3(oldRB.velocity.x, newRB.velocity.y, 0);
	}

	public void setLeft(Bounds bounds, float newLeft, Rigidbody2D oldRB, Rigidbody2D newRB){
		bounds = bound;
		left = focusAreaObject.transform.position.x - bound.extents.x;
		right = focusAreaObject.transform.position.x + bound.extents.x;
		newRB.velocity = new Vector3(oldRB.velocity.x, newRB.velocity.y, 0);
	}

	public float getRight(){
		return right;
	}

	public void setRight(float newRight){
		right = newRight;
	}

	public float getLeft(){
		return left;
	}

	public void setLeft(float newLeft){
		left = newLeft;
	}

	public float getTop(){
		return top;
	}

	public float getBottom(){
		return bottom;
	}
}