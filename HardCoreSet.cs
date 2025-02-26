using HarmonyLib;
using NeonLite;
using NeonLite.Modules;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hardcore
{
    internal class HardCoreSet : IModule
    {
#pragma warning disable CS0414
        const bool priority = false;
        static bool active = true;

        //static GameObject hcButton;
        internal const string h = "Hardcore";

        static void Setup() 
        {
            var setting = Settings.Add("Hardcore", "Hardcore", "hardcoreOn", "Enable Hardcore Mode", "Enables the Hardcore Mode option for Hell Rush.\n \nReduces your HP to 1 and removes your Miracle Katana alongside any ability to heal during the rush.\n \nOnly works with Hell rushes.\n \n\"Those who manage to prevail and overcome these circumstances, are the ones truly worthy of greatness.\"\n \nGood luck. ", true);
            active = setting.SetupForModule(Activate, (_, after) => after);
        }

        //static readonly MethodInfo ogshflbt = AccessTools.Method(typeof(MenuScreenLevelRush), "OnSetVisible");
        //static readonly MethodInfo oglvlrshscr = AccessTools.Method(typeof(MenuScreenLevelRushComplete), "OnSetVisible");
        static void OnLevelLoad(LevelData level)
        {
            if (!level || level.type == LevelData.LevelType.Hub || level.type == LevelData.LevelType.None) return;


            RM.ui.SetRecordMode(LevelRush.IsHellRush());
            GS.recordMode = LevelRush.IsHellRush();

            if (!LevelRush.IsLevelRush()) return;

            RM.mechController.currentHealth = 1;
            RM.mechController.maxHealth = 1;
        }

        static void Activate(bool activate)
        {
            active = activate;

            //NeonLite.Patching.AddPatch(ogshflbt, AddShuffleButton, NeonLite.Patching.PatchTarget.Postfix);
            //NeonLite.Patching.AddPatch(oglvlrshscr, DisplayFailText, NeonLite.Patching.PatchTarget.Postfix);

            if (activate)
            {
                Main.Game.OnLevelLoadComplete += () =>
                    {
                        if (!LevelRush.IsHellRush()) return;
                        GS.AddCard("KATANA");
                    };
            }
            else
            {
                RM.ui.SetRecordMode(false);
                GS.recordMode = false;
            }
        }
        //static void AddShuffleButton(MainMenu __instance) 
        //{
        //    if (!hcButton) 
        //    {
        //        var rushScreen = MainMenu.Instance()._screenLevelRush;
        //        var parent = rushScreen.shuffleToggle.transform.parent;
        //    }
        //}
        //static void DisplayFailText(ref MenuScreenLevelRushComplete __instance) 
        //{
        //    __instance.timeText.
        //}
    }
}
