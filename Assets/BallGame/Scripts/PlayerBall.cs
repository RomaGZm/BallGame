using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class PlayerBall : MonoBehaviour
{
    [Header("Bullet Ball Settings")]
    [SerializeField] private BulletBall bulletBall;
    [SerializeField] private ForceMode forceMode = ForceMode.Force;
    public float forceBullet = 1;

    [Header("Player Ball Settings")]
    public float maxEnergy = 1;
    public float energy = 1;
    public float decEnergySec = 0.01f;

    [Header("UI")]
    [SerializeField] private Slider sliderEnergy;
    [SerializeField] private Slider sliderForce;

    [Header("Move Settings")]
    [SerializeField] float stopDistance = 2f;
    [SerializeField] float moveSpeed = 5f;

    [Header("Track Settings")]
    [SerializeField] private Transform track;
    public float trackScaleDiv = 10;

    private InputSystem_Actions input;
    private Camera cam;
    private bool isDown = false;
    private Coroutine decEnergy;
    private float forceDecBall = 0;
    private Tween moveTween;

    private void Awake()
    {
        cam = Camera.main;
        input = new InputSystem_Actions();
        sliderEnergy.maxValue = maxEnergy;
        sliderEnergy.value = energy;
        sliderForce.maxValue = maxEnergy;
        sliderForce.value = 0;
        bulletBall.onStopMovement += OnBulletStop;
    }

    private void OnEnable()
    {
        input.UI.Enable();

        input.UI.Click.performed += OnPointerClick;   
    }

    private void OnDisable()
    {
        input.UI.Click.performed -= OnPointerClick;
        input.UI.Disable();

    }
    private void OnDestroy()
    {
        bulletBall.onStopMovement -= OnBulletStop;
    }
 
    private void OnPointerClick(InputAction.CallbackContext ctx)
    {
        if (!isDown && IsMine())
        {
            isDown = true;
            PointerDown();
            return;
        }
        if (isDown)
        {
            PointerUP();
          
            isDown = false;
        }
    }

    private void OnBulletStop()
    {
        Move(bulletBall.transform);
    }
    private void PointerUP()
    {
        StopCoroutine(decEnergy);
        bulletBall.Push(forceDecBall*forceBullet, forceMode);
        sliderForce.value = 0;
    }

    private void PointerDown()
    {
        decEnergy = StartCoroutine(StartDecEnergy());
    }
    IEnumerator StartDecEnergy()
    {
        forceDecBall = 0;

        while (isDown) 
        {
            if(energy <= 0)
            {
                UIManager.Instance.Lose();
            }
            forceDecBall += decEnergySec;
            energy -= decEnergySec;

            sliderEnergy.value = energy;
            sliderForce.value = forceDecBall;

            ScaleBalls(decEnergySec/2);

             yield return new WaitForSeconds(0.1f);
            
        }
       
    }
    private void ScaleBalls(float value)
    {
        transform.localScale = new Vector3(transform.localScale.x - value, transform.localScale.y - value, transform.localScale.z - value);
        bulletBall.transform.localScale = new Vector3(bulletBall.transform.localScale.x + value, bulletBall.transform.localScale.y + value, bulletBall.transform.localScale.z + value);
        track.localScale = new Vector3(track.localScale.x - (value / trackScaleDiv), track.localScale.y, track.localScale.z);
    }

    public void Move(Transform target)
    {
        if (target == null) return;

        Vector3 dir = (target.position - transform.position).normalized;
        Vector3 stopPoint = target.position - dir * stopDistance;
        float distance = Vector3.Distance(transform.position, stopPoint);
        float duration = distance / moveSpeed;

        moveTween?.Kill();

        moveTween = transform.DOMove(stopPoint, duration)
            .SetEase(Ease.Linear)
            .SetLink(gameObject); 
    }

private bool IsMine()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("PlayerBall")))
        {
            if (hit.collider.gameObject == gameObject)
            {
               return true;
            }
        }
        return false;
    }
   
}
