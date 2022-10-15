using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public float startingTime;
    float currentTime;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highscoreText;

    public GameObject EndMenuUI;

    bool ended = false;
    private void Start()
    {
        currentTime = startingTime;
        EndMenuUI.SetActive(false);
    }
    void Update()
    {
        currentTime -= Time.deltaTime;
        timerText.text = ((int)currentTime / 60).ToString() + " : " + (currentTime % 60).ToString("0");
        if (currentTime <= 0)
        {
            currentTime = 0;
            if (!ended)
            {
                ended = true;
                EndGame();
            }
        }
    }
    void EndGame()
    {
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        EndMenuUI.SetActive(true);

        float highscore = PlayerPrefs.GetInt("highscore", 0);
        highscoreText.text = "HIGHSCORE: " + highscore;
        scoreText.text = "SCORE: " + ScoreManager.score.ToString();
    }
    public void KeepSmashing()
    {
        EndMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
