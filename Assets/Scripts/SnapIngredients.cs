using UnityEngine;

public class SnapIngredients : MonoBehaviour
{
    private bool _hasLanded = false;
    [SerializeField] private float _gridSize = 1.0f; 

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasLanded) return;

        if (collision.gameObject.CompareTag("Bread") || collision.gameObject.CompareTag("Ingredient"))
        {
            _hasLanded = true;

            // Get top of the bread/ingredient stack
            Collider otherCollider = collision.collider;
            float surfaceY = otherCollider.bounds.max.y;

            // Snap to a grid
            Vector3 newPos = transform.position;
            newPos.x = Mathf.Round(newPos.x / _gridSize) * _gridSize;
            newPos.z = Mathf.Round(newPos.z / _gridSize) * _gridSize;
            newPos.y = surfaceY + 0.05f; 

            transform.position = newPos;
            transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0); 
            transform.localScale = Vector3.one; // Uniform scale

            
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
