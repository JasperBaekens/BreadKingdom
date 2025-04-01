using System.Collections;
using UnityEngine;

public class IngredientsFalling : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    [SerializeField] private float _ingredientNum;
    [SerializeField] private float _ingredientInterval;

    private Vector3 _randomPosition;
    private Quaternion _randomRotation;
    void Start()
    {
        StartCoroutine(SpawnIngredients());
    }

    private void GetIngredient()
    {
        int randomIngredient = Random.Range(0, gameObjects.Length);

        _randomPosition = new Vector3(Random.Range(-18f,18f), transform.position.y, transform.position.z);
        Instantiate(gameObjects[randomIngredient], _randomPosition, Quaternion.identity);
    }

    private IEnumerator SpawnIngredients()
    {
        WaitForSeconds waitTime = new WaitForSeconds(_ingredientInterval);

        while (true)
        {
            GetIngredient();
            yield return waitTime;
        }
    }
}
