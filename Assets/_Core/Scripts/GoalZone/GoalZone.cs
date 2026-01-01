using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalZone : MonoBehaviour
{
    private double timer = 0;
    private bool timerIsRunning = false;

    private void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                NextScene();
            }
        }
    }

    private void TimerStart()
    {
        timerIsRunning = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreBoard.Instance.PostScore();
        GetComponent<Collider2D>().enabled = false;
        TimerStart();
        //load the next level after letting the player look at their time
        //SceneManager.LoadScene("scene");
    }
}
