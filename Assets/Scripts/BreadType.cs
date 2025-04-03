using System.Collections.Generic;
using UnityEngine;

public class BreadType : MonoBehaviour
{
    public string breadName;

    [System.Serializable]
    public class IngredientPreference
    {
        public IngredientType type;
        public int pointMultiplier = 2; 
    }

    public List<IngredientPreference> preferredIngredients;
}
public enum IngredientType
{
    Cheese,
    Lettuce,
    Tomato,
    Bacon,
    Salami,
    Fish,
    Bread
}
