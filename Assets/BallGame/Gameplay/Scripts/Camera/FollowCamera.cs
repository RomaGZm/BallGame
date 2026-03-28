using UnityEngine;
using DG.Tweening;

namespace BallGame.Gameplay.Cameras
{
    public class SmoothFollowCamera : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0, 5, -8);
        public float smoothTime = 0.2f;

        private Tween moveTween;

        //Move camera to target
        private void LateUpdate()
        {
            if (target == null) return;

            Vector3 desiredPos = target.position + offset;
            moveTween?.Kill();
            moveTween = transform.DOMove(desiredPos, smoothTime)
                                 .SetEase(Ease.OutSine);
        }

        private void OnDestroy()
        {
            moveTween?.Kill();
        }
    }
} 
