using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ViewController : MonoBehaviour {
	private float dist;
	private Vector3 MouseStart, MouseMove;
	private Vector3 derp;

	[SerializeField] private int scrollSpeed;
	[SerializeField] private int baseDragSpeed;

	[SerializeField] private int minScreenSize;
	[SerializeField] private int maxScreenSize;

	[SerializeField] private int mapLeftBounds;
	[SerializeField] private int mapRightBounds;
	[SerializeField] private int mapUpperBounds;
	[SerializeField] private int mapLowerBounds;


	void Start () {
		dist = transform.position.z; //Distance camera is above map
	}

	void Update () {

		var screenSize = Camera.main.orthographicSize + Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * -1;

		if (screenSize < minScreenSize) {
			screenSize = minScreenSize;
		} else if (screenSize > maxScreenSize) { 
			screenSize = maxScreenSize;
		}

		Camera.main.orthographicSize = screenSize;

		float dragSpeed = Mathf.Pow(baseDragSpeed + (screenSize / maxScreenSize), 2);

		//Debug.Log (dragSpeed);

		if (Input.GetMouseButtonDown ((int)MouseButton.MiddleMouse)) {
			MouseStart = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, dist);
		} else if (Input.GetMouseButton ((int)MouseButton.MiddleMouse)) {
			MouseMove = new Vector3 (Input.mousePosition.x - MouseStart.x, Input.mousePosition.y - MouseStart.y, dist);
			MouseStart = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, dist);

			float newPosX = transform.position.x + MouseMove.x * Time.deltaTime * -1 * dragSpeed;
			float newPosY = transform.position.y + MouseMove.y * Time.deltaTime * -1 * dragSpeed;

			if (newPosX < mapLeftBounds) {
				newPosX = mapLeftBounds;
			} else if (newPosX > mapRightBounds) {
				newPosX = mapRightBounds;
			}

			if (newPosY < mapLowerBounds) {
				newPosY = mapLowerBounds;
			} else if (newPosY > mapUpperBounds) {
				newPosY = mapUpperBounds;
			}

			Vector3 newPosition = new Vector3 (newPosX, newPosY, dist);

			transform.position = newPosition;
		}

	}

}