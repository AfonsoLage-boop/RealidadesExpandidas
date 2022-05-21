using UnityEngine;
using UnityEngine.UI;

public class UISquareDistancesInitial : MonoBehaviour
{
    [SerializeField] private float defaultValue;
    [SerializeField] private Slider slider;
    [SerializeField] private Limb limb;
    [SerializeField] private HV hv;

    private void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat(limb.ToString() + hv.ToString(), defaultValue);    
    }
}

public enum HV { Horizontal, Vertical, }
