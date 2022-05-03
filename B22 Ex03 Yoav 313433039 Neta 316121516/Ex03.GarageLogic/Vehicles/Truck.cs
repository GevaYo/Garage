using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class Truck : Vehicle
    {
        private bool m_IsDeliveringCargoInCooling;
        private float m_CargoCapacity;

        public Truck(EnergySource i_EnergySource) : base(16, 24)
        {
            m_EnergySource = i_EnergySource;
            
        }
    }
}
