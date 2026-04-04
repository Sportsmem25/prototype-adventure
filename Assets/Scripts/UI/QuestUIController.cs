using UnityEngine;
using TMPro;
using UniRx;

public class QuestUIController : MonoBehaviour
{
    [SerializeField] private QuestController questController;
    [SerializeField] private TextMeshProUGUI questText;

    private void Start()
    {
        questController.CurrentQuest.Subscribe(UpdateQuestText).AddTo(this);
    }

    private void UpdateQuestText(string text)
    {
        questText.text = text;
    }
}
