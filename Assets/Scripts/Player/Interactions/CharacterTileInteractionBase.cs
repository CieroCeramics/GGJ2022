using System;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class CharacterTileInteractionBase : MonoBehaviour, IInteractWithTile
{
    public static Action<CharacterTileInteractionBase> OnCharacterDied;
    
    [SerializeField, Header("Trail Effects")]
    //Can the player move
    protected Material trailMaterial;

    //IInteractWithTile Functions
    //====================================================================================================================//

    public abstract void EnterTile(Tile tile);

    public abstract void ExitTile(Tile tile);

    protected void DestroyCharacter(in string cause, in Object killedObject = null)
    {
        Debug.Log($"{gameObject.name} was killed by {cause}", killedObject);
        OnCharacterDied?.Invoke(this);
        Destroy(gameObject);
        
    }

    //====================================================================================================================//

}
