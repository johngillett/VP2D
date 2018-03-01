using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolBar : MonoBehaviour {


	public enum ToolType {None, Shovel, Grass};

	private ToolType tool = ToolType.None;

	public Texture2D cursorTexture;
	public Texture2D shovelTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	[SerializeField]
	public Tilemap groundTiles; 

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Debug.Log(string.Format("Co-ords of mouse is [X: {0} Y: {0}]", pos.x, pos.y));
			InteractableTile tile = (InteractableTile) groundTiles.GetTile(groundTiles.WorldToCell(pos));

			if (tile != null)
			{	
				tile.interact (tool);
				Debug.Log(string.Format("Tile is: {0}", tile.GetType()));
			}
		}
	}

	void ClickShovel() {
		if (tool.Equals (ToolType.Shovel)) {
			Debug.Log ("nothin");
			tool = ToolType.None;
			Cursor.SetCursor (cursorTexture, hotSpot, cursorMode);
		} else {
			Debug.Log ("shovel");
			tool = ToolType.Shovel;
			Cursor.SetCursor (shovelTexture, hotSpot, cursorMode);
		}
	}

	void ClickGrass() {
		if (tool.Equals (ToolType.Grass)) {
			tool = ToolType.None;
		} else {
			tool = ToolType.Grass;
		}
	}

}
