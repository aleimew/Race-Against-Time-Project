using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject failStateWindow;

    [SerializeField, Range(5, 120)] private float maxLevelCompletionTime;
    public float timeRemaining { get; private set; }

    public static GameManager Instance;

    [SerializeField] private bool timerEnabled = true;

    private bool resettingFromHazard = false;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        failStateWindow.SetActive(false);
        timeRemaining = maxLevelCompletionTime;
        if (timerEnabled)
        {
            StartCoroutine(Countdown());
            ScoreBoard.Instance.onLevelCompleted.AddListener(StopCountdown);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
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

    public void ResetPlayers()
    {
        foreach (Player_Movement playerController in FindObjectsByType<Player_Movement>(FindObjectsSortMode.None))
        {
            playerController.ResetPosition();
        }
    }

    public void ResetBothPlayersFromHazard(float delay)
    {
        if (resettingFromHazard) return;
        StartCoroutine(ResetBothPlayersFromHazardRoutine(delay));
    }

    private IEnumerator ResetBothPlayersFromHazardRoutine(float delay)
    {
        resettingFromHazard = true;

        Player_Movement[] players = FindObjectsByType<Player_Movement>(FindObjectsSortMode.None);

        for (int i = 0; i < players.Length; i++)
        {
            players[i].LockMovement();
            players[i].SetPlayerDied(true);
        }

        yield return new WaitForSeconds(delay);

        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetPlayerDied(false);
            players[i].ResetPosition();
            players[i].UnlockMovement();
        }

        resettingFromHazard = false;
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}




