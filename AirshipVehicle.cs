using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFramework.VehicleTypes;

namespace AircraftLib
{
    public abstract class AirshipVehicle : Submarine
    {
        public abstract float maxSpeed { get; set; }

        public abstract bool floatInWater { get; set; }

        public override void Update()
        {
            base.Update();

            FlightManager.DoHoverFlight(this);

            if (floatInWater)
            {
                this.worldForces.underwaterGravity = -5f;
            }
        }

    }
}
