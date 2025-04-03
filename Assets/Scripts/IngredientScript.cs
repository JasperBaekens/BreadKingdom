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
            rb.constraints = RigidbodyConstraints.FreezeAll; // Fully freeze to avoid issues
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

    public void DetachFromSandwich()
    {
        _isAttached = false;
        transform.SetParent(null);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None; // Ensure it moves properly
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

}
