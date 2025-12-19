using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private float startTime;
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        text.text = "Time: " + (Time.time - startTime).ToString("F2");
    }

    public float GetTime()
    {
        return Time.time - startTime;
    }
}
