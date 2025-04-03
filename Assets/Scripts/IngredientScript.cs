using System;
using UnityEngine;

public class IngredientScript : MonoBehaviour
{
    [SerializeField] private float _heightLimit;
    private bool _isAttached = false;

    public IngredientType ingredientType; 
    public int basePoints = 10; 

    void Update()
    {
        DestroyIfFalls();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isAttached) return;

        Transform parentBread = FindBread(collision.transform);

        if (parentBread != null)
        {
            AttachToBread(parentBread);
        }
    }

    private Transform FindBread(Transform obj)
    {
        while (obj != null)
        {
            if (obj.CompareTag("Bread")) return obj;
            obj = obj.parent;
        }
        return null;
    }

    private void AttachToBread(Transform bread)
    {
        _isAttached = true;
        transform.SetParent(bread);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        BreadType breadType = bread.GetComponent<BreadType>();
        if (breadType != null && ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoints(this.ingredientType, breadType);
        }
    }

    private void DestroyIfFalls()
    {
        if (transform.position.y <= _heightLimit)
        {
            Destroy(gameObject);
        }
    }

    public bool IsAttached => _isAttached;
}
