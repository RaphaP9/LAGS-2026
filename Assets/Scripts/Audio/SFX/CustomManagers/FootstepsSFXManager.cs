using UnityEngine;

public class FootstepsSFXManager : CustomSFXManager
{
    private void OnEnable()
    {
        PlayerMovement.OnPlayerStartMoving += PlayerMovement_OnPlayerStartMoving;
        PlayerMovement.OnPlayerStopMoving += PlayerMovement_OnPlayerStopMoving;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerStartMoving -= PlayerMovement_OnPlayerStartMoving;
        PlayerMovement.OnPlayerStopMoving -= PlayerMovement_OnPlayerStopMoving;
    }

    private void PlayerMovement_OnPlayerStartMoving(object sender, System.EventArgs e)
    {
        ReplaceAudioClip(SFXPool.playerFootsteps);
    }

    private void PlayerMovement_OnPlayerStopMoving(object sender, System.EventArgs e)
    {
        StopAudioSource();
    }

}
