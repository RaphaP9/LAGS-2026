using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class MinigamesInput : MonoBehaviour
{
    public static MinigamesInput Instance { get; private set; }

    protected virtual void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one MovementInput instance");
        }

        Instance = this;
    }

    public abstract bool CanProcessInput();

    public abstract bool GetFishDown();
    public abstract bool GetWeaveDown();
}
