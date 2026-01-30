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
    private bool levelFailed = false;

    [Header("SFX")]
    [SerializeField] private AudioClip wallHitSound;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private float wallHitCooldown = 0.15f;
    private bool wallHitLocked = false;

    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioSource musicSource;

    [Header("Timer Ticking")]
    [SerializeField] private AudioClip tickingSound;
    [SerializeField] private AudioSource tickingSource;
    [SerializeField] private float tickingThreshold = 5f;
    private bool tickingActive = false;

    private void Awake()
    {
        Instance = this;

        if (sfxSource == null || musicSource == null || tickingSource == null)
        {
            AudioSource[] sources = GetComponents<AudioSource>();
            if (sfxSource == null && sources.Length > 0) sfxSource = sources[0];
            if (musicSource == null && sources.Length > 1) musicSource = sources[1];
            if (tickingSource == null && sources.Length > 2) tickingSource = sources[2];
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Start()
    {
        levelFailed = false;

        failStateWindow.SetActive(false);
        timeRemaining = maxLevelCompletionTime;

        StartMusicFromBeginning();
        StopTicking();

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

        if (levelFailed) return;

        HandleTickingSound();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelFailed = false;
        StartMusicFromBeginning();
        StopTicking();
    }

    private void StartMusicFromBeginning()
    {
        if (musicSource == null || backgroundMusic == null) return;

        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Stop();
        musicSource.time = 0f;
        musicSource.Play();
    }

    private void StopTicking()
    {
        tickingActive = false;
        if (tickingSource != null)
            tickingSource.Stop();
    }

    private void HandleTickingSound()
    {
        if (!timerEnabled || tickingSource == null || tickingSound == null)
            return;

        float timeLeft = GetTimeRemaining();

        if (timeLeft <= tickingThreshold && !tickingActive)
        {
            tickingActive = true;
            tickingSource.clip = tickingSound;
            tickingSource.loop = true;
            tickingSource.Play();
        }
        else if (timeLeft > tickingThreshold && tickingActive)
        {
            StopTicking();
        }
    }

    private void StopAllAudio()
    {
        if (sfxSource != null) sfxSource.Stop();
        if (musicSource != null) musicSource.Stop();
        if (tickingSource != null) tickingSource.Stop();
        tickingActive = false;
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
        levelFailed = true;
        StopAllCoroutines();
        failStateWindow.SetActive(true);
        StopAllAudio();
    }

    public void OnPlayerHitWall()
    {
        if (wallHitLocked) return;
        wallHitLocked = true;

        if (sfxSource != null && wallHitSound != null)
            sfxSource.PlayOneShot(wallHitSound);

        ResetPlayers();
        StartMusicFromBeginning();
        StopTicking();

        StartCoroutine(WallHitUnlockRoutine());
    }

    private IEnumerator WallHitUnlockRoutine()
    {
        yield return new WaitForSeconds(wallHitCooldown);
        wallHitLocked = false;
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

        StartMusicFromBeginning();
        StopTicking();

        resettingFromHazard = false;
    }

    public void ReloadCurrentScene()
    {
        StopAllAudio();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        StopAllAudio();
        SceneManager.LoadScene("MainMenu");
    }
}
