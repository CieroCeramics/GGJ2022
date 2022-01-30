using System;
using UnityEngine;
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
    private Rect _movementRect;

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

        _movementRect = FindObjectOfType<PuzzleLevelManager>().MovementBounds;
    }

    // Update is called once per frame
    private void Update()
    {
        _playerInput.ProcessMovement();

        ProcessTileCollisions();
    }

    private void FixedUpdate()
    {
        var currentPosition = rigidBody.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, _movementRect.min.x, _movementRect.max.x);
        currentPosition.z = Mathf.Clamp(currentPosition.z, _movementRect.min.y, _movementRect.max.y);

        if (currentPosition.y < -5)
        {
            if(_playerInteractWithTiles is CharacterTileInteractionBase characterTileInteractionBase)
                characterTileInteractionBase.ForceKill();
            return;
        }

        rigidBody.position = currentPosition;
    }

    //====================================================================================================================//



    private void ProcessTileCollisions()
    {
        ProcessHitObject(Physics.Raycast(transform.position, Vector3.down * 1.5f, out var hit, raycastLayer.value)
            ? hit.collider.gameObject
            : null);
    }

    private void ProcessHitObject(in GameObject hitGameObject)
    {
        //If we're standing on the same Object ignore
        if (hitGameObject == _currentTileObject)
            return;

        if (_currentTile != null)
        {
            _playerInteractWithTiles.ExitTile(_currentTile);
                
            if(_currentTile is ICharacterExit characterExit)
                characterExit.CharacterExited();
        }

        //Update Current Standing Tile
        //--------------------------------------------------------------------------------------------------------//
            
        _currentTileObject = hitGameObject;
        if (_currentTileObject == null)
        {
            _currentTile = null;
            return;
        }
        _currentTile = hitGameObject.GetComponent<Tile>();
            
        _playerInteractWithTiles.EnterTile(_currentTile);
            
        if(_currentTile is ICharacterEnter characterEnter)
            characterEnter.CharacterEntered();


        //--------------------------------------------------------------------------------------------------------//
    }
}
