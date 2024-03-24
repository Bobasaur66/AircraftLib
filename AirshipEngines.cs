using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftLib
{
    public class AirshipRCEngine : RCAirshipEngine
    {
        protected override float FORWARD_TOP_SPEED => 2000f;

        protected override float REVERSE_TOP_SPEED => 2000f;

        protected override float STRAFE_MAX_SPEED => 2000f;

        protected override float VERT_MAX_SPEED => 2000f;

        protected override float FORWARD_ACCEL => 200f;

        protected override float REVERSE_ACCEL => 200f;

        protected override float STRAFE_ACCEL => 200f;

        protected override float VERT_ACCEL => 200f;
    }
}
