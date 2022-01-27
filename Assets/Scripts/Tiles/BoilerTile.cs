using System;
using UnityEngine;
using UnityEngine.Events;

public class BoilerTile : Tile
{
    [Serializable]
    public class BoilerEvent : UnityEvent<bool> { }
    
    [SerializeField]
    private bool isActive;
    
    [SerializeField]
    private BoilerEvent unityEvent;
    
    public override void ChangeState(TILE_STATE targetState, Material newMaterial)
    {
        switch (targetState)
        {
            case TILE_STATE.FIRE:
                TrySetIsActive(true);
                break;
            case TILE_STATE.DEFAULT:
            case TILE_STATE.ICE:
            case TILE_STATE.WATER:
                TrySetIsActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(targetState), targetState, null);
        }
    }

    private void TrySetIsActive(bool newState)
    {
        if (newState == isActive)
            return;

        //TODO We can set a material here as well when the boiler is active
        isActive = newState;
        unityEvent?.Invoke(isActive);
        Debug.Log($"Boiler {(isActive ? "is Active" : "is INACTIVE")}");
    }
}
