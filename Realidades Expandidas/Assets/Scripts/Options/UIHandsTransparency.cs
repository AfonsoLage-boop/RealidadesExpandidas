using UnityEngine;
using UnityEngine.UI;

public class UIHandsTransparency : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private RawImage image;

    private void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat("HandsTransparency", 0.65f);
        image.color = new Color(image.color.r, image.color.g, image.color.b, slider.value);
    }

    public void SetValue(float x)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, x);
        PlayerPrefs.SetFloat("HandsTransparency", x);
    }
}

