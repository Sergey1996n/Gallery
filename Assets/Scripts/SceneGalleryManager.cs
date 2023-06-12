using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneGalleryManager : MonoBehaviour
{
    private const string SERVER_URL = "http://data.ikppbb.com/test-task-unity-data/pics/"; 
    private const float IMAGE_SIZE = 528;
    private const float MAX_IMAGE = 66;

    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private Transform contentPanel;

    private int imagesPerPage = 0;
    private int imagerNeedDownload = 0;
    private static List<Texture2D> listTexture;
    private void Awake()
    {
        if (listTexture == null)
        {
            listTexture = new List<Texture2D>();
        }

        float scaleFactor = GetComponent<CanvasScaler>().referenceResolution.x / Screen.width;
        imagerNeedDownload = Mathf.CeilToInt(Screen.height / (IMAGE_SIZE / scaleFactor)) * 2;
        imagerNeedDownload = Mathf.Max(imagerNeedDownload, listTexture.Count);
    }
    private void Start()
    {
        if (listTexture.Count != 0)
        {
            LoadImages();
        }
        LoadImagesServer();
    }

    private void LoadImages()
    {
        foreach (var texture in listTexture)
        {
            GameObject gameObjectImage = CreateImage();
            SettingGameObjectImage(gameObjectImage, texture);
        }
    }

    private GameObject CreateImage()
    {
        GameObject gameObjectImage = Instantiate(imagePrefab, contentPanel);
        gameObjectImage.name = "Image" + (imagesPerPage + 1);
        imagesPerPage++;
        return gameObjectImage;
    }

    private void SettingGameObjectImage(GameObject gameObject, Texture2D texture)
    {
        RawImage image = gameObject.GetComponent<RawImage>();
        image.texture = texture;

        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            Dictionary<string, Texture2D> parameters = new Dictionary<string, Texture2D>()
                {
                    {"Image", texture }
                };
            SceneManagerExtensions.LoadSceneWithHistory("Image", parameters);
        });
    }

    private void LoadImagesServer()
    {
        for (int i = imagesPerPage; i < imagerNeedDownload; i++)
        {
            string url = $"{SERVER_URL}{i + 1}.jpg";
            StartCoroutine(GetImages(url));
        }
    }

    private IEnumerator GetImages(string url)
    {
        GameObject gameObjectImage = CreateImage();
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            gameObjectImage.transform.GetChild(0).gameObject.SetActive(true);
            yield break;
        }

        Texture2D texture = DownloadHandlerTexture.GetContent(request);
        SettingGameObjectImage(gameObjectImage, texture);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManagerExtensions.GoBack();
        }
    }

    public void OnScrollValueChanged(Vector2 position)
    {
        if (position.y <= 0 && imagerNeedDownload < MAX_IMAGE)
        {
            imagerNeedDownload += 2;
            LoadImagesServer();
        }
    }
}
