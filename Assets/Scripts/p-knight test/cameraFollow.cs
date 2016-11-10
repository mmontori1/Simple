using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	public pKnightController controller;
	public GameObject pKnight;
	public GameObject cameraFollower;
	public SpriteRenderer focusAreaRender;
	public Vector3 newLoc;
	public Bounds cameraBounds;
	public focusArea cameraFocusArea;

	// Use this for initialization
	void Start () {
		pKnight = GameObject.Find("Purple Knight");
		cameraFollower = this.gameObject;
		controller = pKnight.GetComponent<pKnightController>();
		focusAreaRender = cameraFollower.GetComponent<SpriteRenderer> ();
		newLoc = new Vector3 (pKnight.transform.position.x, pKnight.transform.position.y, 1);
		cameraFollower.transform.position = newLoc;
		cameraBounds = focusAreaRender.bounds;
		cameraFocusArea = new focusArea(cameraBounds);

		Debug.Log(controller.pKnightMovement.left);
	}
	
	// Update is called once per frame
	void Update () {
//		if(focusArea.left < ){
//		}
	}

	public class focusArea{
		public Bounds focusAreaBounds;
		float left, right;
		float top, bottom;

		public focusArea(Bounds bound){
			focusAreaBounds = bound;
			right = bound.extents.x;
			left = -1 * right;
			top = bound.extents.y;
			bottom = -1 * top;
		}
	}
}
