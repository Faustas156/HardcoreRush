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





    //   public class Main : MelonMod
    //   {
    //	public override void OnApplicationLateStart()
    //	{
    //		var gameObject = new GameObject("HardCoreSet", typeof(ModManager));
    //           UnityEngine.Object.DontDestroyOnLoad(gameObject);
    //       }
    //}

    //   internal class ModManager : MonoBehaviour
    //{
    //       private readonly Type[] mods = { typeof(HardCoreSet) };

    //       void Awake()
    //       {
    //           foreach (Type type in mods)
    //               gameObject.AddComponent(type);
    //       }
    //       void Update() 
    //       {
    //           HardCoreSet.HPModifier();
    //       }

    //       void Start()
    //       {
    //           HardCoreSet.HideGUI();
    //       }
    //   }
}