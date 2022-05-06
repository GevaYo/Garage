using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        protected static Dictionary<string, List<string>> s_ListOfSpecificParamsToUser = new Dictionary<string, List<string>>();
        private readonly float r_MaxAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        static Wheel()
        {
            List<string> parameters = new List<string>();
            string className = typeof(Wheel).Name;

            parameters.Add("Who is you wheel manufacturer?");
            parameters.Add("What is your wheel current air pressure?");
            s_ListOfSpecificParamsToUser.Add(className, parameters);
        }

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public void FillAirToMax()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }

        public void AddAirToWheel(float i_AirToAdd)
        {
		    // m_currentAirPressure + i_AirToAdd <= r_MaxAirPressure

        }
    }
}
