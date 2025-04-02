using System;
using UnityEngine;

public class IngredientScript : MonoBehaviour
{
    [SerializeField] private float _heightLimit;
    private bool _isAttached = false;

    void Update()
    {
        DestroyIfFalls();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isAttached) return;

        Transform parentBread = FindBread(collision.collider.transform);

        if (parentBread != null)
        {
            AttachToBread(collision.collider.transform);
        }
    }

    private Transform FindBread(Transform collidedObject)
    {
        if (collidedObject.CompareTag("Bread"))
        {
            return collidedObject;
        }
        if (collidedObject.CompareTag("Ingredient"))
        {
            return collidedObject.root;
        }
        return null;
    }

    private void AttachToBread(Transform bread)
    {
        _isAttached = true;
        transform.SetParent(bread);
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    private void DestroyIfFalls()
    {
        if (transform.position.y <= _heightLimit)
            Destroy(gameObject);
    }

}
