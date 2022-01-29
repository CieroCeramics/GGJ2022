using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("Main Menu")]
    private GameObject mainMenuWindowObject;
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private Button exitGameMenuButton;

    [SerializeField, Header("Game Pause")]
    private GameObject gamePauseWindowObject;
    [SerializeField]
    private TMP_Text gamePauseTitleText;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button restartLevelButton;
    [SerializeField]
    private Button nextLevelButton;
    [SerializeField]
    private Button exitGameGamePauseButton;
    
    //====================================================================================================================//

    private void OnEnable()
    {
        PuzzleLevelManager.OnVictory += OnVictory;
        CharacterTileInteractionBase.OnCharacterDied += OnCharacterDied;
        GameManager.NoMoreLevels += SetupWindows;
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SetupButtons();
        SetupWindows();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == false)
            return;

        SetGamePauseWindowActive(!gamePauseWindowObject.activeInHierarchy, "Paused", true, true, false);
    }

    private void OnDisable()
    {
        PuzzleLevelManager.OnVictory -= OnVictory;
        CharacterTileInteractionBase.OnCharacterDied -= OnCharacterDied;
        GameManager.NoMoreLevels -= SetupWindows;
    }

    //====================================================================================================================//

    private void SetupButtons()
    {
        startGameButton.onClick.AddListener(OnStartGamePressed);
        exitGameMenuButton.onClick.AddListener(OnExitGamePressed);
        
        resumeButton.onClick.AddListener(OnResumeGamePressed);
        restartLevelButton.onClick.AddListener(OnRestartLevelPressed);
        nextLevelButton.onClick.AddListener(OnNextLevelPressed);
        exitGameGamePauseButton.onClick.AddListener(OnExitGamePressed);
    }

    private void SetupWindows()
    {
        mainMenuWindowObject.SetActive(true);
        gamePauseWindowObject.SetActive(false);
    }

    private void SetGamePauseWindowActive(in bool state, in string title, bool showResumeButton,bool showRestartLevelButton, bool showNextLevelButton)
    {
        gamePauseWindowObject.SetActive(state);

        if (state == false)
            return;
        
        gamePauseTitleText.text = title;
        resumeButton.gameObject.SetActive(showResumeButton);
        restartLevelButton.gameObject.SetActive(showRestartLevelButton);
        nextLevelButton.gameObject.SetActive(showNextLevelButton);
        
    }
    

    //====================================================================================================================//

    private void OnStartGamePressed()
    {
        mainMenuWindowObject.SetActive(false);
        gamePauseWindowObject.SetActive(false);
        //TODO Need to implement some sort of fading
        GameManager.LoadLevel(0);
    }

    private void OnResumeGamePressed()
    {
        gamePauseWindowObject.SetActive(false);
    }

    private void OnRestartLevelPressed()
    {
        GameManager.RestartLevel();
        gamePauseWindowObject.SetActive(false);
    }

    private void OnNextLevelPressed()
    {
        GameManager.LoadNextLevel();
        gamePauseWindowObject.SetActive(false);
    }
    private void OnExitGamePressed()
    {
        Application.Quit();
    }
    
    //====================================================================================================================//

    private void OnVictory()
    {
        SetGamePauseWindowActive(true, "Victory", false, false, true);
    }

    private void OnCharacterDied(CharacterTileInteractionBase _)
    {
        SetGamePauseWindowActive(true, "Died", false, true, false);
    }
    
    //====================================================================================================================//
}
