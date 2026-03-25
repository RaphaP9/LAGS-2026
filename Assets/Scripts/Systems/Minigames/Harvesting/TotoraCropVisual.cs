using UnityEngine;

public class TotoraCropVisual : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TotoraCropHandler totoraCropHandler;
    [Space]
    [SerializeField] private Transform cropHarvestedVFXPrefab;
    [Space]
    [SerializeField] private Transform notHarvestedTransform;
    [SerializeField] private Transform harvestedTransform;

    private void OnEnable()
    {
        totoraCropHandler.OnTotoraCropInitialized += TotoraCropHandler_OnTotoraCropInitialized;
        totoraCropHandler.OnTotoraCropHarvested += TotoraCropHandler_OnTotoraCropHarvested;
    }

    private void OnDisable()
    {
        totoraCropHandler.OnTotoraCropInitialized -= TotoraCropHandler_OnTotoraCropInitialized;
        totoraCropHandler.OnTotoraCropHarvested -= TotoraCropHandler_OnTotoraCropHarvested;
    }

    private void HarvestedVisual()
    {
        notHarvestedTransform.gameObject.SetActive(false);
        harvestedTransform.gameObject.SetActive(true);
    }

    private void NotHarvestedVisual()
    {
        harvestedTransform.gameObject.SetActive(false);
        notHarvestedTransform.gameObject.SetActive(true);
    }

    private void CreateVFX()
    {
        Transform VFXTransform = Instantiate(cropHarvestedVFXPrefab, transform);
    }

    #region Subscriptions
    private void TotoraCropHandler_OnTotoraCropInitialized(object sender, TotoraCropHandler.OnTotoraCropEventArgs e)
    {
        if (e.isHarvested) HarvestedVisual();
        else NotHarvestedVisual();
    }

    private void TotoraCropHandler_OnTotoraCropHarvested(object sender, TotoraCropHandler.OnTotoraCropEventArgs e)
    {
        HarvestedVisual();
        CreateVFX();
    }
    #endregion
}
