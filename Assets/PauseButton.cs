using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public static bool Pressed { get; set; }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => Pressed = true);
    }
}