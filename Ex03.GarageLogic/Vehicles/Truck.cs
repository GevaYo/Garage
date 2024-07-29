using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private enum eQuestionIndex
        {
            IS_DELIVERING_CARGO_IN_COOLING = 1,
            CARGO_CAPACITY,
        }

        private bool m_IsDeliveringCargoInCooling;
        private float m_CargoCapacity;

        static Truck()
        {
            Dictionary<int, string> parameters = new Dictionary<int, string>();
            string className = typeof(Truck).Name;

            parameters.Add((int)eQuestionIndex.IS_DELIVERING_CARGO_IN_COOLING, "Does your Truck deliver cargo in cooling? (Y/N)");
            parameters.Add((int)eQuestionIndex.CARGO_CAPACITY, "What is your cargo capacity?");
            s_SpecificParamsToUser.Add(className, parameters);
        }

        public Truck(EnergySource i_EnergySource, string io_LicensePlate, string io_VehicleModel)
            : base(16, 24, io_LicensePlate, io_VehicleModel)
        {
            m_EnergySource = i_EnergySource;
        }

        public override StringBuilder GetVehicleInfo()
        {
            StringBuilder info = new StringBuilder();

            string cargoInCooling = m_IsDeliveringCargoInCooling ? "Yes" : "No";
            info.AppendFormat("Is delivering cargo in cooling: {0}{1}", cargoInCooling, Environment.NewLine);
            info.AppendFormat("Cargo capacity: {0}{1}", m_CargoCapacity, Environment.NewLine);

            return info;
        }

        public override void UpdateVehicleParameters(string io_Response, int i_TruckQuestion)
        {
            if (i_TruckQuestion == (int)eQuestionIndex.CARGO_CAPACITY)
            {
                validateCargoCapacity(io_Response);
            }
            else
            {
                validateCargoInCooling(io_Response);
            }
        }

        private void validateCargoCapacity(string i_CargoCapacity)
        {
            int validCargoCapacity;

            if (!int.TryParse(i_CargoCapacity, out validCargoCapacity))
            {
                throw new FormatException("Please enter an integer");
            }

            m_CargoCapacity = validCargoCapacity;
        }

        private void validateCargoInCooling(string i_CargoInCooling)
        {
            string strToCheck = i_CargoInCooling.ToUpper();

            if(strToCheck.Length != 1 || (strToCheck != "Y" && strToCheck != "N"))
            {
                throw new ArgumentException("The value must be 'Y' or 'N'");
            }

            m_IsDeliveringCargoInCooling = strToCheck == "Y" ? true : false;
        }
    }
}
