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

        static Car()
        {
            List<string> parameters = new List<string>();
            string className = typeof(Car).Name;

            parameters.Add("What is your car color? (1- Blue ,2- Red ,3- White ,4- Green)");
            parameters.Add("How many doors does your car has? (2/3/4/5)");
            s_ListOfSpecificParamsToUser.Add(className, parameters);
        }

        public Car(EnergySource i_EnergySource)
            : base(4, 29)       // int i_NumOfWheels, float i_MaxAirPressure
        {
            m_EnergySource = i_EnergySource;
        }
    }
}
