using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalZone : MonoBehaviour
{
    [SerializeField] private ScoreBoard refScoreBoard;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        refScoreBoard.PostScore();

        //load the next level after letting the player look at their time
        //SceneManager.LoadScene("scene");
    }
}
