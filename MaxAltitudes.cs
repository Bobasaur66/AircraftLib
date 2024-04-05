using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFramework;

namespace AircraftLib
{
    public class MaxAltitudes
    {
        public static float baseMaxAlt = 1000f;

        public static float oneMaxAlt = 2000f;

        public static float twoMaxAlt = 3000f;

        public static float threeMaxAlt = 4000f;

        public static float fourMaxAlt = 6000f;

        // unlimited basically since teleported back at 8k
        public static float fiveMaxAlt = 10000f;


        public static void ClampAltitude(ModVehicle mv)
        {
            return;
        }
    }
}
