
using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (door.isOpen) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerBall"))
        {
            door.Open(() =>
            {
                UIManager.Instance.Win();
            });
        }
    }

    

}
