using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadingManager : MonoBehaviour
{
    public static string NameScene;

    [SerializeField] private GameObject progressBar;
    [SerializeField] private float loadTime = 10f;

    AsyncOperation sceneLoadingOperation;

    private Image loadingImage;
    private TextMeshProUGUI loadingText;
    private void Awake()
    {
        loadingText = progressBar.GetComponentInChildren<TextMeshProUGUI>();
        loadingImage = progressBar.transform.GetChild(0).GetComponent<Image>();
    }
    void Start()
    {
        StartCoroutine(LoadGalleryScene(NameScene));
    }

    private IEnumerator LoadGalleryScene(string nameScene)
    {
        sceneLoadingOperation = SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Single);
        sceneLoadingOperation.allowSceneActivation = false;

        float timer = 0f;

        while (!sceneLoadingOperation.isDone)
        {
            timer += Time.deltaTime;

            if (timer >= loadTime)
            {
                sceneLoadingOperation.allowSceneActivation = true;
            }

            float progress = Math.Min(timer / loadTime * 100, 100);

            loadingText.text = string.Format("Загрузка... {0:0}%", progress);
            loadingImage.fillAmount = timer / loadTime;

            yield return null;
        }
    }

}
