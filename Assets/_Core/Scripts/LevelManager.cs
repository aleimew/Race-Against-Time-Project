using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Level Manager - Handles level progression and unlocking
/// Should be placed in each level scene
/// </summary>
public class LevelManager : MonoBehaviour
{
    [Header("Level Configuration")]
    [SerializeField] private int currentLevelIndex = 0;
    [SerializeField] private string nextLevelSceneName = "";

    [Header("Level Names")]
    [SerializeField] private string[] levelSceneNames = { 
        "Tutorial", 
        "Level1_Easy", 
        "Level2_Intermediate", 
        "Level3_Hard", 
        "Level4_ExtraHard" 
    };

    /// <summary>
    /// Called when level is completed successfully
    /// </summary>
    public void OnLevelCompleted()
    {
        // Unlock next level
        if (currentLevelIndex >= 0 && currentLevelIndex < levelSceneNames.Length - 1)
        {
            int nextLevelIndex = currentLevelIndex + 1;
            LevelSelectManager.UnlockLevel(nextLevelIndex);
        }

        // Load next level after a short delay (to show score)
        StartCoroutine(LoadNextLevelDelayed(2f));
    }

    /// <summary>
    /// Load next level after delay
    /// </summary>
    private IEnumerator LoadNextLevelDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!string.IsNullOrEmpty(nextLevelSceneName))
        {
            SceneManager.LoadScene(nextLevelSceneName);
        }
        else if (currentLevelIndex >= 0 && currentLevelIndex < levelSceneNames.Length - 1)
        {
            // Auto-load next level if scene name not specified
            SceneManager.LoadScene(levelSceneNames[currentLevelIndex + 1]);
        }
        else
        {
            // All levels completed - return to main menu
            SceneManager.LoadScene("MainMenu");
        }
    }

    /// <summary>
    /// Return to main menu
    /// </summary>
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Restart current level
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

