using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class Wheel
    {
        private string m_ManufaturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public void AddAirToWheel(float i_AirToAdd)
        {
		    // m_currentAirPressure + i_AirToAdd <= r_MaxAirPressure

        }
    }
}
