using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMP_Text newScoreText;
    public TMP_Text newHighscoreText;

    int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        newScoreText.text = score.ToString() + " POINTS";
        newHighscoreText.text = "HIGHSCORE: " + highscore;

    }

    public void AddPoint()
    {
        score += 1;
        newScoreText.text = score.ToString() + " POINTS";
        if (highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
            highscore = PlayerPrefs.GetInt("highscore", 0);
            newHighscoreText.text = "HIGHSCORE: " + highscore;
        }
    }
}
