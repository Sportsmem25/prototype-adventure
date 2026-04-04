using UnityEngine;
using Zenject;

public class GameLoop : MonoBehaviour
{
    private PlayerInputMain playerInput;

    [Inject]
    public void Construct(PlayerInputMain _playerInput)
    {
        playerInput = _playerInput;
    }

    private void Update()
    {
        playerInput.Tick();
    }
}