using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This is manager script that should be instanciated in every level
/// Handles:
///     - Countdown system:
///     - Opening the "next" scene to progress and current scene to restart
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Reference to the window that displays if the player "fails" the current level
    /// </summary>
    [SerializeField] private GameObject failStateWindow;

    [SerializeField, Range(5, 120)] private float maxLevelCompletionTime;
    public float timeRemaining { get; private set; }

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        failStateWindow.SetActive(false);
        timeRemaining = maxLevelCompletionTime;
        StartCoroutine(Countdown());
        ScoreBoard.Instance.onLevelCompleted.AddListener(StopCountdown);
    }

    private void StopCountdown()
    {
        StopAllCoroutines();
    }

    IEnumerator Countdown()
    {
        while (maxLevelCompletionTime > ScoreBoard.Instance.GetTime())
        {
            yield return null;
        }
        LevelFailed();
    }

    public float GetTimeRemaining()
    {
        return maxLevelCompletionTime - ScoreBoard.Instance.GetTime();
    }

    public void LevelFailed()
    {
        StopCoroutine(Countdown());
        failStateWindow.SetActive(true);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}