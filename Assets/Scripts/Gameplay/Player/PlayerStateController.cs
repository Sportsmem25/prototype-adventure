using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }

    public void SetState(PlayerState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;
        Debug.Log($"PlayerState {CurrentState}");
    }
    public bool Is(PlayerState state) => CurrentState == state;
}