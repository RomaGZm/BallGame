
using BallGame.Gameplay.UI;
using UnityEngine;

namespace BallGame.Gameplay.Door
{
    public class DoorTrigger : MonoBehaviour
    {
        [SerializeField] private Door door;
        [SerializeField] private LayerMask triggeredLayer;

        //Trigger on the open door for the player
        private void OnTriggerEnter(Collider other)
        {
            if (door.isOpen) return;

            if (((1 << other.gameObject.layer) & triggeredLayer) != 0)
            {
                door.Open(() =>
                {
                    UIManager.Instance.Win();
                });
            }
        }
    }
}


