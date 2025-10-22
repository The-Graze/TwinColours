using System;
using HarmonyLib;
using Save;
using UI.MmScreens;
using UI.Pm;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TwinColours;

internal abstract class Patches
{
    [HarmonyPatch(typeof(PauseMenuCustomize))]
    private class PauseMenuCustomizePatches
    {
        private static MmColourButton? _custom1, _custom2;

        [HarmonyPatch(nameof(PauseMenuCustomize.Awake))]
        [HarmonyPrefix]
        // ReSharper disable once InconsistentNaming
        private static bool NewAwake(PauseMenuCustomize __instance)
        {
            __instance._buttons =
            [
                __instance.backButton,
                __instance.primaryButton,
                __instance.secondaryButton
            ];

            _custom1 = Object.Instantiate(__instance.primaryButton.GetComponentsInChildren<MmColourButton>()[1],
                __instance.primaryButton.transform, true);
            _custom1.transform.localPosition += new Vector3(128, 0, 0);
            _custom2 = Object.Instantiate(__instance.secondaryButton.GetComponentsInChildren<MmColourButton>()[1],
                __instance.secondaryButton.transform, true);
            _custom2.transform.localPosition += new Vector3(128, 0, 0);

            Plugin.CustomButtons = [_custom1, _custom2];
            Plugin.RefreshColourButtons();

            __instance._primaries = __instance.primaryButton.GetComponentsInChildren<MmColourButton>();
            __instance._secondaries = __instance.secondaryButton.GetComponentsInChildren<MmColourButton>();

            __instance._selected2 = Array.FindIndex<MmColourButton>(__instance._primaries,
                button => button.colour == SaveData.save.primaryColour);
            if (__instance._selected2 == -1) __instance._selected2 = 0;
            return false;
        }
    }
}