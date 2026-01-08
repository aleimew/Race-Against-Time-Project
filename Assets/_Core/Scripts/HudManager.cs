using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{
    [SerializeField] TMP_Text countdownText;


    // Update is called once per frame
    void Update()
    {
        if(ScoreBoard.Instance.levelCompleted)
        {
            countdownText.text = ScoreBoard.Instance.levelCompletionText;
            countdownText.color = Color.green;
        }
        else
        {
            countdownText.text = $"{GameManager.Instance.GetTimeRemaining():F2}";
            countdownText.color = GameManager.Instance.GetTimeRemaining() > 10 ? Color.white : Color.red;
        }
    }

    public void RetryButtonPressed()
    {
        GameManager.Instance.ReloadCurrentScene();
    }

    public void MainMenuButtonPressed()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
