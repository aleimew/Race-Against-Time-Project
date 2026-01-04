using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Level Select Manager - Handles level selection UI and level unlocking
/// </summary>
public class LevelSelectManager : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private LevelData[] levels;

    [Header("UI Elements")]
    [SerializeField] private Transform levelButtonParent;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Button backButton;

    [System.Serializable]
    public class LevelData
    {
        public string levelName;
        public string sceneName;
        public string displayName;
        public bool isUnlocked = false;
        public int levelIndex;
    }

    private void Start()
    {
        // Load unlocked levels from PlayerPrefs
        LoadLevelProgress();

        // Create level buttons
        CreateLevelButtons();

        // Set up back button
        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }
    }

    /// <summary>
    /// Load level progress from PlayerPrefs
    /// </summary>
    private void LoadLevelProgress()
    {
        // Tutorial is always unlocked
        if (levels.Length > 0)
        {
            levels[0].isUnlocked = true;
        }

        // Check unlocked levels
        for (int i = 1; i < levels.Length; i++)
        {
            string key = $"Level_{i}_Unlocked";
            levels[i].isUnlocked = PlayerPrefs.GetInt(key, 0) == 1;
        }
    }

    /// <summary>
    /// Create level selection buttons dynamically
    /// </summary>
    private void CreateLevelButtons()
    {
        if (levelButtonParent == null || levelButtonPrefab == null)
        {
            Debug.LogWarning("Level button parent or prefab not assigned!");
            return;
        }

        // Clear existing buttons
        foreach (Transform child in levelButtonParent)
        {
            Destroy(child.gameObject);
        }

        // Create buttons for each level
        for (int i = 0; i < levels.Length; i++)
        {
            GameObject buttonObj = Instantiate(levelButtonPrefab, levelButtonParent);
            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText != null)
            {
                buttonText.text = levels[i].displayName;
            }

            // Set button interactability based on unlock status
            if (button != null)
            {
                button.interactable = levels[i].isUnlocked;
                
                int levelIndex = i; // Capture for closure
                button.onClick.AddListener(() => LoadLevel(levelIndex));
            }

            // Visual indication of locked levels
            if (!levels[i].isUnlocked)
            {
                Image buttonImage = buttonObj.GetComponent<Image>();
                if (buttonImage != null)
                {
                    Color lockedColor = buttonImage.color;
                    lockedColor.a = 0.5f;
                    buttonImage.color = lockedColor;
                }
            }
        }
    }

    /// <summary>
    /// Load a specific level
    /// </summary>
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levels.Length)
        {
            if (levels[levelIndex].isUnlocked)
            {
                SceneManager.LoadScene(levels[levelIndex].sceneName);
            }
            else
            {
                Debug.Log($"Level {levels[levelIndex].displayName} is locked!");
            }
        }
    }

    /// <summary>
    /// Unlock a level (called when completing previous level)
    /// </summary>
    public static void UnlockLevel(int levelIndex)
    {
        string key = $"Level_{levelIndex}_Unlocked";
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Back button handler
    /// </summary>
    private void OnBackButtonClicked()
    {
        MainMenu mainMenu = FindObjectOfType<MainMenu>();
        if (mainMenu != null)
        {
            mainMenu.ShowMainMenu();
        }
    }
}

