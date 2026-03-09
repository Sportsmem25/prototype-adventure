using UnityEngine;

public class StaticNPC : HumanBase
{
    [SerializeField] private HumanState staticState;
    protected override void Awake()
    {
        base.Awake();
        agent.enabled = false;
        SetState(staticState);
    }

    protected override void UpdateStateFromMovement()
    {
        
    }
}