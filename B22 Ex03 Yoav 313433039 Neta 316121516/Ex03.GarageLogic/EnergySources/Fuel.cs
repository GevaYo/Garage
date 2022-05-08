using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Fuel : EnergySource
    {
        public enum eQuestionIndex
        {
            CURRENT_AMOUNT = 1,
            FUEL_TYPE,
            REFUEL_AMOUNT,
        }

        private readonly eFuelType r_FuelType;

        static Fuel()
        {
            Dictionary<int, string> newParameters = new Dictionary<int, string>();
            string className = typeof(Fuel).Name;

            newParameters.Add((int)eQuestionIndex.CURRENT_AMOUNT, "What is your current amount of fuel?");
            s_SpecificParamsToUser.Add(className, newParameters);
        }

        public Fuel(float i_MaxEnergyAmount, eFuelType i_FuelType)
            : base(i_MaxEnergyAmount)
        {
            r_FuelType = i_FuelType;
        }

        public override StringBuilder GetEnergySourceInfo()
        {
            StringBuilder info = new StringBuilder();

            info.AppendFormat("Fuel type: {0}{1}", r_FuelType, Environment.NewLine);
            info.Append(base.GetEnergySourceInfo());

            return info;
        }

        public override void UpdateEnergyParameters(string io_Response, int i_FuelQuestion)
        {
            if (i_FuelQuestion == (int)eQuestionIndex.FUEL_TYPE)
            {
                validateFuelType(io_Response);
            }
            else
            {
                float vaildAmount = validateParseToFloat(io_Response);

                if (i_FuelQuestion == (int)eQuestionIndex.REFUEL_AMOUNT)
                {
                    validateAddedEnergyAmount(vaildAmount, r_MaxEnergyAmount - CurrentEnergyAmount);
                }
                else
                {
                    validateCurrentEnergyAmount(vaildAmount);
                }
            }
        }

        private void validateFuelType(string i_FuelTypeToCheck)
        {
            int validFuelType;

            if (!int.TryParse(i_FuelTypeToCheck, out validFuelType))
            {
                throw new FormatException("Please enter an integer");
            }
            else
            {
                if (!Enum.IsDefined(typeof(eFuelType), validFuelType))
                {
                    throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eFuelType)).Length);
                }
            }

            if ((int)r_FuelType != validFuelType)
            {
                throw new ArgumentException("The fuel type does not match your vehicle type");
            }
        }
    }
}
