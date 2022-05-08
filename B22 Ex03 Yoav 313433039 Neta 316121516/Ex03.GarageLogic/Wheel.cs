using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        private enum eQuestionIndex
        {
            WHEEL_MANUFACTURER = 1,
            CURRENT_AMOUNT,
        }

        protected static Dictionary<string, Dictionary<int, string>> s_SpecificParamsToUser = new Dictionary<string, Dictionary<int, string>>();
        private readonly float r_MaxAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        static Wheel()
        {
            Dictionary<int, string> parameters = new Dictionary<int, string>();
            string className = typeof(Wheel).Name;

            parameters.Add((int)eQuestionIndex.WHEEL_MANUFACTURER, "Who is you wheel manufacturer?");
            parameters.Add((int)eQuestionIndex.CURRENT_AMOUNT, "What is your wheel current air pressure?");
            s_SpecificParamsToUser.Add(className, parameters);
        }

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
        }

        public Dictionary<int, string> DictionaryOfSpecificParamsToUser(string i_Key)
        {
            return s_SpecificParamsToUser[i_Key];
        }

        public void UpdateWheelParameters(string io_Response, int i_WheelQuestion)
        {
            if (i_WheelQuestion == (int)eQuestionIndex.WHEEL_MANUFACTURER)
            {
                validateManufacturer(io_Response);
            }
            else
            {
                validateCurrentAirAmount(io_Response);
            }
        }

        public void FillAirToMax()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }

        private void validateManufacturer(string i_Manufacturer)
        {
            if(!i_Manufacturer.All(char.IsLetter))
            {
                throw new ArgumentException("The manufacturer name must contain only letters");
            }

            m_ManufacturerName = i_Manufacturer;
        }

        private void validateCurrentAirAmount(string i_CurrentAmount)
        {
            float validAmount;

            if (!float.TryParse(i_CurrentAmount, out validAmount))
            {
                throw new FormatException("Please enter a float value");
            }
            else
            {
                if (validAmount < 0 || validAmount > r_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(0, r_MaxAirPressure);
                }
            }

            m_CurrentAirPressure = validAmount;
        }
    }
}
