using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTile : Tile, ICharacterExit
{
    public override TILE_STATE CurrentState => _isBridgeAlive ? _bridgeState : currentState;

    [SerializeField]
    private Renderer bridgeRenderer;

    [SerializeField]
    private TILE_STATE startingBridgeState;
    private TILE_STATE _bridgeState;

    private bool _isBridgeAlive = true;
    private bool _onFire;

    protected override void Setup()
    {
        base.Setup();

        _bridgeState = startingBridgeState;
    }

    public override void ChangeState(TILE_STATE targetState, Material newMaterial)
    {
        if (_isBridgeAlive == false)
        {
            base.ChangeState(targetState, newMaterial);
            return;
        }

        switch (targetState)
        {
            case TILE_STATE.DEFAULT:
                break;
            case TILE_STATE.FIRE:
                bridgeRenderer.material = newMaterial;
                _onFire = true;
                break;
            case TILE_STATE.ICE:
                bridgeRenderer.material = newMaterial;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(targetState), targetState, null);
        }

        _bridgeState = targetState;
    }

    public void CharacterExited()
    {
        if (_onFire == false)
            return;

        bridgeRenderer.enabled = false;
        _isBridgeAlive = false;
        _bridgeState = TILE_STATE.NONE;
    }
}
