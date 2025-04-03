using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScreen2 : MonoBehaviour
{
    public Text timerText;
    private float timeLeft = 60f;

    public Text score1Text;
    public Text score2Text;
    private int score2 = 0;

    void Start()
    {
        score1Text.text = "X " + GameManager.Instance.Score1;
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timeLeft);

        if (timeLeft <= 0)
        {
            FinishGame();
        }
    }

    public void AddScore(int points)
    {
        score2 += points;
        score2Text.text = "Score: " + score2;
    }

    public void FinishGame()
    {
        GameManager.Instance.Score2 = score2;
        SceneManager.LoadScene("EndScene");
    }
}
