using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] TMP_Text countdownText;

    // Update is called once per frame
    void Update()
    {
        countdownText.text = $"{GameManager.Instance.timeRemaining:F2}";
        countdownText.color = GameManager.Instance.timeRemaining > 10 ? Color.white : Color.red;
    }

    public void RetryButtonPressed()
    {
        GameManager.Instance.ReloadCurrentScene();
    }
}
