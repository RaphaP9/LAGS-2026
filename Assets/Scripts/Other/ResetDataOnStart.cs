using System;
using UnityEngine;

public class ResetDataOnStart : MonoBehaviour
{
    [Header("Enabler")]
    [SerializeField] private bool enable;

    void Start()
    {
        HandleDataReset();
    }

    private void HandleDataReset()
    {
        if (!enable) return;
        StaticDataManager.Instance.ResetData();
    }
}
