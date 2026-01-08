using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Main Menu Manager - Handles main menu UI and navigation
/// </summary>
public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private GameObject howToPlayPanel;


    [Header("Main Menu Elements")]
    [SerializeField] private TextMeshProUGUI gameTitleText;
    [SerializeField] private Button startButton;
    [SerializeField] private Button levelSelectButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Transform levelSelectScrollArea;

    [Header("Level Settings")]
    [SerializeField] private string tutorialSceneName = "Tutorial";
    [SerializeField] private string[] levelSceneNames = { "Tutorial", "Level1_Easy", "Level2_Intermediate", "Level3_Hard", "Level4_ExtraHard" };

    [Header("References")]
    [SerializeField] private GameObject levelSelectButtonPrefab;

    private void Start()
    {
        // Initialize UI
        if (gameTitleText != null)
        {
            gameTitleText.text = "RACE AGAINST TIME";
        }

        // Set up button listeners
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }

        if (levelSelectButton != null)
        {
            levelSelectButton.onClick.AddListener(OnLevelSelectButtonClicked);
        }

        if (howToPlayButton != null)
        {
            howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
        }

        if(quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        int index = 0;
        foreach (string levelName in levelSceneNames)
        {
            GameObject instance = Instantiate(levelSelectButtonPrefab, levelSelectScrollArea);
            instance.GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene(levelName); } );
            string text = levelName.Split('_')[1];
            text = text.Replace(' ', '!');
            instance.GetComponentInChildren<TMP_Text>().text = text;
            index++;
        }


        // Show main menu by default
        ShowMainMenu();
    }

    /// <summary>
    /// Start button - Loads the tutorial/first level
    /// </summary>
    public void OnStartButtonClicked()
    {
        if (!string.IsNullOrEmpty(tutorialSceneName))
        {
            SceneManager.LoadScene(tutorialSceneName);
        }
        else
        {
            Debug.LogWarning("Tutorial scene name not set!");
        }
    }


    /// <summary>
    /// Level Select button - Shows level selection panel
    /// </summary>
    public void OnLevelSelectButtonClicked()
    {
        ShowLevelSelect();
    }

    /// <summary>
    /// How to Play button - Shows instructions panel
    /// </summary>
    public void OnHowToPlayButtonClicked()
    {
        ShowHowToPlay();
    }

    /// <summary>
    /// Show main menu panel
    /// </summary>
    public void ShowMainMenu()
    {
        SetPanelActive(mainMenuPanel, true);
        SetPanelActive(levelSelectPanel, false);
        SetPanelActive(howToPlayPanel, false);
    }

    /// <summary>
    /// Show level select panel
    /// </summary>
    public void ShowLevelSelect()
    {
        SetPanelActive(mainMenuPanel, false);
        SetPanelActive(levelSelectPanel, true);
        SetPanelActive(howToPlayPanel, false);
    }

    /// <summary>
    /// Show how to play panel
    /// </summary>
    public void ShowHowToPlay()
    {
        SetPanelActive(mainMenuPanel, false);
        SetPanelActive(levelSelectPanel, false);
        SetPanelActive(howToPlayPanel, true);
    }

    /// <summary>
    /// Helper method to safely set panel active state
    /// </summary>
    private void SetPanelActive(GameObject panel, bool active)
    {
        if (panel != null)
        {
            panel.SetActive(active);
        }
    }

    /// <summary>
    /// Back button - Returns to main menu
    /// </summary>
    public void OnBackButtonClicked()
    {
        ShowMainMenu();
    }

    /// <summary>
    /// Quit game (for standalone builds)
    /// </summary>
    public void OnQuitButtonClicked()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

