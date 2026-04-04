using UnityEngine;
using DG.Tweening;

public class BoneAnimation : MonoBehaviour
{
    [SerializeField] private float duration;
    private Vector3 rotationAmount = new Vector3(0, 360, 0);

    private void Start()
    {
        transform.DORotate(rotationAmount, duration, RotateMode.FastBeyond360).
            SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}