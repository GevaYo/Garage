using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class Motorcycle : Vehicle
    {
        public enum eLicenseType { A, A1, B1, BB }
        private eLicenseType m_LicenseType;
        private readonly int r_EngineVolume;
        // TODO: private: static readonly / const for "2" (num of wheels), "31" max air pr..
        public Motorcycle(EnergySource i_EnergySource) : base(2, 31)
        {
            m_EnergySource = i_EnergySource;
        }
    }
}
