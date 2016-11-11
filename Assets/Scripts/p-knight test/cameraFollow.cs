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
		if(pKnightMovement.getRight() > cFocusArea.getRight()){
			cFocusArea.setRight(cameraSpriteRender.bounds, pKnightMovement.getRight(), pKnightRB, rb);
		}
		else if(pKnightMovement.getLeft() < cFocusArea.getLeft()){
			cFocusArea.setLeft(cameraSpriteRender.bounds, pKnightMovement.getLeft(), pKnightRB, rb);
		}
		else{
			cFocusArea.resetHorizontalSpeed(cameraSpriteRender.bounds, rb);
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
		left = focusAreaObject.transform.position.x - bound.extents.x;
		right = focusAreaObject.transform.position.x + bound.extents.x;
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

	public float getLeft(){
		return left;
	}

	public float getTop(){
		return top;
	}

	public float getBottom(){
		return bottom;
	}
}