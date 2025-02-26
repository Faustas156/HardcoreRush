using KinematicCharacterController.Examples;
using MelonLoader;
using System;
using UnityEngine;

namespace Hardcore
{
    public class Main : MelonMod
    {
        internal static Game Game { get { return Singleton<Game>.Instance; } }
        public override void OnInitializeMelon() 
        {
            NeonLite.Settings.AddHolder("Hardcore");
            NeonLite.NeonLite.LoadModules(MelonAssembly);
        }
    }
}