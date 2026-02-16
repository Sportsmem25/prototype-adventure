using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private Transform player;

    private PlayerStateController stateController;

    private void Awake()
    {
        stateController = GetComponent<PlayerStateController>();
    }

    private void Update()
    {
        UpdateCameraMode();
    }
    private void UpdateCameraMode()
    {
        if (stateController.Is(PlayerState.Idle) || stateController.Is(PlayerState.Sit))
            EnableFreeLook();
        else
            EnableFollowMode();
    }

    private void EnableFreeLook()
    {
        freeLook.m_XAxis.m_MaxSpeed = 300f;
        freeLook.m_YAxis.m_MaxSpeed = 2f;
        freeLook.m_RecenterToTargetHeading.m_enabled = false;
    }

    private void EnableFollowMode()
    {
        freeLook.m_XAxis.m_MaxSpeed = 0f;
        freeLook.m_YAxis.m_MaxSpeed = 0f;
        freeLook.m_RecenterToTargetHeading.m_enabled = true;
        freeLook.m_RecenterToTargetHeading.m_WaitTime = 0.1f;
        freeLook.m_RecenterToTargetHeading.m_RecenteringTime = 0.5f;
    }
}