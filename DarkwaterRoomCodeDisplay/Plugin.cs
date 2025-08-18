using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using TMPro;

namespace DarkwaterRoomCodeDisplay
{
    [BepInPlugin("eu.puszkapotato.darkwater.roomcodedisplay", "Darkwater Display Room Code", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;

            var harmony = new Harmony("eu.puszkapotato.darkwater.roomcodedisplay");
            harmony.PatchAll();

            Log.LogInfo("Darkwater Display Room Code mod has been loaded!");
        }
    }

    [HarmonyPatch]
    public class RoomCodeDisplayPatch
    {
        [HarmonyPatch(typeof(UI_SettingsMenu), "Update")]
        [HarmonyPostfix]
        public static void CheckPlayerListStatus(UI_SettingsMenu __instance)
        {
            // 1. Get the UI manager instance using the singleton pattern.
            CMD_UI uiManager = CMD_UI.Instance;

            // 2. Add safety checks to prevent errors if something hasn't loaded yet.
            if (uiManager == null || uiManager.roomCodeDisplay == null || __instance.playerListPanel == null)
            {
                return;
            }

            // 3. Get a reference to the text object we want to control.
            TextMeshProUGUI roomCodeDisplay = uiManager.roomCodeDisplay;

            // 4. This is your core logic: check the state of the player list and
            //    set the visibility of the room code to match.
            roomCodeDisplay.gameObject.SetActive(__instance.playerListPanel.activeInHierarchy);
        }
    }
}
