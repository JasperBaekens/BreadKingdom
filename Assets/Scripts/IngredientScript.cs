using UnityEngine;

public class IngredientScript : MonoBehaviour
{
    [SerializeField] private float _heightLimit;

    void Update()
    {
        if (transform.position.y <= _heightLimit)
            Destroy(this.gameObject);
    }
}
