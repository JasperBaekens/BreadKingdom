using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Score1 { get; set; }
    public int Score2 { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
