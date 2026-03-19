using UnityEngine;
using System;

public class PauseUIBase : MonoBehaviour
{
    public static event EventHandler<OnPauseUIEventArgs> OnPauseUIBaseOpen;
    public static event EventHandler<OnPauseUIEventArgs> OnPauseUIBaseClose;

    public class OnPauseUIEventArgs : EventArgs
    {
        public PauseUIBase pauseUIBase;
    }

    protected void OnPauseUIBaseOpenMethod()
    {
        OnPauseUIBaseOpen?.Invoke(this, new OnPauseUIEventArgs { pauseUIBase = this});
    }

    protected void OnPauseUIBaseCloseMethod()
    {
        OnPauseUIBaseClose?.Invoke(this, new OnPauseUIEventArgs { pauseUIBase = this });
    }
}