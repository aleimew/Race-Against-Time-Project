using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreBoard : MonoBehaviour
{
    private float startTime;
    //[SerializeField] TextMeshProUGUI text;

    public static ScoreBoard Instance;
    public UnityEvent onLevelCompleted;

    public bool levelCompleted { get; private set; } = false;
    public string levelCompletionText { get; private set; }

    float currentTime = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //startTime = Time.time;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        //text.text = "Time: " + (Time.time - startTime).ToString("F2");
    }

    public float GetTime()
    {
        return currentTime;
        //return Time.time - startTime;
    }

    public void PostScore()
    {
        //text.text = "You Win! -- It Took You " + GetTime() + " Seconds!";
        levelCompletionText = $"You Win! -- It Took You {GetTime():F2} Seconds!";
        levelCompleted = true;
        onLevelCompleted?.Invoke();
    }
}
