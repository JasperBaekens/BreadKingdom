using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsFalling : MonoBehaviour
{
    [SerializeField] private GameObject[] _ingredients;
    [SerializeField] private GameObject _butter;
    [SerializeField] private GameObject[] _breadPrefabs;
    [SerializeField] private GameObject[] _startBreadPrefabs;

    private List<Rigidbody> _frozenIngredients = new List<Rigidbody>();

    [SerializeField] private int _ingredientsPerRound = 10;
    [SerializeField] private float _ingredientInterval = 1f;
    [SerializeField] private float _spawnRangeX = 18f;
    [SerializeField] private float _spawnHeight = 10f;
    [SerializeField] private float _cycleRestartDelay = 2f;

    private int _currentIngredientCount = 0;
    private bool _spawningFinished = false;
    private GameObject _currentBread;

    void Start()
    {
        StartCoroutine(SpawnIngredients());
        SpawnTheBread();
    }

    private void SpawnTheBread()
    {
        if (_breadPrefabs.Length == 0)
        {
            return;
        }

        int randomBreadIndex = Random.Range(0, _startBreadPrefabs.Length);
        Vector3 breadPosition = new Vector3(0, 0, transform.position.z); 
        _currentBread = Instantiate(_startBreadPrefabs[randomBreadIndex], breadPosition, Quaternion.identity);
    }

    private void SpawnIngredient()
    {
        if (_currentIngredientCount >= _ingredientsPerRound) return; 

        int randomIndex = Random.Range(0, _ingredients.Length);

        Vector3 spawnPosition = new Vector3(
            Random.Range(-_spawnRangeX, _spawnRangeX),
            _spawnHeight,
            transform.position.z
        );

        Quaternion spawnRotation = Quaternion.Euler(
            Random.Range(-10f, 10f), 
            Random.Range(0f, 360f), 
            Random.Range(-10f, 10f)
        );

        Instantiate(_ingredients[randomIndex], spawnPosition, spawnRotation);
        _currentIngredientCount++;

        if (_currentIngredientCount >= _ingredientsPerRound)
        {
            _spawningFinished = true;
            StartCoroutine(DropFinalBread());
        }
    }

    private IEnumerator SpawnIngredients()
    {
        while (_currentIngredientCount < _ingredientsPerRound)
        {
            SpawnIngredient();
            yield return new WaitForSeconds(_ingredientInterval);
        }
    }
    private IEnumerator DropFinalBread()
    {
        yield return new WaitForSeconds(_ingredientInterval);

        int randomBreadIndex = Random.Range(0, _breadPrefabs.Length);

        Vector3 breadPosition = new Vector3(0, _spawnHeight, transform.position.z);

        GameObject topBread = Instantiate(_breadPrefabs[randomBreadIndex], breadPosition, Quaternion.identity);

        yield return new WaitForSeconds(2f); 

        StartCoroutine(FreezeStackedIngredients());
    }

    private IEnumerator FreezeStackedIngredients()
    {
        GameObject bread = GameObject.FindWithTag("Bread");
        if (bread == null) yield break;

        GameObject[] allIngredients = GameObject.FindGameObjectsWithTag("Ingredient");
        float currentHeight = bread.GetComponent<Collider>().bounds.max.y;

        foreach (GameObject ingredient in allIngredients)
        {
            if (ingredient.transform.parent == null) continue;

            Rigidbody rb = ingredient.GetComponent<Rigidbody>();
            if (rb != null)
            {
                _frozenIngredients.Add(rb); 
            }

            ingredient.transform.position = new Vector3(
                bread.transform.position.x,
                currentHeight + 0.075f,
                bread.transform.position.z
            );
            ingredient.transform.SetParent(bread.transform);
            if (ingredient != bread)
            {
                ingredient.AddComponent<PermanentFrozen>();
            }

            currentHeight += 0.075f;
        }

        yield return new WaitForSeconds(_cycleRestartDelay);
        RestartCycle();
    }

    private void RestartCycle()
    {
        foreach(Rigidbody rb in _frozenIngredients)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        _frozenIngredients.Clear();
        _spawningFinished = false;
        _currentIngredientCount = 0;
        StartCoroutine(SpawnIngredients());
    }
}
