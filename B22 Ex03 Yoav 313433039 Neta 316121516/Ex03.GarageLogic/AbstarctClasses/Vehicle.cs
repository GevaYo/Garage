using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class Vehicle
    {
        readonly string r_Model;
        readonly string r_LicensePlateId;
        float m_EnergySourcePercentage;
        List<Wheel> m_WheelsInVehicle;
        EnergySource m_EnergySource;
    }
}
