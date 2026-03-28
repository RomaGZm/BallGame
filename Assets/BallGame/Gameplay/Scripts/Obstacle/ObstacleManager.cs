using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace BallGame.Gameplay.Obstacles
{
    public class ObstacleManager : MonoBehaviour
    {
        private List<ObstacleData> _obstacles = new();

        //Registration obstacle on the spawn
        public void Register(GameObject obj, MeshRenderer rend)
        {
            ObstacleData data = new ObstacleData
            {
                obj = obj,
                rend = rend,
                mpb = new MaterialPropertyBlock()
            };

            rend.GetPropertyBlock(data.mpb);
            _obstacles.Add(data);
        }
        //Infect each object
        public void Infect(GameObject obj, float duration, Color color)
        {
            var obstacle = _obstacles.Find(o => o.obj == obj);
            if (obstacle.obj == null) return;

            Color c = color;

            DOTween.To(
                () => c.a,
                x =>
                {
                    c.a = x;
                    obstacle.mpb.SetColor("_BaseColor", c);
                    obstacle.rend.SetPropertyBlock(obstacle.mpb);
                },
                0f,
                duration
            ).OnComplete(() =>
            {
                obj.SetActive(false);
            });
        }
    }
}