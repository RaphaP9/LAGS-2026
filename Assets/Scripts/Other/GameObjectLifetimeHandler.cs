using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectLifetimeHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(0f, 10f)] private float lifetime;
    [SerializeField] private bool useRealtime;

    private void Start()
    {
        StartCoroutine(LifetimeCoroutine());
    }

    private IEnumerator LifetimeCoroutine()
    {
        if (useRealtime) yield return new WaitForSecondsRealtime(lifetime);
        else yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
