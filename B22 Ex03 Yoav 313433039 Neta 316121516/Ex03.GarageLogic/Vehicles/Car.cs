using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        public enum eCarColors 
        {
            RED,
            WHITE,
            GREEN,
            BLUE
        }

        private readonly byte r_NumOfDoors;
        private eCarColors m_Color;

        public Car(EnergySource i_EnergySource)
            : base(4, 29)       // int i_NumOfWheels, float i_MaxAirPressure
        {
            m_EnergySource = i_EnergySource;
        }
    }
}
