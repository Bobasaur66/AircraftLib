using Nautilus.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VehicleFramework;
using VehicleFramework.VehicleTypes;

namespace AircraftLib
{
    public abstract class PlaneVehicle : Submersible
    {
        public abstract float maxSpeed { get; set; }

        public abstract float takeoffSpeed { get; set; }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (IsPlayerDry)
            {
                FlightManager.DoPlaneFlight(this);
            }
        }
    }
}
