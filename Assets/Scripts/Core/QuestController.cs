using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public IReadOnlyReactiveProperty<string> CurrentQuest => currentQuest;

    [SerializeField] private List<Quest> quests;
    private ReactiveProperty<string> currentQuest = new();
    private int currentQuestIndex = 0;

    private void Start()
    {
        StartFirstQuest();
    }

    public void CompleteCurrentQuest()
    {
        quests[currentQuestIndex].IsCompleted = true;
        currentQuestIndex++;

        if (currentQuestIndex < quests.Count)
            currentQuest.Value = quests[currentQuestIndex].Description;
        else
            currentQuest.Value = "Все задания выполнены";
    }

    private void StartFirstQuest()
    {
        if (quests.Count == 0)
            return;
        currentQuestIndex = 0;
        currentQuest.Value = quests[currentQuestIndex].Description;

    }

}

[System.Serializable]
public class Quest
{
    public string Description;
    public bool IsCompleted;
}
