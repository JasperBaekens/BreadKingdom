using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Text finalScoreText;

    void Start()
    {
        int finalScore = GameManager.Instance.Score1 * GameManager.Instance.Score2;
        finalScoreText.text = "Final Score: " + finalScore;
    }

    public void Replay()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
