using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        /*protected const int r_NumOfWheels = 16;
        protected const float r_MaxAirPressure = 24;*/
        private bool m_IsDeliveringCargoInCooling;
        private float m_CargoCapacity;

        public Truck(EnergySource i_EnergySource)
            : base(16, 24)
        {
            m_EnergySource = i_EnergySource;
        }
    }
}
