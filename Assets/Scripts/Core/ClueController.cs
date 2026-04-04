using UnityEngine;

public class ClueController : MonoBehaviour
{
    [SerializeField] private GameObject panelClue;

    private void Start()
    {
        panelClue.SetActive(true);
        Invoke(nameof(Hide), 7f);
    }

    private void Hide()
    {
        panelClue.SetActive(false);
    }
}