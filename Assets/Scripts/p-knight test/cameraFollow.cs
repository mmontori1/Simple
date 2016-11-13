using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	public Transform pKnightTransform;
	public float cameraDistance;

	public GameObject focusObject;
	public Rigidbody2D rb;
	public Rigidbody2D pKnightRB;
	public SpriteRenderer cameraSpriteRender;
	public pKnightController controller;
	public focusArea cFocusArea;
	public charMovement pKnightMovement;

	// Use this for initialization
	void Start () {
		controller = GameObject.Find("Purple Knight").GetComponent<pKnightController>();
		cameraSpriteRender = focusObject.GetComponent<SpriteRenderer>();
		cFocusArea = new focusArea (cameraSpriteRender.bounds, focusObject);
		rb = focusObject.GetComponent<Rigidbody2D>();
		pKnightRB = controller.GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Awake () {
		cameraDistance = 90f;
		GetComponent<UnityEngine.Camera>().orthographicSize = cameraDistance;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		pKnightMovement = controller.pKnightMovement;
		if(pKnightMovement.getRight() > cFocusArea.getRight()){
			cFocusArea.setRight(cameraSpriteRender.bounds, pKnightMovement.getRight());
			transform.position = new Vector3 (pKnightTransform.position.x, transform.position.y, transform.position.z);
		}
		if(pKnightMovement.getLeft() < cFocusArea.getLeft()){
			cFocusArea.setLeft(cameraSpriteRender.bounds, pKnightMovement.getLeft());
			transform.position = new Vector3 (pKnightTransform.position.x, transform.position.y, transform.position.z);
		}
		//if velocity is 0? also knight moving a bit strange?
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

	public void setRight(Bounds bounds, float newRight){
		bound = bounds;
		left = focusAreaObject.transform.position.x - bound.extents.x;
		right = focusAreaObject.transform.position.x + bound.extents.x;
		focusAreaObject.transform.Translate (new Vector3 (newRight - (focusAreaObject.transform.position.x + (focusAreaObject.transform.localScale.x / 2)), 0, 0));
	}

	public void setLeft(Bounds bounds, float newLeft){
		bound = bounds;
		left = focusAreaObject.transform.position.x - bound.extents.x;
		right = focusAreaObject.transform.position.x + bound.extents.x;
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
