using UnityEngine;
using System.Collections;

public class cameraSelectiveFollow : MonoBehaviour {

	private bool once;
	private float b;
	private Vector3 ab;
	public float cameraDistance;
	public float airHeight;
	public float cameraTop;
	public float cameraBottom;
	public pKnightController target;
	public Vector2 focusAreaSize;
	FocusArea focusArea;

	void Awake () {
		once = false;
		cameraDistance = 90f;
		GetComponent<UnityEngine.Camera>().orthographicSize = cameraDistance;
		cameraTop = transform.position.y + cameraDistance - 23f;
		cameraBottom = transform.position.y - cameraDistance;
		Debug.Log (cameraTop);
	}

	void Start () {
		focusArea = new FocusArea(target.spriteRender.bounds, focusAreaSize);
		airHeight = 0 - focusArea.center.y;
	}

	void LateUpdate () {
		focusArea.Update (target.spriteRender.bounds);
//		transform.position = new Vector3 (focusArea.center.x, focusArea.center.y + airHeight, transform.position.z);
		if (focusArea.center.y < cameraTop) {
			transform.position = new Vector3 (focusArea.center.x, transform.position.y, transform.position.z);
		}
		else if(focusArea.center.y < cameraBottom){
			cameraTop -= (2*cameraDistance);
			cameraBottom -= (2 * cameraDistance);
			Debug.Log (2 * cameraDistance);
			Debug.Log (transform.position.y);
			b = transform.position.y - 2*cameraDistance + airHeight;
			Debug.Log (b);
			ab = new Vector3 (transform.position.x, b, transform.position.z);
			once = true;
		}
//		else{
//			cameraTop += (2*cameraDistance);
//			cameraBottom += (2 * cameraDistance);
//			Debug.Log (2 * cameraDistance);
//			Debug.Log (transform.position.y);
//			b = transform.position.y + 2*cameraDistance - airHeight;
//			Debug.Log (b);
//			ab = new Vector3 (transform.position.x, b, transform.position.z);
//			once = true;
////			float a = transform.position.y;
////			Debug.Log (b);
////			transform.position = Vector3.Lerp (transform.position, ab, 0.01f);
////			Mathf.Lerp (a, b, 0.5f);
//			Debug.Log (cameraBottom);
//		}
		if (once) {
			StartCoroutine (transition (1000f));
		}
//		Debug.Log (target.spriteRender.bounds);

//		if(focusArea.center.y > 60){
//		if (focusArea.center.y > cameraBottom) {
//			transform.position = new Vector3 (focusArea.center.x, focusArea.center.y + airHeight, transform.position.z);
//		}
//			Debug.Log (target.spriteRender.bounds);
//		} 
//		else{
//			transform.position = new Vector3 (focusArea.center.x, transform.position.y, transform.position.z);
//		}
	}

	void OnDrawGizmos(){
		Gizmos.color = new Color (1, 0, 2, .5f);
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

	private IEnumerator transition(float time){
		float i = 0.0f;
		float rate = 1.0f/time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			transform.position = Vector3.Lerp (transform.position, ab, i);
			yield return new WaitForSeconds(i);
		}
	}
}
