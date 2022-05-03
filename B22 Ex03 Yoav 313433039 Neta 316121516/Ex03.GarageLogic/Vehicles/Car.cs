using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        public enum eCarColors { RED, WHITE, GREEN, BLUE }
        private eCarColors m_Color;
        private readonly byte r_NumOfDoors;

        public Car(EnergySource i_EnergySource) : base(4, 29)
        {
            m_EnergySource = i_EnergySource;
        }
    }
}
