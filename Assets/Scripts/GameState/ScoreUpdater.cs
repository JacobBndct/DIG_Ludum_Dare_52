using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    TextMeshProUGUI highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = transform.Find("CurrentScore").GetComponent<TextMeshProUGUI>();
        highscoreText = transform.Find("HighScore").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        setScoreText();
    }

    void setScoreText()
    {
        scoreText.text = "Score: " + Mathf.RoundToInt(ScoreManager.Instance.CurrentScore);
        highscoreText.text = "Highscore: " + Mathf.RoundToInt(ScoreManager.Instance.HighScore);
    }
}
