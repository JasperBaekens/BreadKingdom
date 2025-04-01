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

        _randomPosition = new Vector3(Random.Range(-7f,7f), transform.position.y, Random.Range(-3f,3f));
        _randomRotation = new Quaternion(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));
        Instantiate(gameObjects[randomIngredient], _randomPosition, _randomRotation);
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
