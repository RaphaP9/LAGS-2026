using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHarvesting : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Settings")]
    [SerializeField, Range(0f, 3f)] private float timeStopMovement;

    private void OnEnable()
    {
        TotoraCropHandler.OnAnyTotoraCropHarvested += TotoraCropHandler_OnAnyTotoraCropHarvested;
    }

    private void OnDisable()
    {
        TotoraCropHandler.OnAnyTotoraCropHarvested -= TotoraCropHandler_OnAnyTotoraCropHarvested;
    }

    private void TotoraCropHandler_OnAnyTotoraCropHarvested(object sender, TotoraCropHandler.OnTotoraCropEventArgs e)
    {
        playerMovement.StopPlayerForSeconds(timeStopMovement);
    }
}
