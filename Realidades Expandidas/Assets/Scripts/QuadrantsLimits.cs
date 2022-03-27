using UnityEngine;

public class QuadrantsLimits : MonoBehaviour
{
    // Screen positions
    public Vector2 FirstQuadrantWidth { get; private set; }
    public Vector2 FirstQuadrantHeight { get; private set; }
    public Vector2 SecondQuadrantWidth { get; private set; }
    public Vector2 SecondQuadrantHeight { get; private set; }
    public Vector2 ThirdQuadrantWidth { get; private set; }
    public Vector2 ThirdQuadrantHeight { get; private set; }
    public Vector2 ForthQuadrantWidth { get; private set; }
    public Vector2 ForthQuadrantHeight { get; private set; }
    public Vector2 AllQuadrantsWidth { get; private set; }
    public Vector2 AllQuadrantsHeight { get; private set; }

    private float lastScreenWidth;
    private float lastScreenHeight;

    private void Update()
    {
        if (lastScreenWidth != Screen.width ||
            lastScreenHeight != Screen.height)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            OnScreenResolutionUpdate();
        }
    }

    private void OnScreenResolutionUpdate()
    {
        FirstQuadrantWidth = new Vector2(Screen.width / 3, Screen.width / 2);
        FirstQuadrantHeight = new Vector2(Screen.height / 2, Screen.height / 1.33f);
        SecondQuadrantWidth = new Vector2(Screen.width / 2, Screen.width / 1.5f);
        SecondQuadrantHeight = new Vector2(Screen.height / 2, Screen.height / 1.33f);
        ThirdQuadrantWidth = new Vector2(Screen.width / 2, Screen.width / 1.5f);
        ThirdQuadrantHeight = new Vector2(Screen.height / 4, Screen.height / 2f);
        ForthQuadrantWidth = new Vector2(Screen.width / 3, Screen.width / 2);
        ForthQuadrantHeight = new Vector2(Screen.height / 4, Screen.height / 2f);
        AllQuadrantsWidth = new Vector2(Screen.width / 3, Screen.width / 1.5f);
        AllQuadrantsHeight = new Vector2(Screen.height / 4, Screen.height / 1.33f);
    }
}
