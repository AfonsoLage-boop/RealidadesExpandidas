using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDisableMenuControl : MonoBehaviour
{
    [SerializeField] private Slider[] allSliders;
    [SerializeField] private Button[] allButtons;
    [SerializeField] private GameObject gameObjToSelectOnPause;

    private ObjectSpawner spawner;

    private void Awake()
    {
        spawner = FindObjectOfType<ObjectSpawner>();
    }

    private void OnEnable()
    {
        spawner.PausedEvent += ControlInterectables;
    }

    private void OnDisable()
    {
        spawner.PausedEvent -= ControlInterectables;
    }

    private void ControlInterectables(bool value)
    {
        if (value)
        {
            EventSystem.current.SetSelectedGameObject(gameObjToSelectOnPause);

            foreach (var slider in allSliders)
            {
                slider.interactable = true;
            }

            foreach (var button in allButtons)
            {
                button.interactable = true;
            }
        }
        else
        {
            foreach (var slider in allSliders)
            {
                slider.interactable = false;
            }

            foreach (var button in allButtons)
            {
                button.interactable = false;
            }
        }
    }
}
