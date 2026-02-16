using UnityEngine;
using UniRx;
using Zenject;
using System.Collections.Generic;

public class SmellVisualController : MonoBehaviour
{
    [SerializeField] private SmellController smellController;
    [SerializeField] private GameObject smellOverlay;
    [SerializeField] private List<SmellTarget> smellTargets;

    private void Start()
    {

        smellController.IsSmelling.DistinctUntilChanged().Subscribe(OnSmellStateChanged).AddTo(this);
    }

    private void OnSmellStateChanged(bool isActive)
    {
        if(smellOverlay != null)
            smellOverlay.SetActive(isActive);

        foreach (var target in smellTargets)
        {
            if(target != null)
                target.SetVisible(isActive);
        }
        //smellOverlay.SetActive(isActive);

        //foreach (var target in smellTargets)
        //    target.SetVisible(isActive);
    }
}
