using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int _totalScore = 0;

    private void Awake()
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

    public void AddPoints(IngredientType ingredientType, BreadType breadType)
    {
        int points = CalculatePoints(ingredientType, breadType);
        _totalScore += points;
        Debug.Log($"Added {points} points! Total: {_totalScore}");
    }

    private int CalculatePoints(IngredientType ingredientType, BreadType breadType)
    {
        int basePoints = 100; 

        foreach (var preference in breadType.preferredIngredients)
        {
            if (preference.type == ingredientType)
            {
                return basePoints * preference.pointMultiplier;
            }
        }

        return basePoints;
    }
}
