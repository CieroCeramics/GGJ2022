using UnityEngine;

public abstract class CharacterTileInteractionBase : MonoBehaviour, IInteractWithTile
{
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
        Destroy(gameObject);
    }

    //====================================================================================================================//

}
