using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AircraftLib
{
    public class RCCrosshair
    {
        public static GameObject rcCrosshairCanvas;

        public static void LoadAssets()
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(directory, "assets/aircraftlib"));
            if (assetBundle == null)
            {
                VehicleFramework.Logger.Log("Failure loading RCCrosshair assetbundle");
            }
            else
            {
                rcCrosshairCanvas = assetBundle.LoadAsset<GameObject>("TetherCanvas");

                VehicleFramework.Logger.Log("RCCrosshair assets loaded");
            }
        }
    }
}
