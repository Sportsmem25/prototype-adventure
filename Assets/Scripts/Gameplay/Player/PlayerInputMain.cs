using UniRx;
using UnityEngine;

public class PlayerInputMain
{
    public IReadOnlyReactiveProperty<Vector2> MoveDirection => moveDirection;
    public IReadOnlyReactiveProperty<bool> Sprint => sprint;

    private readonly ReactiveProperty<Vector2> moveDirection = new();
    private readonly ReactiveProperty<bool> sprint = new();

    public void Tick()
    {
        Vector2 move = new (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection.Value = Vector2.ClampMagnitude(move, 1f);
        sprint.Value = Input.GetKey(KeyCode.LeftShift);
    }
}
