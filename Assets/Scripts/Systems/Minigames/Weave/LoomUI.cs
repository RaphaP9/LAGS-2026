using UnityEngine;
using System.Collections.Generic;
using System;

public class LoomUI : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] private List<LoomPointUI> loomPointUIList;

    [Header("Runtime Filled")]
    [SerializeField] private RectTransform refferenceRectTransform;
    [Space]
    [SerializeField] private List<LoomPointUI> wovenLoomPointUIList;

    public static event EventHandler OnLoomSuccess;
    public static event EventHandler OnLoomFail;

    private void OnEnable()
    {
        LoomPointUI.OnPointWoven += LoomPointUI_OnPointWoven;
    }

    private void OnDisable()
    {
        LoomPointUI.OnPointWoven -= LoomPointUI_OnPointWoven;
    }

    public void InitializeLoomUI(RectTransform refferenceRectTransform, WeavingUI weavingUI)
    {
        this.refferenceRectTransform = refferenceRectTransform;

        for (int i = 0; i < loomPointUIList.Count; i++)
        {
            loomPointUIList[i].SetLoomPointUI(i+1, refferenceRectTransform, weavingUI);
        }
    }

    private void AddLoomPointToWovenList(LoomPointUI loomPointUI)
    {
        wovenLoomPointUIList.Add(loomPointUI);
        CheckAllPointsWoven();
    }

    private void CheckAllPointsWoven()
    {
        if (wovenLoomPointUIList.Count == loomPointUIList.Count)
        {
            if(CheckWeavingSuccess()) OnLoomSuccess?.Invoke(this, EventArgs.Empty);
            else OnLoomFail?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool CheckWeavingSuccess()
    {
        for (int i = 0;i < loomPointUIList.Count; i++)
        {
            if (loomPointUIList[i] != wovenLoomPointUIList[i]) return false;
        }

        return true;
    }

    private void LoomPointUI_OnPointWoven(object sender, LoomPointUI.OnPointWovenEventArgs e)
    {
        AddLoomPointToWovenList(e.loomPointUI);
    }
}
