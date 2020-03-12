using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{

    [SerializeField] private bool mobileInput;
    private static bool initialized;
    public static IPlayerInput CurrentInput { get; private set; }

    private void Awake()
    {
        if (initialized)
        {
            Destroy(gameObject);
            return;
        }

        initialized = true;
        DontDestroyOnLoad(gameObject);

        CurrentInput = mobileInput ? new MobileInput() : new GamepadOrKeyboardInput() as IPlayerInput;
    }

    private void Update()
    {
        CurrentInput.Tick();
    }
}