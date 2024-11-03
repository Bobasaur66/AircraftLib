using System;
using Nautilus.Options.Attributes;
using Nautilus.Json;
using UnityEngine;

namespace AircraftLib.Utility
{
    [Menu("Aircraftlib Configuration")]
    public class Config : ConfigFile
    {
        [Keybind("Yaw Left")]
        public KeyCode yawLeftBind = KeyCode.A;

        [Keybind("Yaw Right")]
        public KeyCode yawRightBind = KeyCode.D;

        [Toggle("Invert pitch")]
        public bool invertPitch = false;

        [Toggle("Invert pitch when submerged")]
        public bool invertPitchSubmerged = false;
    }
}
