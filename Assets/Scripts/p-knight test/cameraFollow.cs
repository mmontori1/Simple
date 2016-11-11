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

//		Debug.Log (cameraSpriteRender.bounds);
	}
	
	// Update is called once per frame
	void Update () {
		pKnightMovement = controller.pKnightMovement;
		if(pKnightMovement.getRight() > cFocusArea.getRight() && pKnightRB.velocity.x != 0){
			cFocusArea.setRight(cameraSpriteRender.bounds, pKnightMovement.getRight(), pKnightRB, rb);
		}
		else if(pKnightMovement.getLeft() < cFocusArea.getLeft() && pKnightRB.velocity.x != 0){
			cFocusArea.setLeft(cameraSpriteRender.bounds, pKnightMovement.getLeft(), pKnightRB, rb);
		}
		else{
			cFocusArea.resetHorizontalSpeed(cameraSpriteRender.bounds, rb, pKnightMovement.getLeft(), pKnightMovement.getRight());
		}
	}
}

public class focusArea{
	public GameObject focusAreaObject;
	public Bounds bound;
	bool onceRight;
	bool onceLeft;
	float top, bottom;
	float left, right;

	public focusArea(Bounds bounds, GameObject newObject){
		bound = bounds;
		right = bound.center.x + bound.extents.x;
		left = right - 2 * bound.extents.x;
		top = bound.center.y + bound.extents.y;
		bottom = top - 2 * bound.extents.y;
		focusAreaObject = newObject;
		onceLeft = true;
		onceRight = true;
	}

	public void resetHorizontalSpeed(Bounds bounds, Rigidbody2D newRB, float pLeft, float pRight){
		Debug.Log (pLeft);
		Debug.Log(pLeft + bound.extents.x);
		newRB.velocity = new Vector3(0, newRB.velocity.y, 0);
		bound = bounds;
		left = focusAreaObject.transform.position.x - bound.extents.x;
		right = focusAreaObject.transform.position.x + bound.extents.x;
		if(onceLeft){
			focusAreaObject.transform.position = new Vector3(pLeft + bound.extents.x, focusAreaObject.transform.position.y, focusAreaObject.transform.position.z);
			onceLeft = false;
		}
		if(onceRight){
			focusAreaObject.transform.position = new Vector3(pRight - bound.extents.x, focusAreaObject.transform.position.y, focusAreaObject.transform.position.z);
			onceRight = false;
		}
	}
		
	public void setRight(Bounds bounds, float newRight, Rigidbody2D oldRB, Rigidbody2D newRB){
		bound = bounds;
		left = focusAreaObject.transform.position.x - bound.extents.x;
		right = focusAreaObject.transform.position.x + bound.extents.x;
		newRB.velocity = new Vector3(oldRB.velocity.x, newRB.velocity.y, 0);
		if(!onceRight){
			onceRight = true;
		}
	}

	public void setLeft(Bounds bounds, float newLeft, Rigidbody2D oldRB, Rigidbody2D newRB){
		bound = bounds;
		left = focusAreaObject.transform.position.x - bound.extents.x;
		right = focusAreaObject.transform.position.x + bound.extents.x;
		newRB.velocity = new Vector3(oldRB.velocity.x, newRB.velocity.y, 0);
		if(!onceLeft){
			onceLeft = true;
		}
	}

	public void setTop(Bounds bounds, float newRight, Rigidbody2D oldRB, Rigidbody2D newRB){
		bounds = bound;
		top = focusAreaObject.transform.position.y + bound.extents.y;
		bottom = focusAreaObject.transform.position.y - bound.extents.y;
		newRB.velocity = new Vector3(newRB.velocity.x, oldRB.velocity.y, 0);
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