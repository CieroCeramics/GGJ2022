using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverStartTile : Tile
{
    private const float FLOW_UPDATE_TIME = 0.5f;
    
    public enum DIRECTION
    {
        NONE,
        LEFT,
        RIGHT,
        FORWARD,
        BACKWARD
    }

    [SerializeField]
    private DIRECTION flowDirection;

    public override TILE_STATE CurrentState => TILE_STATE.WATER;
    private float _flowUpdateTimer;
    
    private void Update()
    {
        if (_flowUpdateTimer < FLOW_UPDATE_TIME)
        {
            _flowUpdateTimer += Time.deltaTime;
            return;
        }
        
        CalculateFlow();
    }
    

    public override void ChangeState(TILE_STATE targetState, Material newMaterial)
    {
        
    }



    private void CalculateFlow()
    {
        //TODO From this position, flow through all tiles in DIRECTION until interrupted
        throw new NotImplementedException();
    }
}

public static class DIRECTIONExtensions
{
    public static Vector2Int DirectionAsDirectionVector(this RiverStartTile.DIRECTION direction)
    {
        switch (direction)
        {
            case RiverStartTile.DIRECTION.LEFT:
                return Vector2Int.left;
            case RiverStartTile.DIRECTION.RIGHT:
                return Vector2Int.right;
            case RiverStartTile.DIRECTION.FORWARD:
                return Vector2Int.up;
            case RiverStartTile.DIRECTION.BACKWARD:
                return Vector2Int.down;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
}
