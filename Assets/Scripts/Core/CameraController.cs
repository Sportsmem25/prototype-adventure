using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float mouseSensitivity = 2.5f;
    [SerializeField] private float rotationSmoothSpeed = 4f;
    [SerializeField] private float minY = -30f;
    [SerializeField] private float maxY = 60f;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 cameraOffset;

    private float followSmooth = 0.1f;
    private float yaw;
    private float pitch;
    private Vector3 followVelocity;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        yaw = player.eulerAngles.y;
    }

    private void LateUpdate()
    {
        FollowPlayer();
        RotationCamera();
        UpdateCameraPosition();
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.position, ref followVelocity, followSmooth);
    }

    private void RotationCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minY, maxY);

        cameraPivot.localRotation = Quaternion.Euler(pitch, yaw, 0);
    }

    private void UpdateCameraPosition()
    {
        cameraTransform.localPosition = cameraOffset;
        cameraTransform.LookAt(cameraPivot.position);
    }
}