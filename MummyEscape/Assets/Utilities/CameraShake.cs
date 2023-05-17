using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    private void Awake()
    {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {    
        cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    //Set Camera FOV for each level 
    public void SetCameraFOV(float fovValue)
    {
        virtualCamera.m_Lens.FieldOfView = fovValue;
    }

    //Shake camera with intensity and duration
    public void Shake(float intensity, float duration)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        StartCoroutine(StopShakingRoutine(duration));
    }

    IEnumerator StopShakingRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }
}
