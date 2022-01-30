﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityTemplateProjects;

public class PuzzleLevelManager : MonoBehaviour
{
    public static Action OnVictory;
    
    [SerializeField]
    private string puzzleName;

    [SerializeField]
    private CinemachineTargetGroup cinemachineTargetGroup;
    
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
        
        SetupPuzzleTiles();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) == false)
            return;
        
        GameManager.RestartLevel();
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
        
        cinemachineTargetGroup.m_Targets = characters.Select(x => new CinemachineTargetGroup.Target
        {
            radius = 0.5f,
            target = x.transform,
            weight = 1f
        }).ToArray();
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
    [ContextMenu("Generate and Setup Puzzle")]
    private void GenerateSetupLevel()
    {
        var generator = GetComponent<LevelGen>();
        
        generator.GenerateLevelData();

        FindAllLevelObjects();
    }
    
    [ContextMenu("Find All Objects")]
    private void FindAllLevelObjects()
    {
        puzzleTiles = FindObjectsOfType<Tile>();
        characters = FindObjectsOfType<CharacterTileInteractionBase>();
        characterStands = FindObjectsOfType<CharacterDestination>();
    }
    
#endif
}


