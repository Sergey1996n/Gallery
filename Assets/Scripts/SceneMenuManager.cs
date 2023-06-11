using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject galleryButton;

    private void Awake()
    {
        galleryButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnGalleryButtonClicked();
        });

    }

    private void Start()
    {
        galleryButton.SetActive(true);
    }

    public void OnGalleryButtonClicked()
    {
        galleryButton.SetActive(false);

        SceneManagerExtensions.LoadSceneWithHistory("Gallery");
    }
}
