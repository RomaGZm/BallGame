using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletBall : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbodyBall;
    [SerializeField] private SphereCollider infectionArea;

    public Action onStopMovement;

    public void Push(float force, ForceMode forceMode)
    {
        rigidbodyBall.AddForce(Vector3.forward * force, forceMode);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {

            var hits = OverlapBySphereCollider(infectionArea, LayerMask.GetMask("Obstacle"));
            foreach(Collider c in hits)
            {
                rigidbodyBall.linearVelocity = Vector3.zero;
                c.gameObject.GetComponent<Obstacle>().Infection();
                onStopMovement?.Invoke();
            }
            
        }

    }

    public void SetInfectionArea(float scale)
    {
        Vector3 s = infectionArea.transform.localScale;
        infectionArea.transform.localScale = new Vector3(s.x + scale, s.y + scale, s.z + scale);
    }

    Collider[] OverlapBySphereCollider(SphereCollider col, LayerMask layer)
    {
       
        Vector3 center = col.transform.TransformPoint(col.center);

        float radius = col.radius * Mathf.Max(
            col.transform.lossyScale.x,
            col.transform.lossyScale.y,
            col.transform.lossyScale.z
        );

        return Physics.OverlapSphere(center, radius, layer, QueryTriggerInteraction.Collide);
    }
}
