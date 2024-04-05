using System;
using Nautilus.Options.Attributes;
using Nautilus.Json;
using UnityEngine;

namespace AircraftLib
{
    [Menu("Aircraftlib Configuration")]
    public class Config : ConfigFile
    {
        [Keybind("Yaw Left")]
        public KeyCode yawLeftBind = KeyCode.A;

        [Keybind("Yaw Right")]
        public KeyCode yawRightBind = KeyCode.D;
    }
}
