using UnityEngine;

public class MousePauseGame : MonoBehaviour
{
    public Vector3 MousePosition { get; set; }
    private ObjectSpawner objectSpawner;
    [SerializeField] private Animator optionsMenu;
    [SerializeField] private GameplayStatisticsSO statstics;

    private void Awake()
    {
        objectSpawner = FindObjectOfType<ObjectSpawner>();
    }

    private void Update()
    {
        if (statstics.Lives <= 0) return;

        if (objectSpawner.InInitialMenu == false && objectSpawner.IsPaused == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                optionsMenu.SetTrigger("Show");
                objectSpawner.PauseSpawn(true);
            }

            if (Vector3.Distance(Input.mousePosition, MousePosition) > 5)
            {
                optionsMenu.SetTrigger("Show");
                objectSpawner.PauseSpawn(true);
            }

            MousePosition = Input.mousePosition;
        }
    }
}
