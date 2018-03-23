using System.Collections;
using System.Collections.Generic;
using Assets.Code.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolBar : MonoBehaviour
{
    public enum ToolType { None, Shovel, Grass };

    private ToolType tool = ToolType.None;

    public Texture2D cursorTexture;
    public Texture2D shovelTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(string.Format("Co-ords of mouse is [X: {0} Y: {0}]", pos.x, pos.y));
			Vector3Int cellPos = TerrainGlossary.Instance.groundMap.WorldToCell(pos);
			IInteractable tile = (IInteractable)TerrainGlossary.Instance.groundMap.GetTile(cellPos);

            if (tile != null)
            {
                tile.Interact(tool, cellPos);
                Debug.Log(string.Format("Tile is: {0}", tile.GetType()));
            }
        }
    }

    void ClickShovel()
    {
        if (tool.Equals(ToolType.Shovel))
        {
            Debug.Log("nothin");
            tool = ToolType.None;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else
        {
            Debug.Log("shovel");
            tool = ToolType.Shovel;
            Cursor.SetCursor(shovelTexture, hotSpot, cursorMode);
        }
    }

    void ClickGrass()
    {
        if (tool.Equals(ToolType.Grass))
        {
            tool = ToolType.None;
        }
        else
        {
            tool = ToolType.Grass;
        }
    }

}
