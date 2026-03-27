using UnityEngine;
using System;

public class ScheduleOpeningManager : MonoBehaviour
{
    public static ScheduleOpeningManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private UIInput UIInput;

    [Header("Settings")]
    [SerializeField] private bool startOpen;
    [SerializeField] private bool setInputOnCooldown;

    [Header("Runtime Filled")]
    [SerializeField] private bool isOpen;

    private bool ScheduleInput => UIInput.GetScheduleDown();
    public bool HasOpenedThisFrame { get; private set; }
    public bool IsOpen => isOpen;

    public static event EventHandler OnScheduleUIOpen;
    public static event EventHandler OnScheduleUIClose;

    public static event EventHandler OnScheduleUIOpenInmediately;
    public static event EventHandler OnScheduleUICloseInmediately;

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        InitializeVariables();
        HandleStartOpen();
    }

    private void Update()
    {
        HandleOpenCloseSchedule();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one ScheduleOpeningManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeVariables()
    {
        SetIsOpen(false);
        HasOpenedThisFrame = false;
    }

    private void HandleStartOpen()
    {
        if (startOpen)
        {
            OpenInmediately();
        }
        else
        {
            CloseInmediately();
        }
    }

    private void HandleOpenCloseSchedule()
    {
        HasOpenedThisFrame = false;

        if (!ScheduleInput) return;
        if (!CanListenToInput()) return;

        if (!isOpen)
        {
            Open();
            HasOpenedThisFrame = true;

            if (setInputOnCooldown) UIInput.SetInputOnCooldown();
        }
        else
        {
            Close();

            if (setInputOnCooldown) UIInput.SetInputOnCooldown();
        }
    }

    private bool CanListenToInput()
    {
        return true;
    }

    private void OpenInmediately()
    {
        OnScheduleUIOpenInmediately?.Invoke(this, EventArgs.Empty);
        SetIsOpen(true);
    }
    private void CloseInmediately()
    {
        OnScheduleUICloseInmediately?.Invoke(this, EventArgs.Empty);
        SetIsOpen(false);
    }

    private void Open()
    {
        OnScheduleUIOpen?.Invoke(this, EventArgs.Empty);
        SetIsOpen(true);
    }

    private void Close()
    {
        OnScheduleUIClose?.Invoke(this, EventArgs.Empty);
        SetIsOpen(false);
    }

    private bool SetIsOpen(bool isOpen) => this.isOpen = isOpen;
}
