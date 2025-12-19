using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    public float GetTime()
    {
        return Time.time - startTime;
    }
}
