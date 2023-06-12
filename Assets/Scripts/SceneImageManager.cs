using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneImageManager : MonoBehaviour
{
    [SerializeField] private Image mainImage;
    [SerializeField] private Button back;

    private Texture2D mainTexture;
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        mainTexture = SceneManagerExtensions.GetParam("Image");

        Rect rectSprite = new Rect(Vector2.zero, new Vector2(mainTexture.width, mainTexture.height));
        mainImage.sprite = Sprite.Create(mainTexture, rectSprite, Vector2.zero);

        back.onClick.AddListener(() =>
        {
            SceneManagerExtensions.GoBack();
        });
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManagerExtensions.GoBack();
        }
    }

    private void OnDestroy()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
