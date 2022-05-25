using UnityEngine;
using UnityEngine.UI;

public class UITypeOfControlOptions : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private MarionetteParent marionetteParent;

    [SerializeField] private RawImage[] imagesToDisable;

    private void Awake()
    {
        marionetteParent = FindObjectOfType<MarionetteParent>();
    }

    private void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat("TypeOfControl", 1);

        if (slider.value == 1)
        {
            marionetteParent.ControlWithKeyboard = false;

            if (PlayerPrefs.GetFloat("UIEnable") == 1)
            {
                foreach (var image in imagesToDisable)
                {
                    image.enabled = true;
                }
            }
        }
        else
        {
            marionetteParent.ControlWithKeyboard = true;

            foreach (var image in imagesToDisable)
            {
                image.enabled = false;
            }
        }
    }

    public void SetValue(float x)
    {
        PlayerPrefs.SetFloat("TypeOfControl", x);

        if (slider.value == 1) marionetteParent.ControlWithKeyboard = false;
        else marionetteParent.ControlWithKeyboard = true;

        if (slider.value == 1)
        {
            marionetteParent.ControlWithKeyboard = false;

            if (PlayerPrefs.GetFloat("UIEnable") == 1)
            {
                foreach (var image in imagesToDisable)
                {
                    image.enabled = true;
                }
            }
        }
        else
        {
            marionetteParent.ControlWithKeyboard = true;

            foreach (var image in imagesToDisable)
            {
                image.enabled = false;
            }
        }
    }
}
