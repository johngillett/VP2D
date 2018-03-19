using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface InteractableTile{

	void interact (ToolBar.ToolType tool, Vector3Int pos);

}
