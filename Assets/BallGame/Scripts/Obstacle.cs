using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Infection")]
    public float destroyDuration = 1;

    public Color infectionColor = Color.yellow;
    [Header("Infection")]
    [SerializeField] private Material material;
    [SerializeField] private MeshRenderer rend;

    private MaterialPropertyBlock mpb;
    private Tween t;

    private void Awake()
    {
        mpb = new MaterialPropertyBlock();
        rend.GetPropertyBlock(mpb);
    }

    public void Infection()
    {
        Color c = infectionColor;

        t = DOTween.To(
            () => c.a,
            x =>
            {
                c.a = x;
              
                mpb.SetColor("_BaseColor", c);
                rend.SetPropertyBlock(mpb);
            },
            0f,
            destroyDuration
        ).OnComplete(() =>
        {
            t?.Kill();
            gameObject.SetActive(false);
        });
    }
    private void OnDestroy()
    {
        t?.Kill(); 
    }

}
