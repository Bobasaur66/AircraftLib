using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Nautilus.Handlers;
using UnityEngine;

namespace AircraftLib
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    [BepInDependency("com.mikjaw.subnautica.vehicleframework.mod")]
    [BepInDependency("com.snmodding.nautilus")]
    public class AircraftLibPlugin : BaseUnityPlugin
    {
        private const string MyGUID = "com.Bobasaur.AircraftLib";
        private const string PluginName = "AircraftLib";
        private const string VersionString = "1.2.0";

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        public static ManualLogSource Log = new ManualLogSource(PluginName);

        public static Config ModConfig { get; } = OptionsPanelHandler.RegisterModOptions<Config>();

        private void Awake()
        {
            Logger.LogInfo($"Will load {PluginName} version {VersionString}.");
            Harmony.PatchAll();
            Logger.LogInfo($"{PluginName} version {VersionString} is loaded.");

            Log = Logger;

            RCCrosshair.LoadAssets();
        }
    }
}
