using UnityEngine;
using System.Collections;

public class cameraSelectiveFollow : MonoBehaviour {

	public float cameraDistance;
	public float airHeight;
	public pKnightController target;
	public Vector2 focusAreaSize;
	FocusArea focusArea;

	void Awake () {
		cameraDistance = 90f;
		GetComponent<UnityEngine.Camera>().orthographicSize = cameraDistance;
	}

	void Start () {
		focusArea = new FocusArea(target.spriteRender.bounds, focusAreaSize);
		airHeight = 0 - focusArea.center.y;
	}

	void LateUpdate () {
		focusArea.Update (target.spriteRender.bounds);
		transform.position = new Vector3 (focusArea.center.x, focusArea.center.y + airHeight, transform.position.z);
	}

	void OnDrawGizmos(){
		Gizmos.color = new Color (1, 0, 0, .5f);
		Gizmos.DrawCube (focusArea.center, focusAreaSize);
	}

	struct FocusArea{
		public Vector2 center;
		public Vector2 velocity;
		float left, right;
		float top, bottom;

		public FocusArea(Bounds targetBounds, Vector2 size){
			left = targetBounds.center.x - size.x/2;
			right = targetBounds.center.x + size.x/2;
			top = targetBounds.min.y + size.y;
			bottom = targetBounds.min.y;

			velocity = Vector2.zero;
			center = new Vector2((left + right) / 2, (top + bottom) / 2); 
		}

		public void Update(Bounds targetBounds){
			float shiftX = 0;
			if(targetBounds.min.x < left) {
				shiftX = targetBounds.min.x - left;
			}
			else if(targetBounds.max.x > right){
				shiftX = targetBounds.max.x - right;
			}
			left += shiftX;
			right += shiftX;
			float shiftY = 0;
			if(targetBounds.min.y < bottom) {
				shiftY = targetBounds.min.y - bottom;
			}
			else if(targetBounds.max.y > top){
				shiftY = targetBounds.max.y - top;
			}
			top += shiftY;
			bottom += shiftY;
			center = new Vector2((left + right) / 2, (top + bottom) / 2); 
			velocity = new Vector2 (shiftX, shiftY);
		}
	}
}
