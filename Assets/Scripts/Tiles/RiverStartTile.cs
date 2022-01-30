using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities.Extensions;

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
//====================================================================================================================//
    [SerializeField]
    private DIRECTION flowDirection;

    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Material waterMaterial;

    private Tile[] _flowTiles;

    public override TILE_STATE CurrentState => TILE_STATE.WATER;
    private float _flowUpdateTimer;
    
    //Unity Functions
    //====================================================================================================================//
    
    private void Update()
    {
        if (_flowUpdateTimer < FLOW_UPDATE_TIME)
        {
            _flowUpdateTimer += Time.deltaTime;
            return;
        }
        
        CalculateFlow();
    }
    
    //====================================================================================================================//
    
    protected override void Setup()
    {
        Assert.IsNotNull(defaultMaterial);
        Assert.IsNotNull(waterMaterial);
        
        base.Setup();

        var puzzleLevelManager = FindObjectOfType<PuzzleLevelManager>();
        _flowTiles = puzzleLevelManager.PuzzleTiles.GetAllTilesInDirectionFrom(coordinate, flowDirection).Values.ToArray();
    }

    public override void ChangeState(TILE_STATE targetState, Material newMaterial)
    {
        
    }
    
    private void CalculateFlow()
    {
        bool isInterrupted = false;

        foreach (var flowTile in _flowTiles)
        {
            if (flowTile.InterruptsRiver || flowTile.CurrentState == TILE_STATE.ICE)
            {
                isInterrupted = true;
                continue;
            }

#if UNITY_EDITOR
            DrawDebugArrow(flowTile.transform.position, isInterrupted ? Color.red : Color.green);
#endif
            
            if(flowTile.CurrentState == TILE_STATE.WATER && isInterrupted)
                flowTile.ChangeState(TILE_STATE.DEFAULT, defaultMaterial);
            else if(flowTile.CurrentState != TILE_STATE.WATER && isInterrupted == false)
                flowTile.ChangeState(TILE_STATE.WATER, waterMaterial);
        }
    }
    
    //Editor Functions
    //====================================================================================================================//
    
#if UNITY_EDITOR

    private void DrawDebugArrow(in Vector3 position, in Color color)
    {
        var pos = position + new Vector3(0.5f, 0.05f,-0.5f);
        var direction = flowDirection.DirectionAsDirectionVector3();
        DrawArrow.ForDebug(pos + -direction/2f, direction * 0.5f, color);
    }

    private void OnDrawGizmos()
    {
        var pos = transform.position + new Vector3(0.5f, 0.05f,-0.5f);
        var direction = flowDirection.DirectionAsDirectionVector3();
        Gizmos.color = Color.red;
        DrawArrow.ForGizmo(pos + -direction/2f, direction * 0.5f);
    }

#endif
}


