using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisibleHandle : MonoBehaviour
{
    [SerializeField] private GameObject handle;

    private Coroutine coroutine;
    private float positionY;

    private void Update()
    {
        if (positionY != handle.transform.position.y) {
            positionY = handle.transform.position.y;
            Hide();
        }
    }

    private void Hide()
    {
        if (coroutine != null)
        {
            handle.SetActive(true);
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(HideCoroutine());
    }

    private IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(1);
        handle.SetActive(false);
    }
}
