using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VehicleFramework.Engines;
using VehicleFramework;

namespace AircraftLib
{
    public abstract class RCVehicleEngine : ModVehicleEngine, IPlayerListener
    {
        public Vector2 mousePosition = new Vector2(0f, 0f);

        public bool isControllingRC = false;

        public GameObject canvasClone;

        public override void Start()
        {
            base.Start();

            canvasClone = Instantiate(RCCrosshair.rcCrosshairCanvas);
            isControllingRC = false;
        }

        void IPlayerListener.OnPilotBegin()
        {
            mousePosition = new Vector2(0f, 0f);
            isControllingRC = true;
        }

        void IPlayerListener.OnPilotEnd()
        {
            mousePosition = new Vector2(0f, 0f);
            isControllingRC = false;
            canvasClone.SetActive(false);
        }

        void IPlayerListener.OnPlayerEntry()
        {
            mousePosition = new Vector2(0f, 0f);
            isControllingRC = false;
            canvasClone.SetActive(false);
        }

        void IPlayerListener.OnPlayerExit()
        {
            mousePosition = new Vector2(0f, 0f);
            isControllingRC = false;
            canvasClone.SetActive(false);
        }

        public virtual void Update()
        {
            Vector2 lookDelta = GameInput.GetLookDelta();

            mousePosition = mousePosition + lookDelta;

            if (mousePosition.sqrMagnitude > 40000f)
            {
                mousePosition.Normalize();
                mousePosition *= 200f;
            }

            if (canvasClone == null)
            {
                VehicleFramework.Logger.Log("canvas was null for rccrosshair");
                return;
            }
            else
            {
                canvasClone.SetActive(isControllingRC);
            }
        }

        public void setRCCrosshairPosition()
        {
            if (canvasClone.FindChild("CtrlCrosshairImage") == null)
            {
                VehicleFramework.Logger.Error("Couldn't find ctrlcrosshairimage");
                return;
            }
            else
            {
                canvasClone.FindChild("CtrlCrosshairImage").gameObject.GetComponent<RectTransform>().anchoredPosition = mousePosition;
            }
        }

        public override void DrainPower(Vector3 moveDirection)
        {
            float num = 0.1f;
            float num2 = moveDirection.x + moveDirection.y + moveDirection.z;
            float num3 = Mathf.Pow(0.85f, (float)this.mv.numEfficiencyModules);
            this.mv.GetComponent<PowerManager>().TrySpendEnergy(num * num2 * num3 * Time.deltaTime);
        }
    }

    public abstract class RCPlaneEngine : RCVehicleEngine
    {
        public override bool CanMoveAboveWater { get => true; set => base.CanMoveAboveWater = value; }
        public override bool CanRotateAboveWater { get => true; set => base.CanRotateAboveWater = value; }

        protected override float DragDecay => 4f;

        public float GetCurrentPercentOfEngineForwardTopSpeed()
        {
            float num = Mathf.Abs(this.ForwardMomentum);
            float num2 = this.FORWARD_TOP_SPEED;
            return num / num2;
        }

        public float GetCurrentPercentOfVehicleTopSpeed()
        {
            float num = Mathf.Abs(this.ForwardMomentum + this.RightMomentum + this.UpMomentum);
            float num2 = (mv as PlaneVehicle).maxSpeed * 100;
            return num / num2;
        }

        protected float rollSensitivity = -1.4f;
        protected float pitchSensitivity = -1f;
        protected float yawSensitivity = 1f;
        protected float bankYawSpeed = 1f;

        protected float rollAngle = 0f;
        protected float rollDirection;
        protected float yawValue = 0f;
        protected float inputYawValue = 0f;

        public void RCControlRotation()
        {
            setRCCrosshairPosition();

            // if underwater
            if (FlightManager.checkUnderwaterActual(this.mv) == true)
            {
                // stabilize roll
                AircraftLib.FlightManager.StabilizeRoll(this.mv, true);

                // do yaw with mouse horizontalness
                this.rb.AddTorque(this.mv.transform.up * Time.deltaTime * yawSensitivity * mousePosition.x / 30, (ForceMode)2);

                // pitch
                this.rb.AddTorque(this.mv.transform.right * Time.deltaTime * pitchSensitivity * mousePosition.y / 30, (ForceMode)2);
            }
            else
            {
                // pitch
                this.rb.AddTorque(this.mv.transform.right * Time.deltaTime * pitchSensitivity * GetCurrentPercentOfVehicleTopSpeed() * mousePosition.y / 18, (ForceMode)2);

                // destablize roll
                AircraftLib.FlightManager.StabilizeRoll(this.mv, false);

                // do roll with mouse horizontalness
                this.rb.AddTorque(this.mv.transform.forward * Time.deltaTime * rollSensitivity * GetCurrentPercentOfVehicleTopSpeed() * mousePosition.x / 18, (ForceMode)2);

                // do yaw with bank angle
                rollAngle = this.mv.transform.localEulerAngles.z;
                rollAngle = FlightManager.GetNormalizedAngle(rollAngle);
                rollDirection = rollAngle / Mathf.Abs(rollAngle);

                if (Mathf.Abs(rollAngle) > 90)
                {
                    // thank you metious
                    yawValue = Mathf.Lerp(1f, 0f,
                    Mathf.InverseLerp(90 * rollDirection, 180 * rollDirection, rollAngle)
                    );
                }
                else
                {
                    // thank you metious
                    yawValue = Mathf.Lerp(0f, 1f,
                    Mathf.InverseLerp(0 * rollDirection, 90 * rollDirection, rollAngle)
                    );
                }

                this.rb.AddTorque(Vector3.up * Time.deltaTime * yawSensitivity * GetCurrentPercentOfVehicleTopSpeed() * yawValue * rollDirection * -3, (ForceMode)2);
            }
        }

        public override void ControlRotation()
        {
            RCControlRotation();
        }
    }

    public abstract class RCAirshipEngine : RCVehicleEngine
    {
        public override bool CanMoveAboveWater { get => true; set => base.CanMoveAboveWater = value; }
        public override bool CanRotateAboveWater { get => true; set => base.CanRotateAboveWater = value; }

        protected override float DragDecay => 4f;

        public float GetCurrentPercentOfEngineForwardTopSpeed()
        {
            float num = Mathf.Abs(this.ForwardMomentum);
            float num2 = this.FORWARD_TOP_SPEED;
            return num / num2;
        }

        public float GetCurrentPercentOfVehicleTopSpeed()
        {
            float num = Mathf.Abs(this.ForwardMomentum + this.RightMomentum + this.UpMomentum);
            float num2 = (mv as AirshipVehicle).maxSpeed * 100;
            return num / num2;
        }

        protected float pitchSensitivity = -1f;
        protected float yawSensitivity = 1f;

        public void RCControlRotation()
        {
            setRCCrosshairPosition();

            // stabilize roll
            AircraftLib.FlightManager.StabilizeRoll(this.mv, true);

            // yaw
            this.rb.AddTorque(this.mv.transform.up * Time.deltaTime * yawSensitivity * mousePosition.x / 30, (ForceMode)2);

            // pitch
            this.rb.AddTorque(this.mv.transform.right * Time.deltaTime * pitchSensitivity * mousePosition.y / 30, (ForceMode)2);
        }

        public override void ControlRotation()
        {
            RCControlRotation();
        }
    }
}
