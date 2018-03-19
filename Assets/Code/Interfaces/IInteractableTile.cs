using UnityEngine;

namespace Assets.Code.Interfaces
{
    public interface IInteractable
    {

        void Interact(ToolBar.ToolType tool, Vector3Int pos);

    }
}
