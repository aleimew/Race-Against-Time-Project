using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreBoard.Instance.PostScore();
        GetComponent<Collider2D>().enabled = false;
        //load the next level after letting the player look at their time
        //SceneManager.LoadScene("scene");
    }
}
