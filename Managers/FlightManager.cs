using AircraftLib.VehicleTypes;
using Nautilus.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VehicleFramework;
using VehicleFramework.Engines;
using static GameInput;

namespace AircraftLib.Managers
{
    public class FlightManager
    {
        public static Vehicle playerVehicle;

        public static float liftFactor;

        public static void ShowHintMessage(string messageToHint)
        {
            Nautilus.Utility.BasicText message = new Nautilus.Utility.BasicText(500, 0);
            message.ShowMessage(messageToHint, 2 * Time.deltaTime);
        }

        public static void StabilizeRoll(ModVehicle mv, bool enabled)
        {
            if (mv == null)
            {
                return;
            }
            
            mv.stabilizeRoll = enabled;
        }

        public static void CheckLandingGear(ModVehicle mv)
        {
            if (mv == null)
            {
                return;
            }

            if (mv.GetIsUnderwater())
            {
                mv.gameObject.FindChild("LandingGear").gameObject.SetActive(false);
            }
            else
            {
                mv.gameObject.FindChild("LandingGear").gameObject.SetActive(true);
            }
        }

        // thank you to metious
        public static float GetNormalizedAngle(float angle)
        {
            angle %= 360;
            if (angle > 180)
            {
                return angle - 360;
            }

            return angle;
        }



        public static void DoPlaneFlight(ModVehicle mv)
        {
            if (mv == null)
            {
                return;
            }
            mv.moveOnLand = true;

            mv.worldForces.aboveWaterDrag = 0.5f;

            // set liftFactor between 0 and takeoff speed based on current speed
            liftFactor = Mathf.Clamp(Mathf.Abs(mv.useRigidbody.velocity.z), 0, (mv as PlaneVehicle).takeoffSpeed);
            liftFactor /= (mv as PlaneVehicle).takeoffSpeed;

            // add lift force (gravity x -2 so that max lift is twice gravity)
            mv.useRigidbody.AddRelativeForce(Physics.gravity * -2 * liftFactor, ForceMode.Acceleration);

            mv.useRigidbody.velocity = Vector3.ClampMagnitude(mv.useRigidbody.velocity, (mv as PlaneVehicle).maxSpeed);

            //ShowHintMessage(mv.useRigidbody.velocity.ToString() + liftFactor.ToString());
        }

        public static void DoHoverFlight(ModVehicle mv)
        {
            if (mv == null)
            {
                return;
            }
            mv.moveOnLand = true;

            mv.worldForces.aboveWaterDrag = 0.5f;

            if (mv.gameObject.transform.position.y > GetMaxAltitude(mv))
            {
                mv.useRigidbody.AddForce(Physics.gravity * 2);
            }
            else
            {
                mv.worldForces.aboveWaterGravity = 0f;
            }

            mv.useRigidbody.velocity = Vector3.ClampMagnitude(mv.useRigidbody.velocity, (mv as AirshipVehicle).maxSpeed);
        }



        public static bool checkUnderwaterActual(ModVehicle mv)
        {
            if (mv.gameObject.transform.position.y < 0.75f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static float GetMaxAltitude(ModVehicle mv)
        {
            float currentMVMaxAlt = 1000f;

            return currentMVMaxAlt;
        }
    }
}
