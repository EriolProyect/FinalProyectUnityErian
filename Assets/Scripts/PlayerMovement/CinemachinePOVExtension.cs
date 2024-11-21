using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
 [SerializeField]
    private float horizontalSpeed = 10f;
    [SerializeField]
    private float verticalSpeed = 10f;
    [SerializeField]
    private float clampAngle = 90f;

    private InputManager inputManager;
    private Vector3 startingRotation;

    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        if (inputManager == null)
        {
            Debug.LogError("InputManager.Instance is null! Make sure InputManager is properly set up.");
        }

        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase virtualCameraBase, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (virtualCameraBase.Follow != null)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == Vector3.zero)
                {
                    startingRotation = transform.localRotation.eulerAngles;
                }

                if (inputManager != null)
                {
                    Vector2 deltainput = inputManager.GetMouseDelta();

                    startingRotation.x += deltainput.x * verticalSpeed * Time.deltaTime;
                    startingRotation.y += deltainput.y * horizontalSpeed * Time.deltaTime;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                }
                else
                {
                    Debug.LogWarning("InputManager is null in PostPipelineStageCallback.");
                }
            }
        }
        else
        {
            Debug.LogWarning("Virtual Camera's Follow property is not assigned.");
        }
    }
}
