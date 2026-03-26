using System;
using UnityEngine;

public class TotoraCropHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int id;
    [SerializeField] private bool isHarvested;

    public int ID => id;
    public bool IsHarvested => isHarvested;

    public event EventHandler<OnTotoraCropEventArgs> OnTotoraCropInitialized;
    public event EventHandler<OnTotoraCropEventArgs> OnTotoraCropHarvested;
    public static event EventHandler<OnTotoraCropEventArgs> OnAnyTotoraCropHarvested;

    public class OnTotoraCropEventArgs : EventArgs
    {
        public int id;
        public bool isHarvested;
    }

    private void Start()
    {
        InitializeTotoraCrop();
    }

    private void InitializeTotoraCrop()
    {
        isHarvested = StaticDataManager.Instance.GetTotoraCropHarvested(id);
        OnTotoraCropInitialized?.Invoke(this, new OnTotoraCropEventArgs { id = id, isHarvested = isHarvested });
    }

    public void HarvestTotoraCrop()
    {
        isHarvested = true;
        StaticDataManager.Instance.SetTotoraCropHarvested(id);

        OnTotoraCropHarvested?.Invoke(this, new OnTotoraCropEventArgs { id = id, isHarvested = isHarvested });
        OnAnyTotoraCropHarvested?.Invoke(this, new OnTotoraCropEventArgs { id = id, isHarvested = isHarvested });
    }
}
