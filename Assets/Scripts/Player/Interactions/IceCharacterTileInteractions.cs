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
                tile.ChangeState(TILE_STATE.ICE, trailMaterial);
                break;
            case TILE_STATE.FIRE:
                //TODO Dead
                break;
            case TILE_STATE.WATER:
                //TODO Freeze Water
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
