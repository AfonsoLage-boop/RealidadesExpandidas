using UnityEngine;
using UnityEngine.EventSystems;

public class UIIsSelected : MonoBehaviour
{
    [SerializeField] private GameObject isSelectedGameObject;

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            isSelectedGameObject.SetActive(true);
        }
        else
        {
            if (isSelectedGameObject.activeSelf)
            {
                isSelectedGameObject.SetActive(false);
            }
        }
    }
}
