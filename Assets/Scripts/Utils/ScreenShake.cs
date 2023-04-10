using Cinemachine;
using UnityEngine;

public class ScreenShake : Singleton<ScreenShake>
{
    private CinemachineVirtualCamera _vCam;
    private float _shakeTime;

    private CinemachineBasicMultiChannelPerlin _cinemachineNoise;
    private void OnValidate()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();
        _cinemachineNoise = _vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void ShakeScreen(float amplitude, float frequency, float time)
    {
        _cinemachineNoise.m_AmplitudeGain = amplitude;
        _cinemachineNoise.m_FrequencyGain = frequency;
        _shakeTime = time;
    }
    public void ResetShake()
    {
        _cinemachineNoise.m_AmplitudeGain = 0f;
        _cinemachineNoise.m_FrequencyGain = 0f;
    }
    private void Update()
    {
        if (_shakeTime > 0)
        {
            _shakeTime -= Time.deltaTime;
        }
        else
        {
            ResetShake();
        }
    }
}
