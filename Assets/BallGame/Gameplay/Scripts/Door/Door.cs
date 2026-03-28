using UnityEngine;
using DG.Tweening;
using System;

namespace BallGame.Gameplay.Door
{
    public class Door : MonoBehaviour
    {
        [Header("Settings")]
        public Vector3 openOffset = new Vector3(1f, 0f, 0f);
        public float duration = 0.7f;
        [Space]
        public bool isOpen = false;
       
        private Vector3 startPos;
        private Tween tween;

        //Init local position
        private void Start()
        {
            startPos = transform.localPosition;
        }

        /// <summary>
        /// Open door and calling complete event
        /// </summary>
        /// <param name="openComplete"></param>
        public void Open(Action openComplete)
        {
            tween?.Kill();

            Vector3 target = isOpen ? startPos : startPos + openOffset;

            tween = transform.DOLocalMove(target, duration)
                .SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    openComplete?.Invoke();
                });

            isOpen = !isOpen;
        }

    }
}
