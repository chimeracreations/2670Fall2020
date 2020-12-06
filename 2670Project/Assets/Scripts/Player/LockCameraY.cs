using UnityEngine;
using Cinemachine;
 
[SaveDuringPlay] [AddComponentMenu("")]
public class LockCameraY : CinemachineExtension
{
    [Tooltip("Lock the camera's Y position to this value")]
    public float m_YRotation = 0;
 
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var ori = state.FinalOrientation;
            ori.y = ori.y;
            state.OrientationCorrection = ori;
        }
    }
}
