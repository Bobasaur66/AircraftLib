using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFramework;

namespace AircraftLib
{
    public class PlaneRCJetEngine : RCPlaneEngine
    {
        protected override float FORWARD_TOP_SPEED => 4000f;

        protected override float REVERSE_TOP_SPEED => 300f;

        protected override float STRAFE_MAX_SPEED => 0f;

        protected override float VERT_MAX_SPEED => 0f;

        protected override float FORWARD_ACCEL => 400f;

        protected override float REVERSE_ACCEL => 400f;

        protected override float STRAFE_ACCEL => 0f;

        protected override float VERT_ACCEL => 0f;
    }

    public class PlaneRCAgileEngine : RCPlaneEngine
    {
        protected override float FORWARD_TOP_SPEED => 4000f;

        protected override float REVERSE_TOP_SPEED => 1000f;

        protected override float STRAFE_MAX_SPEED => 1000f;

        protected override float VERT_MAX_SPEED => 1000f;

        protected override float FORWARD_ACCEL => 400f;

        protected override float REVERSE_ACCEL => 4000f;

        protected override float STRAFE_ACCEL => 4000f;

        protected override float VERT_ACCEL => 4000f;
    }
}
