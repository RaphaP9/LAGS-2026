using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class LoomWeaveVisual : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LoomUI loomUI;
    [SerializeField] private UILineRenderer UILineRenderer;
    [SerializeField] private Transform sewPrefab;

    [Header("Settings")]
    [SerializeField] private Color weaveColor;

    [Header("Lists")]
    [SerializeField] private List<SewUI> sewUIList;

    private void OnEnable()
    {
        loomUI.OnWovenPointAdded += LoomUI_OnWovenPointAdded;
        loomUI.OnWovenPointRemoved += LoomUI_OnWovenPointRemoved;
    }

    private void OnDisable()
    {
        loomUI.OnWovenPointAdded -= LoomUI_OnWovenPointAdded;
        loomUI.OnWovenPointRemoved -= LoomUI_OnWovenPointRemoved;
    }

    private void Start()
    {
        SetLineRendererColor();
    }

    private void SetLineRendererColor()
    {
        UILineRenderer.color = weaveColor;
    }

    private void UpdateLineRenderer(List<LoomPointUI> loomPoints)
    {
        UILineRenderer.ClearPoints();

        foreach(LoomPointUI loomPoint in loomPoints)
        {
            UILineRenderer.AddPoint(loomPoint.RelativePosition);
        }
    }

    private void AddSewPoint(LoomPointUI loomPointUI)
    {
        Transform sewTransform = Instantiate(sewPrefab, transform);
        RectTransform rectTransorm = sewTransform.GetComponent<RectTransform>();
        rectTransorm.localPosition = loomPointUI.RelativePosition;

        SewUI sewUI = sewTransform.GetComponentInChildren<SewUI>();

        if (sewUI == null) return;

        sewUI.SetSew(loomPointUI, weaveColor);
        sewUIList.Add(sewUI);
    }

    private void RemoveSewPoint(LoomPointUI loomPointUI)
    {
        foreach(SewUI sewUI in sewUIList)
        {
            if (sewUI.LinkedLoomPointUI == loomPointUI)
            {
                sewUIList.Remove(sewUI);
                sewUI.DestroySew();
                return;
            }
        }
    }


    #region Subscriptions
    private void LoomUI_OnWovenPointAdded(object sender, LoomUI.OnWovenPointEventArgs e)
    {
        AddSewPoint(e.wovenPoint);
        UpdateLineRenderer(e.wovenLoomPointUIList);
    }

    private void LoomUI_OnWovenPointRemoved(object sender, LoomUI.OnWovenPointEventArgs e)
    {
        RemoveSewPoint(e.wovenPoint);
        UpdateLineRenderer(e.wovenLoomPointUIList);
    }
    #endregion
}
