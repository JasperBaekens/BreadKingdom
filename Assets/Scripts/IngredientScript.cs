using System;
using UnityEngine;

public class IngredientScript : MonoBehaviour
{
    [SerializeField] private float _heightLimit;
    private FixedJoint _joint;

    void Update()
    {
        DestroyIfFalls();
    }

    private void OnCollisionEnter(Collision collision)
    {
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.anchor = collision.contacts[0].point;
        joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
        joint.enableCollision = false;
    }


    private void DestroyIfFalls()
    {
        if (transform.position.y <= _heightLimit)
            Destroy(this.gameObject);
    }
}
