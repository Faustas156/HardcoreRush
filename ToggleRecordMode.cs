using UnityEngine;
using UnityEngine.InputSystem;

namespace Hardcore
{
    internal class ToggleRecordMode : Mod
    {
        void Update()
        {
            if (Keyboard.current.yKey.wasPressedThisFrame)
            {
                GS.ToggleRecordMode();
            }
        }
    }
}
