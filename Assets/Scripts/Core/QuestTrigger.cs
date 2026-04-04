using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private QuestController controller;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.CompleteCurrentQuest();
            gameObject.SetActive(false);
        }
    }
}