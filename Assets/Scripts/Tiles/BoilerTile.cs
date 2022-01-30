using System;
using UnityEngine;
using UnityEngine.Events;

public class BoilerTile : Tile, ICharacterExit
{
    [Serializable]
    public class BoilerEvent : UnityEvent<bool> { }

    public override bool InterruptsRiver => !isActive;

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
                TrySetIsActive(false);
                break;
            case TILE_STATE.ICE:
                case TILE_STATE.WATER:
                return;
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
        Debug.Log($"{gameObject.name}<{nameof(BoilerTile)}> {(isActive ? "is Active" : "is INACTIVE")}", gameObject);
    }


    public void CharacterExited()
    {
        ChangeState(TILE_STATE.DEFAULT, null);
    }
}
