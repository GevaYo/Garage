using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class Motorcycle : Vehicle
    {
        enum eLicenseType { A, A1, B1, BB }
        readonly eLicenseType r_LicenseType;
        readonly int r_EngineVolume;
    }
}
