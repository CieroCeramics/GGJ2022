using System;

public class FireCharacterTileInteractions : CharacterTileInteractionBase
{
    public override void EnterTile(Tile tile)
    {
        switch (tile.CurrentState)
        {
            case TILE_STATE.DEFAULT:
                tile.ChangeState(TILE_STATE.FIRE, trailMaterial);
                break;
            case TILE_STATE.FIRE:
                return;
            case TILE_STATE.WATER:
            case TILE_STATE.ICE:
                //TODO Dead
                DestroyCharacter($"Stepping on {tile.gameObject.name} in {tile.CurrentState}", tile);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void ExitTile(Tile tile)
    {
        
    }
}
