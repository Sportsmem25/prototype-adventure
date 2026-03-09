using UniRx;
using UnityEngine;

public class EnemySmellHighlightController : MonoBehaviour
{
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private Material smellHighlightMaterial;
    [SerializeField] private SmellController smellController;

    private Material[] originalMaterial;
    private Material[] highlightedMaterial;

    private void Awake()
    {
        originalMaterial = enemyRenderer.materials;
        highlightedMaterial = new Material[originalMaterial.Length + 1];
        for(int i = 0; i < originalMaterial.Length; i++)
        {
            highlightedMaterial[i] = originalMaterial[i];
        }
        highlightedMaterial[originalMaterial.Length] = smellHighlightMaterial;
    }

    private void Start()
    {
        smellController.IsSmelling.Subscribe(OnSmellChanged).AddTo(this);
    }

    private void OnSmellChanged(bool isSmelling)
    {
        enemyRenderer.materials = isSmelling
            ? highlightedMaterial
            : originalMaterial;
    }
}
