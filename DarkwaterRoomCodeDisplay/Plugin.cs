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
            CMD_UI uiManager = CMD_UI.Instance;

            if (uiManager == null || uiManager.roomCodeDisplay == null || __instance.playerListPanel == null)
            {
                return;
            }

            TextMeshProUGUI roomCodeDisplay = uiManager.roomCodeDisplay;

            if (__instance.playerListPanel.activeInHierarchy)
            {
                // If the player list is active, show the room code display
                roomCodeDisplay.enabled = true;
            }
            else
            {
                // If the player list is not active, hide the room code display
                roomCodeDisplay.enabled = false;
            }

        }
    }
}
