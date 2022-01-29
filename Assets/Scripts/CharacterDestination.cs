using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(BoxCollider))]
public class CharacterDestination : MonoBehaviour
{
    private enum CHARACTER_TYPE
    {
        NONE,
        FIRE,
        ICE,
    }

    //Properties
    //====================================================================================================================//
    private const string ICE_CHARACTER_TAG = "BlueGuy";
    private const string FIRE_CHARACTER_TAG = "RedGuy";
    
    public static Action CharacterEnterStateChanged;

    public bool IsActive { get; private set; }

    [SerializeField]
    private CHARACTER_TYPE expectedCharacter;

    private BoxCollider BoxCollider
    {
        get
        {
            if (_boxCollider == null)
                _boxCollider = GetComponent<BoxCollider>();

            return _boxCollider;
        }
    }
    private BoxCollider _boxCollider;
    
    private GameObject _activeObject;

    //Unity Functions
    //====================================================================================================================//
    
    // Start is called before the first frame update
    private void Start()
    {
        Assert.AreNotEqual(CHARACTER_TYPE.NONE, expectedCharacter);
        Assert.IsTrue(BoxCollider.isTrigger);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_activeObject != null && _activeObject == other.gameObject)
            return;
        
        CharacterTileInteractionBase characterTileInteractionBase;
        switch (expectedCharacter)
        {
            //--------------------------------------------------------------------------------------------------------//
            case CHARACTER_TYPE.FIRE when other.CompareTag(FIRE_CHARACTER_TAG):
                characterTileInteractionBase = other.GetComponent<FireCharacterTileInteractions>();
                break;
            //--------------------------------------------------------------------------------------------------------//
            case CHARACTER_TYPE.ICE when other.CompareTag(ICE_CHARACTER_TAG):
                characterTileInteractionBase = other.GetComponent<FireCharacterTileInteractions>();
                break;
            //--------------------------------------------------------------------------------------------------------//
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (characterTileInteractionBase == null)
            return;

        _activeObject = other.gameObject;
        IsActive = true;
        CharacterEnterStateChanged?.Invoke();

    }

    private void OnTriggerExit(Collider other)
    {
        if (_activeObject == null)
            return;

        if (other.gameObject != _activeObject)
            return;
        
        _activeObject = null;
        IsActive = false;
        CharacterEnterStateChanged?.Invoke();
    }

    //====================================================================================================================//
    
}
