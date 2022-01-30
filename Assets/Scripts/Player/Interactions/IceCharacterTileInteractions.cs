using System;


public class IceCharacterTileInteractions : CharacterTileInteractionBase
{
    //IInteractWithTile Functions
    //====================================================================================================================//

    public override void EnterTile(Tile tile)
    {
        switch (tile.CurrentState)
        {
            case TILE_STATE.DEFAULT:
            case TILE_STATE.WATER:
                tile.ChangeState(TILE_STATE.ICE, trailMaterial);
                audioSrc.PlayOneShot(walkSound);
                break;
            case TILE_STATE.FIRE:
                //TODO Dead
                DestroyCharacter($"Stepping on {tile.gameObject.name} in {tile.CurrentState}", tile);
                break;
            case TILE_STATE.ICE:
                return;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void ExitTile(Tile tile)
    {
        
    }

    //====================================================================================================================//
    
}
