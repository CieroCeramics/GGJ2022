using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class GameManager : MonoBehaviour
    {
        public static Action NoMoreLevels;
        
        private static GameManager _instance;
        
        [SerializeField]
        private List<PuzzleLevelManager> puzzleLevelsPrefabs;

        private int _currentLevelIndex = -1;
        private PuzzleLevelManager _currentLevelObject;
        
        //====================================================================================================================//

        public static void RestartLevel()
        {
            _instance?._RestartLevel();
        }
        public static void LoadNextLevel()
        {
            _instance?._LoadNextLevel();
        }

        public static void LoadLevel(int levelIndex)
        {
            _instance?._LoadLevel(levelIndex);
        }
        
        //====================================================================================================================//

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;

        }

        //====================================================================================================================//
        
        private void _RestartLevel()
        {
            _LoadLevel(_currentLevelIndex);
        }

        private void _LoadNextLevel()
        {
            var hasNextLevel = _currentLevelIndex + 1 < puzzleLevelsPrefabs.Count;

            if (hasNextLevel == false)
            {
                TryUnloadLevel();
                NoMoreLevels?.Invoke();
                return;
            }
            //TODO Need to determine how to get to the main menu when no more levels remain
            
            _LoadLevel(_currentLevelIndex + 1);
        }

        private void _LoadLevel(int levelIndex)
        {
            TryUnloadLevel();

            _currentLevelIndex = levelIndex;
            _currentLevelObject = Instantiate(puzzleLevelsPrefabs[_currentLevelIndex]);
        }

        private void TryUnloadLevel()
        {
            if(_currentLevelObject != null)
                Destroy(_currentLevelObject.gameObject);
            _currentLevelObject = null;
        }

        //====================================================================================================================//
    }
}