using UnityEngine;
using UnityEngine.UI;

public class UIShowIndicators : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private RawImage[] images;

    private void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat("UIEnable", 1);

        if (slider.value == 1)
        {
            foreach (RawImage im in images)
                im.enabled = true;
        }
        else
        {
            foreach (RawImage im in images)
                im.enabled = false;
        }
    }

    public void SetValue(float x)
    {
        if (x == 1)
        {
            foreach (RawImage im in images)
                im.enabled = true;
        }
        else
        {
            foreach (RawImage im in images)
                im.enabled = false;
        }
        PlayerPrefs.SetFloat("UIEnable", x);
    }
}

