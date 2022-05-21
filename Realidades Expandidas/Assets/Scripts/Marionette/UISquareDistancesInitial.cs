using UnityEngine;
using UnityEngine.UI;

public class UISquareDistancesInitial : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Limb limb;
    [SerializeField] private HV hv;

    private void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat(limb.ToString() + hv.ToString(), 0);    
    }
}

public enum HV { Horizontal, Vertical, }
