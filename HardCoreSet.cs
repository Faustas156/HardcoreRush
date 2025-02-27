using HarmonyLib;
using NeonLite;
using NeonLite.Modules;
using System.Collections.Generic;
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
        static readonly MethodInfo oglvlrshscr = AccessTools.Method(typeof(MenuScreenLevelRushComplete), "OnSetVisible");
        static readonly MethodInfo ogaddcard = AccessTools.Method(typeof(PlayerCardDeck), "Initialize");
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

            if (activate)
            {
                if (!LevelRush.IsHellRush()) return;
                Patching.AddPatch(oglvlrshscr, DisplayHardcoreText, Patching.PatchTarget.Postfix);
                Patching.AddPatch(ogaddcard, OnLevelLoadComplete, Patching.PatchTarget.Postfix);
            }
            else
            {
                Patching.RemovePatch(ogaddcard, OnLevelLoadComplete);
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
        static void DisplayHardcoreText(ref MenuScreenLevelRushComplete __instance)
        {
            if (LevelRush.IsHellRush() && active)
            {
                string text = LevelRush.GetCurrentLevelRushType() switch
                {
                    LevelRush.LevelRushType.WhiteRush => "White's Hardcore",
                    LevelRush.LevelRushType.MikeyRush => "Mikey's Hardcore",
                    LevelRush.LevelRushType.VioletRush => "Violet's Hardcore",
                    LevelRush.LevelRushType.RedRush => "Red's Hardcore",
                    LevelRush.LevelRushType.YellowRush => "Yellow's Hardcore",
                    _ => "Error"
                };

                if (text == "Error") return;

                text += " Hell Rush";

                __instance._rushName.textMeshProUGUI.text = text;
            }
        }

        static void OnLevelLoadComplete()
        {
            GS.AddCard("KATANA");
        }
    }
}

//static void ForceCard(PlayerCardDeck __instance)
//{

//Main.Game.OnLevelLoadComplete += () =>
//{
//    PlayerCard playerCard = new PlayerCard();
//    playerCard.data = Singleton<Game>.Instance.GetGameData().GetCard("KATANA");
//};
