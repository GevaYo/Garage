using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        public enum eCarColors
        {
            RED = 1,
            WHITE,
            GREEN,
            BLUE,
        }

        private enum eQuestionIndex
        {
            CAR_COLOR = 1,
            NUM_OF_DOORS,
        }

        private byte m_NumOfDoors;
        private eCarColors m_Color;

        static Car()
        {
            Dictionary<int, string> parameters = new Dictionary<int, string>();
            string className = typeof(Car).Name;

            parameters.Add((int)eQuestionIndex.CAR_COLOR, "What is your car color? (1- Red ,2- White ,3- Green ,4- Blue)");
            parameters.Add((int)eQuestionIndex.NUM_OF_DOORS, "How many doors does your car has? (2/3/4/5)");
            s_SpecificParamsToUser.Add(className, parameters);
        }

        public Car(EnergySource i_EnergySource, string io_LicensePlate, string io_VehicleModel)
            : base(4, 29, io_LicensePlate, io_VehicleModel)       // int i_NumOfWheels, float i_MaxAirPressure
        {
            m_EnergySource = i_EnergySource;
        }

        public override StringBuilder GetVehicleInfo()
        {
            StringBuilder info = new StringBuilder();

            info.AppendFormat("Number of doors: {0}{1}", m_NumOfDoors, Environment.NewLine);
            info.AppendFormat("Color: {0}{1}", m_Color, Environment.NewLine);

            return info;
        }

        public override void UpdateVehicleParameters(string io_Response, int i_CarQuestion)
        {
            if (i_CarQuestion == (int)eQuestionIndex.CAR_COLOR)
            {
                validateCarColor(io_Response);
            }
            else
            {
                validateNumOfDoors(io_Response);
            }
        }

        private void validateCarColor(string i_ColorToCheck)
        {
            int validColor;

            if (!int.TryParse(i_ColorToCheck, out validColor))
            {
                throw new FormatException("Please enter an integer");
            }
            else
            {
                if (!Enum.IsDefined(typeof(eCarColors), validColor))
                {
                    throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eCarColors)).Length);
                }
            }

            m_Color = (eCarColors)validColor;
        }

        private void validateNumOfDoors(string i_NumOfDoors)
        {
            byte validDoors;

            if (!byte.TryParse(i_NumOfDoors, out validDoors))
            {
                throw new FormatException("Please enter a byte value");
            }
            else
            {
                if(validDoors < 2 || validDoors > 5)
                {
                    throw new ValueOutOfRangeException(2, 5);
                }
            }

            m_NumOfDoors = validDoors;
        }
    }
}
