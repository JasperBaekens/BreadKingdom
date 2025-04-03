using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScreen1 : MonoBehaviour
{
    public Text timerText;
    public Text scoreText;
    private float timeLeft = 15f;
    private int score1 = 0;


    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timeLeft);

        if (timeLeft <= 0)
        {
            GameManager.Instance.Score1 = score1;
            SceneManager.LoadScene("protoScene");
        }
    }

    public void UpdateScore(int points)
    {
        score1 = points;
        scoreText.text = "Score: " + score1;
    }
}