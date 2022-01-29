using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleLevelManager : MonoBehaviour
{
    public static Action OnVictory;
    
    [SerializeField]
    private string puzzleName;
    
    [SerializeField] 
    private Tile[] puzzleTiles;

    [SerializeField]
    private CharacterTileInteractionBase[] characters;
    [SerializeField]
    private CharacterDestination[] characterStands;

    public Dictionary<Vector2Int, Tile> PuzzleTiles { get; private set; }

    //Unity Functions
    //====================================================================================================================//

    private void OnEnable()
    {
        CharacterTileInteractionBase.OnCharacterDied += OnDeath;
        CharacterDestination.CharacterEnterStateChanged += CheckForVictoryCondition;
    }

    private void Start()
    {
        SetupPuzzleTiles();
    }

    private void OnDisable()
    {
        CharacterTileInteractionBase.OnCharacterDied -= OnDeath;
        CharacterDestination.CharacterEnterStateChanged -= CheckForVictoryCondition;
    }

    //PuzzleLevelManager Functions
    //====================================================================================================================//

    private void SetupPuzzleTiles()
    {
        if (PuzzleTiles == null)
            PuzzleTiles = new Dictionary<Vector2Int, Tile>();
        
        for (int i = 0; i < puzzleTiles.Length; i++)
        {
            if(PuzzleTiles.ContainsKey(puzzleTiles[i].coordinate))
                Debug.LogError($"Object {puzzleTiles[i].gameObject.name} trying to use coordinate [{puzzleTiles[i].coordinate}]");
            
            PuzzleTiles.Add(puzzleTiles[i].coordinate, puzzleTiles[i]);
        }
    }

    private void CheckForVictoryCondition()
    {
        if (characterStands.Any(x => x.IsActive == false))
            return;
        
        OnVictory?.Invoke();
    }

    private void OnDeath(CharacterTileInteractionBase deadCharacter)
    {
        
    }

    //Unity Editor Functions
    //====================================================================================================================//
    
#if UNITY_EDITOR
    [ContextMenu("Generate & Setup Puzzle Level Manager")]
    private void FindAllLevelObjects()
    {
        var generator = GetComponent<LevelGen>();
        
        generator.GenerateLevelData();

        puzzleTiles = FindObjectsOfType<Tile>();
        characters = FindObjectsOfType<CharacterTileInteractionBase>();
        characterStands = FindObjectsOfType<CharacterDestination>();
    }
    
#endif
}


