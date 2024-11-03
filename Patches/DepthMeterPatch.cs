using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AircraftLib.Patches
{
    [HarmonyPatch(typeof(Ocean), nameof(Ocean.GetDepthOf))]
    public class DepthMeterPatch
    {
        [HarmonyPatch(new Type[] { typeof(Vector3) })]
        private static bool Prefix(ref float __result, Vector3 pos)
        {
            __result = Ocean.GetOceanLevel() - pos.y;

            return false; 
        }
    }
}
