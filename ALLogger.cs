using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AircraftLib
{
    public class ALLogger
    {
        public static void Log(string message)
        {
            Debug.Log("[Aircraftlib] " + message);
        }

        public static void Warn(string message)
        {
            Debug.LogWarning("[Aircraftlib] " + message);
        }

        public static void Error(string message)
        {
            Debug.LogError("[Aircraftlib] " + message);
        }

        public static void Hint(string messageToHint, float timeToHint)
        {
            Nautilus.Utility.BasicText message = new Nautilus.Utility.BasicText(500, 0);
            message.ShowMessage(messageToHint, timeToHint * Time.deltaTime);
        }
    }
}
