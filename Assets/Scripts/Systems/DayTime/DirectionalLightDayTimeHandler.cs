using System;
using UnityEngine;

public class DirectionalLightDayTimeHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform directionalLightTransform;
    [SerializeField] private Light directionalLight;

    [Header("Settings")]
    [SerializeField, Range(0.01f, 100f)] private float lerpSmoothFactor;

    [SerializeField] private float startingRotation;
    [SerializeField] private float endRotation;
    [Space]
    [SerializeField] private Color startingColor;
    [SerializeField] private Color endColor;

    private void OnEnable()
    {
        DayTimeManager.OnTimeInitialized += DayTimeManager_OnTimeInitialized;
    }

    private void OnDisable()
    {
        DayTimeManager.OnTimeInitialized -= DayTimeManager_OnTimeInitialized;
    }

    private void Update()
    {
        HandleDirectionalLightLerp();
    }

    private void HandleDirectionalLightLerp()
    {
        directionalLight.color = Color.Lerp(directionalLight.color, GetTargetColor(DayTimeManager.Instance.GetNormalizedTime()), lerpSmoothFactor*Time.deltaTime);

        float currentY = GeneralUtilities.ConvertToSignedAngle(directionalLight.transform.localEulerAngles.y);
        float targetY = GetTargetRotation(DayTimeManager.Instance.GetNormalizedTime());

        SetEulerAngleY(Mathf.Lerp(currentY, targetY, lerpSmoothFactor * Time.deltaTime));
    }

    private Color GetTargetColor(float normalizedTime)
    {
        Color targetColor = Color.Lerp(startingColor, endColor, normalizedTime);
        return targetColor;
    }

    private float GetTargetRotation(float normalizedTime)
    {
        float targetRotation = Mathf.Lerp(startingRotation, endRotation, normalizedTime);
        return targetRotation;
    }

    private void ApplyColorInstantly(float normalizedTime)
    {
        directionalLight.color = GetTargetColor(normalizedTime);
    }

    private void ApplyRotationInstantly(float normalizedTime)
    {
        SetEulerAngleY(GetTargetRotation(normalizedTime));
    }

    private void SetEulerAngleY(float eulerAngleY)
    {
        Vector3 signedEuler = GeneralUtilities.GetSignedEulerAngles(directionalLightTransform.localRotation);
        signedEuler.y = eulerAngleY;

        directionalLightTransform.localRotation = Quaternion.Euler(signedEuler);
    }

    #region Subscriptions
    private void DayTimeManager_OnTimeInitialized(object sender, DayTimeManager.OnTimeEventArgs e)
    {
        ApplyRotationInstantly(DayTimeManager.Instance.GetNormalizedTime());
        ApplyColorInstantly(DayTimeManager.Instance.GetNormalizedTime());
    }
    #endregion
}
