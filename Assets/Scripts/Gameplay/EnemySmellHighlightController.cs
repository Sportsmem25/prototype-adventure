using UniRx;
using UnityEngine;

public class EnemySmellHighlightController : MonoBehaviour
{
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private Material smellHighlightMaterial;
    [SerializeField] private SmellController smellController;

    private Material originalMaterial;

    private void Awake()
    {
        originalMaterial = enemyRenderer.material;
    }

    private void Start()
    {
        smellController.IsSmelling.Subscribe(OnSmellChanged).AddTo(this);
    }

    private void OnSmellChanged(bool isSmelling)
    {
        enemyRenderer.material = isSmelling
            ? smellHighlightMaterial
            : originalMaterial;
    }
}
