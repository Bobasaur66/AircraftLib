using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Nautilus.Handlers;
using UnityEngine;
using AircraftLib.Utility;
using AircraftLib.Managers;

namespace AircraftLib
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    [BepInDependency("com.mikjaw.subnautica.vehicleframework.mod")]
    [BepInDependency("com.snmodding.nautilus")]
    public class AircraftLibPlugin : BaseUnityPlugin
    {
        private const string MyGUID = "com.Bobasaur.AircraftLib";
        private const string PluginName = "AircraftLib";
        private const string VersionString = "1.5.1";

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        public static ManualLogSource Log = new ManualLogSource(PluginName);

        public static Config ModConfig { get; } = OptionsPanelHandler.RegisterModOptions<Config>();

        private void Awake()
        {
            Logger.LogInfo($"Will load {PluginName} version {VersionString}.");


            Harmony.PatchAll();
            Log = Logger;

            RCCrosshair.LoadAssets();



            Logger.LogInfo($"{PluginName} version {VersionString} is loaded.");
        }
    }
}
