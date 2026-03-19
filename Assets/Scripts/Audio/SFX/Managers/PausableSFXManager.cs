using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausableSFXManager : SFXManager
{
    public static PausableSFXManager Instance { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    #region Singleton Settings
    protected override void Awake()
    {
        base.Awake();
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one AudioManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }
    #endregion


}
