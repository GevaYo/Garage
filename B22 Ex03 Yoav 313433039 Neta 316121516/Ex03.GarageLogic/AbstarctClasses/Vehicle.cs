using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class Vehicle
    {
        protected string m_Model;
        protected string m_LicensePlateId;
        protected float m_EnergySourcePercentage;
        protected readonly List<Wheel> r_WheelsInVehicle;
        protected EnergySource m_EnergySource;

        protected Vehicle(int i_NumOfWheels, float i_MaxAirPressure)
        {
            r_WheelsInVehicle = new List<Wheel>(i_NumOfWheels);
            for (int i = 0; i < r_WheelsInVehicle.Count; ++i)
            {
                r_WheelsInVehicle[i] = new Wheel(i_MaxAirPressure);
            }
        }
    }
}
