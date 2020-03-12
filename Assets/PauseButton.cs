using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public static bool Pressed() => pressedAt == Time.unscaledTime;
    private static float pressedAt;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => pressedAt = Time.unscaledTime);
    }
}