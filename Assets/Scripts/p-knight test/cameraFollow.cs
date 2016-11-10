using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	public GameObject focusObject;
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
	}
	
	// Update is called once per frame
	void Update () {
		pKnightMovement = controller.pKnightMovement;
		if(pKnightMovement.getRight() > cFocusArea.getRight()){
			cFocusArea.setRight(cameraSpriteRender.bounds, pKnightMovement.getRight());
//			Debug.Log (cameraSpriteRender.bounds);
		}
		if(pKnightMovement.getLeft() < cFocusArea.getLeft()){
			cFocusArea.setLeft(cameraSpriteRender.bounds, pKnightMovement.getLeft());
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

//	public void setEdges(Bounds bounds){
//		bound = bounds;
//		right = bound.center.x + bound.extents.x;
//		left = right - 2 * bound.extents.x;
//		top = bound.center.y + bound.extents.y;
//		bottom = top - 2 * bound.extents.y;
//	}

	public void setRight(Bounds bounds, float newRight){
//		bound = bounds;
		Debug.Log("newRight" + newRight);
		right = focusAreaObject.transform.position.x + bound.extents.x;
		Debug.Log ("right" + right);
		focusAreaObject.transform.Translate (new Vector3 (newRight - (focusAreaObject.transform.position.x + (focusAreaObject.transform.localScale.x / 2)), 0, 0));
	}

	public void setLeft(Bounds bounds, float newLeft){
//		bound = bounds;
		left = focusAreaObject.transform.position.x - bound.extents.x;
		focusAreaObject.transform.Translate (new Vector3 (newLeft - (focusAreaObject.transform.position.x - (focusAreaObject.transform.localScale.x / 2)), 0, 0));
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