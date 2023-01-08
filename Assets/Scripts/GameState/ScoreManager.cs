using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public float CurrentScore/* { get; private set; }*/;
    [SerializeField] public float HighScore/* { get; private set; }*/;
    public bool active = true;

    [SerializeField] private int numberOfIterations;
    [SerializeField] private float UpdateSpeed;

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
    }

    private void Update()
    {
        if (HighScore <= CurrentScore)
        {
            HighScore = CurrentScore;
        }
    }

    public IEnumerator AddScore(float score)
    {
        float iterationLength = UpdateSpeed / numberOfIterations;
        float iterationScore = (2 * score) / (numberOfIterations * (numberOfIterations + 1));

        for (int i = numberOfIterations; i > 0; i--)
        {
            CurrentScore += i * iterationScore;

            yield return new WaitForSeconds(iterationLength);
        }
    }
}