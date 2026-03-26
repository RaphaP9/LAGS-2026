using System;
using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPositionHandler : MonoBehaviour
{
    public static PlayerPositionHandler Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Runtime Filled")]
    [SerializeField] private Vector3 currentPlayerPosition;

    private void Awake()
    {
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
            //Debug.LogWarning("There is more than one PlayerPositionHandler instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(SetPlayerPositionCoroutine());
    }

    private void Update()
    {
        UpdatePlayerPosition();
    }

    private IEnumerator SetPlayerPositionCoroutine()
    {
        SetPlayerPosition();
        yield return null;
        yield return StartCoroutine(UpdateCinemachineFollowCoroutine());
    }

    private void SetPlayerPosition()
    {
        _rigidbody.position = GeneralUtilities.Vector2ToVector3InZ(StaticDataManager.Instance.Data.currentPlayerPosition);
    }

    private IEnumerator UpdateCinemachineFollowCoroutine()
    {
        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();

        CinemachineCamera vcam = null;

        // Wait until a valid camera is active
        while (vcam == null)
        {
            vcam = brain.ActiveVirtualCamera as CinemachineCamera;
            yield return null;
        }

        if (vcam != null)
        {
            CinemachinePositionComposer composer = vcam.GetComponent<CinemachinePositionComposer>();

            if (composer != null)
            {
                Vector3 originalDamping = composer.Damping;
                composer.Lookahead.Enabled = false;

                composer.Damping = Vector3.zero;

                yield return null;

                composer.Damping = originalDamping;
                composer.Lookahead.Enabled = true;
            }
        }
    }

    private void UpdatePlayerPosition()
    {
        currentPlayerPosition = _rigidbody.position;
    }

    public void SavePlayerPosition()
    {
        StaticDataManager.Instance.SetCurrentPlayerPosition(GeneralUtilities.Vector3ToVector2InZ(currentPlayerPosition));
    }
}
