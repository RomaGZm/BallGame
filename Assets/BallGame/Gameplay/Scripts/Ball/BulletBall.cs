using System;
using UnityEngine;

using BallGame.Gameplay.Obstacles;

namespace BallGame.Gameplay.Ball
{
    public class BulletBall : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody rigidbodyBall;
        [SerializeField] private SphereCollider infectionArea;
        [SerializeField] private ObstacleManager obstacleManager;
        //Event calling then stopping 
        public Action onStopMovement;

        /// <summary>
        /// Push bullet to forward direction
        /// </summary>
        /// <param name="force"></param>
        /// <param name="forceMode"></param>
        public void Push(float force, ForceMode forceMode)
        {
            rigidbodyBall.AddForce(Vector3.forward * force, forceMode);
        }

        //Collision with onstacles
        private void OnCollisionEnter(Collision collision)
        {
            rigidbodyBall.linearVelocity = Vector3.zero;
            rigidbodyBall.angularVelocity = Vector3.zero;

            if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                var hits = OverlapBySphereCollider(infectionArea, LayerMask.GetMask("Obstacle"));
                foreach (Collider c in hits)
                {
                    obstacleManager.Infect(c.gameObject, 1f, Color.yellow);
                    // c.gameObject.GetComponent<Obstacle>().Infection();
                }

            }
            onStopMovement?.Invoke();
        }
        //Freezing Z- move direction
        private void FixedUpdate()
        {
            Vector3 v = rigidbodyBall.linearVelocity;

            if (v.z < 0f)
            {
                v.z = 0f;
                rigidbodyBall.linearVelocity = v;
            }
        }
        //Set scale Infection area
        public void SetInfectionArea(float scale)
        {
            Vector3 s = infectionArea.transform.localScale;
            infectionArea.transform.localScale = new Vector3(s.x + scale, s.y + scale, s.z + scale);
        }
        //Detection obstacles over sphere and layer mask
        private Collider[] OverlapBySphereCollider(SphereCollider col, LayerMask layer)
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

}
