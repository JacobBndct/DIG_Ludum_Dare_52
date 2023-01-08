using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float CurrentScore { get; private set; }
    public bool active = true;

    public static ScoreManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CurrentScore = 0f;
        DontDestroyOnLoad(gameObject);
    }

    //Add Score when Alien dies and/or other event happens
    public void AddScore(float score)
    {
        CurrentScore += score;
    }
}