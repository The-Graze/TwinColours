using MelonLoader;
using TwinColours;
using UI.MmScreens;
using UnityEngine;

// ReSharper disable InconsistentNaming

[assembly: MelonInfo(typeof(Plugin), "TwinColours", "1.1.1", "Graze")]

namespace TwinColours;

public class Plugin : MelonMod
{
    private static Color PlayerColour1, PlayerColour2;
    internal static MmColourButton[] CustomButtons = [];
    private MelonPreferences_Category? _cat;
    private MelonPreferences_Entry<string>? _playerColour1, _playerColour2;

    public override void OnInitializeMelon()
    {
        _cat = MelonPreferences.CreateCategory("TwinColours", "Twin Colours");
        _cat.SetFilePath("UserData/TwinColour.cfg");

        _playerColour1 = _cat.CreateEntry("Left Leg Colour", "#000000", description: "HEX CODES ONLY!");
        _playerColour2 = _cat.CreateEntry("Right Leg Colour", "#000000", description: "HEX CODES ONLY!");
        MelonPreferences.Save();

        PlayerColour1 = ColorUtility.TryParseHtmlString(_playerColour1.Value, out PlayerColour1)
            ? PlayerColour1
            : Color.black;
        PlayerColour2 = ColorUtility.TryParseHtmlString(_playerColour2.Value, out PlayerColour2)
            ? PlayerColour2
            : Color.black;
    }

    public static void RefreshColourButtons()
    {
        var custom1 = CustomButtons[0];
        var custom2 = CustomButtons[1];

        if (!custom1 || !custom2)
            return;

        custom1.colour = PlayerColour1;
        custom1.altColour = PlayerColour2;
        custom1.Awake();

        custom2.colour = PlayerColour2;
        custom2.altColour = PlayerColour1;
        custom2.Awake();
    }
}