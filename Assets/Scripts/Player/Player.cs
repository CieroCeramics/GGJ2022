using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Properties
    //====================================================================================================================//

    [SerializeField] private LayerMask raycastLayer;

    //Cached components
    private IMove _playerInput;
    private IInteractWithTile _playerInteractWithTiles;

    private Rigidbody rigidBody;
    private new Transform transform;
    private Animator animator;


    private GameObject _currentTileObject;
    private Tile _currentTile;

    //Unity Functions
    //====================================================================================================================//

    // Use this for initialization
    private void Start()
    {
        //Cache the attached components for better performance and less typing
        rigidBody = GetComponent<Rigidbody>();
        transform = gameObject.transform;
        animator = gameObject.GetComponent<Animator>();

        _playerInput = GetComponent<IMove>();
        _playerInteractWithTiles = GetComponent<IInteractWithTile>();
    }

    // Update is called once per frame
    private void Update()
    {
        _playerInput.ProcessMovement();

        ProcessTileCollisions();
    }

    //====================================================================================================================//



    private void ProcessTileCollisions()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, raycastLayer.value))
        {
            var collidingObject = hit.collider.gameObject;
            
            //If we're standing on the same Object ignore
            if (collidingObject == _currentTileObject)
                return;
            
            if(_currentTile != null)
                _playerInteractWithTiles.ExitTile(_currentTile);

            //Update Current Standing Tile
            //--------------------------------------------------------------------------------------------------------//
            
            _currentTileObject = collidingObject;
            _currentTile = collidingObject.GetComponent<Tile>();
            
            _playerInteractWithTiles.EnterTile(_currentTile);

            //--------------------------------------------------------------------------------------------------------//
        }
    }
}
