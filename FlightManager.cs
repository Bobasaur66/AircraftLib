using Nautilus.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VehicleFramework;
using static GameInput;

namespace AircraftLib
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

            if (checkUnderwaterActual(mv) == true)
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

            liftFactor = 0f;

            liftFactor = Mathf.Clamp(Mathf.Abs(mv.useRigidbody.velocity.z), 0, (mv as PlaneVehicle).takeoffSpeed);

            liftFactor = liftFactor / (mv as PlaneVehicle).takeoffSpeed;

            mv.useRigidbody.AddRelativeForce(Physics.gravity * -1 * liftFactor, ForceMode.Acceleration);

            mv.useRigidbody.velocity = Vector3.ClampMagnitude(mv.useRigidbody.velocity, (mv as PlaneVehicle).maxSpeed);

            //ShowHintMessage(mv.useRigidbody.velocity.ToString() + liftFactor.ToString());

            ClampAltitude(mv);
        }

        public static void DoHoverFlight(ModVehicle mv)
        {
            if (mv == null)
            {
                return;
            }
            mv.moveOnLand = true;

            mv.worldForces.aboveWaterDrag = 0.5f;

            mv.worldForces.aboveWaterGravity = 0f;

            mv.useRigidbody.velocity = Vector3.ClampMagnitude(mv.useRigidbody.velocity, (mv as AirshipVehicle).maxSpeed);

            ClampAltitude(mv);
        }

        //public static void DoGliderFlight(ModVehicle mv)
        //{
        //    if (mv == null)
        //    {
        //        return;
        //    }
        //    mv.moveOnLand = true;

        //    mv.worldForces.aboveWaterDrag = 0.5f;

        //    liftFactor = 0f;

        //    liftFactor = Mathf.Clamp(Mathf.Abs(mv.useRigidbody.velocity.z), 0, (mv as GliderVehicle).takeoffSpeed);

        //    liftFactor = liftFactor / (mv as GliderVehicle).takeoffSpeed;

        //    liftFactor = liftFactor / 2;

        //    mv.useRigidbody.AddRelativeForce(Physics.gravity * -1 * liftFactor, ForceMode.Acceleration);

        //    mv.useRigidbody.velocity = Vector3.ClampMagnitude(mv.useRigidbody.velocity, (mv as GliderVehicle).maxSpeed);

        //    ClampAltitude(mv);
        //}

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

        // maximum altitudes
        public static float baseMaxAlt = 1000f;

        public static float oneMaxAlt = 2000f;

        public static float twoMaxAlt = 3000f;

        public static float threeMaxAlt = 4000f;

        public static float fourMaxAlt = 6000f;

        public static float fiveMaxAlt = 10000f;


        public static void ClampAltitude(ModVehicle mv)
        {
            return;
        }
    }
}
