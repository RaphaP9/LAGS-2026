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

    public event EventHandler<OnWovenPointEventArgs> OnWovenPointAdded;
    public event EventHandler<OnWovenPointEventArgs> OnWovenPointRemoved;

    public class OnWovenPointEventArgs : EventArgs
    {
        public LoomPointUI wovenPoint;
        public List<LoomPointUI> wovenLoomPointUIList;
    }

    private void OnEnable()
    {
        LoomPointUI.OnPointWoven += LoomPointUI_OnPointWoven;
        LoomPointUI.OnPointUnwoven += LoomPointUI_OnPointUnwoven;
    }

    private void OnDisable()
    {
        LoomPointUI.OnPointWoven -= LoomPointUI_OnPointWoven;
        LoomPointUI.OnPointUnwoven -= LoomPointUI_OnPointUnwoven;
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
        OnWovenPointAdded?.Invoke(this, new OnWovenPointEventArgs { wovenPoint = loomPointUI, wovenLoomPointUIList = wovenLoomPointUIList });

        CheckAllPointsWoven();
    }

    private void RemoveLoomPointToWovenList(LoomPointUI loomPointUI)
    {
        wovenLoomPointUIList.Remove(loomPointUI);
        OnWovenPointRemoved?.Invoke(this, new OnWovenPointEventArgs {wovenPoint = loomPointUI, wovenLoomPointUIList = wovenLoomPointUIList });
    }

    private void CheckAllPointsWoven()
    {
        if (wovenLoomPointUIList.Count == loomPointUIList.Count)
        {
            if(CheckWeavingSuccessInOrder() || CheckWeavingSuccessInverseOrder()) OnLoomSuccess?.Invoke(this, EventArgs.Empty);
            else OnLoomFail?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool CheckWeavingSuccessInOrder()
    {
        for (int i = 0;i < loomPointUIList.Count; i++)
        {
            if (loomPointUIList[i] != wovenLoomPointUIList[i]) return false;
        }

        return true;
    }

    private bool CheckWeavingSuccessInverseOrder()
    {
        List<LoomPointUI> inversedWovenLoomPointUIList = GeneralUtilities.InvertList(wovenLoomPointUIList);

        for (int i = 0; i < loomPointUIList.Count; i++)
        {
            if (loomPointUIList[i] != inversedWovenLoomPointUIList[i]) return false;
        }

        return true;
    }

    private void LoomPointUI_OnPointWoven(object sender, LoomPointUI.OnPointWovenEventArgs e)
    {
        AddLoomPointToWovenList(e.loomPointUI);
    }

    private void LoomPointUI_OnPointUnwoven(object sender, LoomPointUI.OnPointWovenEventArgs e)
    {
        RemoveLoomPointToWovenList(e.loomPointUI);
    }
}
