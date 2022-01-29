﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleLevelManager : MonoBehaviour
{
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
        CharacterDestination.CharacterEnterStateChanged += on
    }

    private void Start()
    {
        SetupPuzzleTiles();
    }

    private void OnDisable()
    {
        CharacterTileInteractionBase.OnCharacterDied += OnDeath;
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
        
    }

    private void OnDeath(CharacterTileInteractionBase deadCharacter)
    {
        
    }

    //Unity Editor Functions
    //====================================================================================================================//
    
#if UNITY_EDITOR
    [ContextMenu("Find All Level Objects")]
    private void FindAllLevelObjects()
    {
        puzzleTiles = FindObjectsOfType<Tile>();
        characters = FindObjectsOfType<CharacterTileInteractionBase>();
        characterStands = FindObjectsOfType<CharacterDestination>();
    }
    
#endif
}


